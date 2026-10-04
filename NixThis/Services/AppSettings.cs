using System.Text.Json;

namespace NixThis;

//The few things NixThis remembers about itself from one start to the next.
//Kept in a json file next to the exe and not in the registry, so the app stays portable and
//leaves nothing behind. A missing or damaged file is not an error, it simply means the defaults
//written below.
internal sealed class AppSettings
{
    private static readonly string File = System.IO.Path.Combine(AppContext.BaseDirectory, "Data", "Settings.json");
    private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public static AppSettings Current { get; } = Load();

    //empty means "whatever Windows is set to"
    public string Language { get; set; } = "";

    //the window opens the way you left it: folded or not, and where you put it.
    //zero width means it was never saved, and the designer size decides instead.
    public bool ListOpen { get; set; }
    public int WindowX { get; set; }
    public int WindowY { get; set; }
    public int WindowWidth { get; set; }
    public int WindowHeight { get; set; }

    public string AiProvider { get; set; } = "Groq";

    //only used by the OpenAI-compatible provider, where the address and the model are yours
    public string Endpoint { get; set; } = "https://openrouter.ai/api/v1/chat/completions";
    public string Model { get; set; } = "";

    //one key per provider, so switching back and forth does not lose the other one
    public Dictionary<string, string> ApiKeys { get; set; } = new Dictionary<string, string>();

    public string Key(string provider) =>
        ApiKeys.TryGetValue(provider, out var key) ? key
        : Environment.GetEnvironmentVariable(provider.ToUpperInvariant() + "_API_KEY") ?? "";

    public void Save()
    {
        try { System.IO.File.WriteAllText(File, JsonSerializer.Serialize(this, Options)); }
        catch { }
    }

    private static AppSettings Load()
    {
        try
        {
            return System.IO.File.Exists(File)
                ? JsonSerializer.Deserialize<AppSettings>(System.IO.File.ReadAllText(File), Options) ?? new AppSettings()
                : new AppSettings();
        }
        catch { return new AppSettings(); }
    }
}
