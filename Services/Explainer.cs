using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace NixThis;

//Asks an AI provider what one entry really does, before it is removed.
//Optional and silent until a key is entered in the settings. Only the name and the registry path
//of that single item are sent, never a scan of the PC. The same question is answered from memory
//the second time. Changes nothing itself.
internal static class Explainer
{
    private static readonly HttpClient Client = new HttpClient { Timeout = TimeSpan.FromSeconds(45) };

    //the same question twice costs nothing the second time
    private static readonly Dictionary<string, string> Cache = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    //the last one is anything that speaks the OpenAI protocol, from OpenRouter to a local server
    public const string Compatible = "OpenAI-compatible";

    public static string[] Providers { get; } = { "Groq", "OpenAI", "Anthropic", Compatible };

    //where you get a key, which is the one thing nobody finds without looking it up
    public static string KeyPage(string provider) => provider switch
    {
        "OpenAI" => "https://platform.openai.com/api-keys",
        "Anthropic" => "https://console.anthropic.com/settings/keys",
        Compatible => "https://openrouter.ai/keys",
        _ => "https://console.groq.com/keys"
    };

    private static (string Endpoint, string Model) Service(string provider) => provider switch
    {
        "OpenAI" => ("https://api.openai.com/v1/chat/completions", "gpt-4o-mini"),
        "Anthropic" => ("https://api.anthropic.com/v1/messages", "claude-haiku-4-5-20251001"),
        Compatible => (AppSettings.Current.Endpoint, AppSettings.Current.Model),
        _ => ("https://api.groq.com/openai/v1/chat/completions", "openai/gpt-oss-120b")
    };

    public static async Task<string> ExplainAsync(Found found)
    {
        var provider = AppSettings.Current.AiProvider;
        var key = AppSettings.Current.Key(provider);
        if (key.Length == 0) return Loc.Format("Ai_NoKey", provider);

        var question = found.Rule == null
            ? Loc.Format("Ai_AskApp", found.Name)
            : Loc.Format("Ai_AskRule", found.Name, found.Rule.Path + "\\" + found.Rule.ValueName,
                found.Rule.RecommendedValue);

        var cacheKey = provider + "\n" + Loc.Locale + "\n" + question;
        if (Cache.TryGetValue(cacheKey, out var known)) return known;

        var system = "You are a careful Windows expert. Explain the supplied item accurately and plainly, " +
            "in at most four short sentences. Say what is lost as well as what is gained. " +
            "Do not invent effects and do not provide commands." + InLanguage();

        var (answer, error) = await SendAsync(provider, key, system, question, 400);
        if (error != null) return error;

        Cache[cacheKey] = answer!;
        return answer!;
    }

    //the settings window tests what is typed in, which is not saved yet
    public static async Task<string> TestAsync(string provider, string key, string endpoint, string model)
    {
        var (answer, error) = await SendAsync(provider, key, null, Loc.Get("Ai_TestPrompt"), 80, endpoint, model);
        return error ?? answer!;
    }

    private static async Task<(string? Answer, string? Error)> SendAsync(
        string provider, string key, string? system, string question, int maxTokens,
        string? ownEndpoint = null, string? ownModel = null)
    {
        var (endpoint, model) = Service(provider);
        if (provider == Compatible)
        {
            endpoint = ownEndpoint ?? endpoint;
            model = ownModel ?? model;
            if (model.Trim().Length == 0) return (null, Loc.Get("Ai_NoModel"));
            if (!Uri.TryCreate(endpoint, UriKind.Absolute, out var address) ||
                (address.Scheme != Uri.UriSchemeHttp && address.Scheme != Uri.UriSchemeHttps))
                return (null, Loc.Get("Ai_BadEndpoint"));
        }

        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            object body;
            if (provider == "Anthropic")
            {
                request.Headers.Add("x-api-key", key);
                request.Headers.Add("anthropic-version", "2023-06-01");
                var messages = new[] { new { role = "user", content = question } };
                body = system == null
                    ? (object)new { model, max_tokens = maxTokens, messages }
                    : new { model, max_tokens = maxTokens, system, messages };
            }
            else
            {
                request.Headers.Add("Authorization", "Bearer " + key);
                body = new
                {
                    model,
                    max_tokens = maxTokens,
                    messages = system == null
                        ? new[] { new { role = "user", content = question } }
                        : new[] { new { role = "system", content = system }, new { role = "user", content = question } }
                };
            }

            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            using var response = await Client.SendAsync(request);
            using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var root = document.RootElement;

            if (root.TryGetProperty("error", out var failure))
                return (null, Loc.Format("Ai_Error", provider, Message(failure)));

            var text = provider == "Anthropic"
                ? root.GetProperty("content")[0].GetProperty("text").GetString()
                : root.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

            return string.IsNullOrWhiteSpace(text)
                ? (null, Loc.Format("Ai_Error", provider, Loc.Get("Ai_Empty")))
                : (text!.Replace("**", ""), null);
        }
        catch (Exception problem) { return (null, Loc.Format("Ai_Error", provider, problem.Message)); }
    }

    private static string Message(JsonElement failure) =>
        (failure.ValueKind == JsonValueKind.Object && failure.TryGetProperty("message", out var detail)
            ? detail.GetString() : failure.GetString()) ?? Loc.Get("Ai_Empty");

    private static string InLanguage()
    {
        if (Loc.Locale.StartsWith("en", StringComparison.OrdinalIgnoreCase)) return "";
        try { return " Answer in " + CultureInfo.GetCultureInfo(Loc.Locale).EnglishName + "."; }
        catch { return ""; }
    }
}
