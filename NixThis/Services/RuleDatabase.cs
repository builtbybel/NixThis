 namespace NixThis;

//Loads the filter list and recognises elements with it.
//Reads Filters.ini once into Rules, then answers the question the rest of the app keeps asking:
//which rule describes this element? Anything it does not recognise is written to Sightings.log
//and becomes the material for new rules. Does not apply anything.
internal static class RuleDatabase
{
    private static readonly string Folder = System.IO.Path.Combine(AppContext.BaseDirectory, "Data");

    //what the filter file calls itself, so two lists can be told apart. Declared before Rules
    //on purpose: field initialisers run top down, and below Load() its empty start would win.
    public static string Version { get; private set; } = "";

    public static List<Rule> Rules { get; } = Load();

    //the history stores the ini section name, which is how an old entry finds its rule again
    public static Rule? ByName(string name) => Rules.FirstOrDefault(rule => rule.Name == name);

    //the first rule whose words all appear in the sighting, most specific first. A group only
    //says which program a window belongs to, so it is never an answer of its own.
    public static Rule? Find(Sighting seen) =>
        Rules.Where(rule => rule.Action != "group" && Fits(rule, seen))
            .OrderByDescending(rule => rule.Match.Length).FirstOrDefault();

    //the element can be a miss while the program around it is not. Explorer paints the Edge
    //icon, the taskbar, the file windows and the desktop alike, so it is asked last.
    public static string About(Sighting seen) =>
        //the label says what it is
        Apps.FirstOrDefault(app => seen.Names.Any(name => name.Contains(app.ToLowerInvariant())))
        //the window class says which window it sits in
        ?? Rules.FirstOrDefault(rule => rule.Action == "group" &&
               rule.Match.Any(word => seen.Ids.Any(id => Flat(id).Contains(Flat(word)))))?.App.FirstOrDefault()
        //the process only says who drew it, and empty means nobody claims it
        ?? Apps.FirstOrDefault(app => seen.Process.Contains(app.ToLowerInvariant())) ?? "";

    private static IEnumerable<string> Apps => Rules.SelectMany(rule => rule.App);

    //ids first: a German Windows calls the button "Aktive Ansicht", every Windows calls it
    //TaskViewButton, so a rule written against the id works everywhere. Some things carry no
    //usable id at all, and for those the ini names the label, which the locale file translates.
    private static bool Fits(Rule rule, Sighting seen) =>
        (rule.Process.Length == 0 || seen.Process.Contains(rule.Process)) &&
        (rule.Match.Any(word => seen.Ids.Any(id => Flat(id).Contains(Flat(word)))) ||
         rule.Names.Any(word => seen.Names.Any(name => Flat(name).Contains(Flat(word)))));

    //Windows writes the same thing as "Task View" and as "TaskViewButton", so spaces and
    //dashes are dropped on both sides and the ini stays readable
    private static string Flat(string text) => text.Replace(" ", "").Replace("-", "");

    //this is how the database grows: every miss is a line someone can turn into a rule
    public static void Remember(Sighting seen)
    {
        var line = DateTime.Now.ToString("s") + "\t" + seen.Process +
            "\t" + string.Join(" | ", seen.Ids) + "\t" + string.Join(" | ", seen.Names);
        System.IO.File.AppendAllText(System.IO.Path.Combine(Folder, "Sightings.log"), line + Environment.NewLine);
    }

    private static List<Rule> Load()
    {
        var rules = new List<Rule>();
        var file = System.IO.Path.Combine(Folder, "Filters.ini");
        if (!System.IO.File.Exists(file)) return rules;

        Rule? current = null;
        foreach (var raw in System.IO.File.ReadAllLines(file))
        {
            var line = raw.Trim();
            if (line.Length == 0 || line.StartsWith(";")) continue;

            if (line.StartsWith("["))
            {
                current = new Rule { Name = line.Trim('[', ']') };
                rules.Add(current);
                continue;
            }

            var split = line.IndexOf('=');
            if (split < 0) continue;
            var key = line.Substring(0, split).Trim();
            var value = line.Substring(split + 1).Trim();

            //before the first section only the file's own version line is expected
            if (current == null) { if (key == "Version") Version = value; continue; }
            Set(current, key, value);
        }

        //labels are matched in the language Windows is showing, so they are swapped in once here
        foreach (var rule in rules.Where(rule => rule.Names.Length > 0))
            rule.Names = Loc.Names(rule.Name, string.Join(";", rule.Names))
                .ToLowerInvariant().Split(';');

        return rules;
    }

    private static void Set(Rule rule, string key, string value)
    {
        switch (key)
        {
            //matching is done in lower case, so the ini may be written however it reads best
            case "Match": rule.Match = value.ToLowerInvariant().Split(';'); break;
            case "Names": rule.Names = value.ToLowerInvariant().Split(';'); break;
            case "Process": rule.Process = value.ToLowerInvariant(); break;
            //kept as written, because the caption and the search box show it
            case "App": rule.App = value.Split(';'); break;
            case "Where": rule.Where = value; break;
            case "Action": rule.Action = value.ToLowerInvariant(); break;
            //one line per package, so a list grows by adding a line and nothing else
            case "Package": rule.Packages.Add(value); break;
            case "Safety": Enum.TryParse(value, true, out rule.Safety); break;
            case "Question": rule.Question = value; break;
            case "Detail": rule.Detail = value; break;
            case "Path": rule.Path = value; break;
            case "ValueName": rule.ValueName = value; break;
            case "ValueType": rule.ValueType = value; break;
            case "RecommendedValue": rule.RecommendedValue = value; break;
            case "DefaultValue": rule.DefaultValue = value; break;
            case "Restart": rule.Restart = value; break;
        }
    }
}
