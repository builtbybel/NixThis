using System.Globalization;
using System.Text.Json;

namespace NixThis;

//Holds every text the user reads, in the language Windows or the settings ask for.
//English is loaded first and the chosen language is laid over it, so a half finished translation
//still shows a complete window instead of empty labels. Rule names and descriptions come from
//the filter file in English and are only replaced here when a translation for them exists.
internal static class Loc
{
    private const string Fallback = "en";
    private static readonly string Folder = System.IO.Path.Combine(AppContext.BaseDirectory, "Localization");
    private static readonly Dictionary<string, string> _english = Read(Fallback);
    private static Dictionary<string, string> _current = _english;

    //labels are read off the screen, so they follow Windows and not the language the app is set to
    private static readonly Dictionary<string, string> _system = Read(Pick(CultureInfo.CurrentUICulture.Name));

    public static string Locale { get; private set; } = Fallback;

    public static void Init(string? wanted)
    {
        Locale = Pick(string.IsNullOrWhiteSpace(wanted) ? CultureInfo.CurrentUICulture.Name : wanted!.Trim());
        _current = Locale == Fallback ? _english : Read(Locale);
    }

    //the name is not translated, so a text file writes {app} and gets the real one here
    public static string Get(string key) =>
        (_current.TryGetValue(key, out var value) && value.Length > 0 ? value
        : _english.TryGetValue(key, out value) && value.Length > 0 ? value : key)
        .Replace("{app}", AppInfo.Name);

    //a translation is a loose file anyone can edit, so a wrong placeholder must not take the app down
    public static string Format(string key, params object[] args)
    {
        try { return string.Format(CultureInfo.CurrentCulture, Get(key), args); }
        catch (FormatException) { return key; }
    }

    //the ini text stays if nobody translated that rule yet, and both may write {app}.
    //a section is named after the rule, so for the name itself there is nothing to hand in
    public static string Rule(string part, string section, string? original = null)
    {
        var key = "Rule_" + part + "_" + section;
        return (_current.TryGetValue(key, out var value) && value.Length > 0 ? value : original ?? section)
            .Replace("{app}", AppInfo.Name);
    }

    //the one text nobody reads: the label a rule recognises an element by, in Windows' own language
    public static string Names(string section, string original) =>
        _system.TryGetValue("Rule_Names_" + section, out var value) && value.Length > 0 ? value : original;

    //who wrote the language file in use, and where to find them. Both are empty for English
    //and for anyone who did not sign their work, and then the about page says nothing about it
    public static string Translator => Meta("_Translator");
    public static string TranslatorUrl => Meta("_TranslatorUrl");

    private static string Meta(string key) => _current.TryGetValue(key, out var value) ? value : "";

    //locale plus the name the file gives itself, which is what the settings combo shows
    public static List<KeyValuePair<string, string>> Languages() =>
        Locales().Select(locale => new KeyValuePair<string, string>(locale,
            Read(locale).TryGetValue("_Language", out var name) && name.Length > 0 ? name : locale)).ToList();

    private static List<string> Locales() =>
        Directory.Exists(Folder)
            ? Directory.GetFiles(Folder, "*.json").Select(System.IO.Path.GetFileNameWithoutExtension).OrderBy(n => n).ToList()!
            : new List<string> { Fallback };

    //"de-DE" also finds "de.json", so a fresh install lands in the user's language by itself
    private static string Pick(string wanted)
    {
        var locales = Locales();
        var neutral = wanted.Split('-')[0];
        return locales.FirstOrDefault(item => item.Equals(wanted, StringComparison.OrdinalIgnoreCase))
            ?? locales.FirstOrDefault(item => item.Equals(neutral, StringComparison.OrdinalIgnoreCase))
            ?? Fallback;
    }

    private static Dictionary<string, string> Read(string locale)
    {
        try
        {
            var path = System.IO.Path.Combine(Folder, locale + ".json");
            if (!System.IO.File.Exists(path)) return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var values = JsonSerializer.Deserialize<Dictionary<string, string>>(System.IO.File.ReadAllText(path));
            return new Dictionary<string, string>(values ?? new Dictionary<string, string>(), StringComparer.OrdinalIgnoreCase);
        }
        catch { return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase); }
    }
}
