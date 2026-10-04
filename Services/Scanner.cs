namespace NixThis;

//Builds the rows the filter list shows.
//Two sources: the rules from Filters.ini, and the apps installed on this PC, which are read from
//the Start menu and kept for later. The picker uses that same app list to say which app an
//element on screen belongs to. Changes nothing.
internal static class Scanner
{
    //walking the Start menu costs half a second, and the same walk answers the scan, the card
    //and the uninstall alike
    private static List<(string Name, string Family)>? _installed;

    //a filter knows where it lives, so nothing has to be read off the screen to say so
    public static List<Found> Rules() =>
        RuleDatabase.Rules.Where(rule => rule.Action.Length == 0).Select(Row).ToList();

    //the Start menu is the slow half, so it is read off the UI thread and fresh every time,
    //and the lookups after it reuse that read
    public static Task<List<Found>> AppsAsync() => Task.Run(() =>
    {
        _installed = null;
        return Apps();
    });

    //the ini is written in English, a translation replaces the two texts the user reads
    private static Found Row(Rule rule) => new Found
    {
        Name = Loc.Rule("Name", rule.Name),
        Detail = Loc.Rule("Detail", rule.Name, rule.Detail),
        //a filter without a place of its own is a Windows-wide switch
        Where = "Where_" + (rule.Where.Length > 0 ? rule.Where : "Windows"),
        Rule = rule,
        Blocked = Applier.Done(rule)
    };

    //a pinned taskbar button carries the app's AppUserModelID, whose first half is the package
    //family the apps folder reports. It sits inside the id rather than at its start - Windows
    //writes it as "AppId: Family!App" - so the family plus its separator is searched for, which
    //is specific enough to belong to one app and nothing else. An app owning the element beats
    //the group answer, which only names the window it happens to sit in. The hover loop asks
    //this every 60ms, so it reads the list that is there and never waits for a new one.
    public static string AppOf(Sighting seen) =>
        _installed?.FirstOrDefault(app =>
            seen.Ids.Any(id => id.Contains(app.Family.ToLowerInvariant() + "!"))).Name
        ?? RuleDatabase.About(seen);

    //one name can sit on a different package on the next machine - on this one Copilot is the
    //Office hub - so a rule names the app the way Start shows it and the package is looked up
    public static string PackageOf(string name) => Installed()
        .FirstOrDefault(app => app.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).Family ?? "";

    //no curated list of bloatware: this reads what is actually in this user's Start menu
    private static List<Found> Apps() => Installed().Select(app =>
    {
        var safety = AppSafety.Of(app.Family);
        return new Found
        {
            Name = app.Name,
            //an installed app belongs to Apps, not to the Start menu it happens to be listed in
            Where = "Where_Apps",
            Detail = Loc.Get("Scan_App" + safety),
            Package = app.Family,
            Safety = safety
        };
    }).OrderBy(app => app.Name).ToList();

    private static List<(string Name, string Family)> Installed() => _installed ??= Read();

    private static List<(string Name, string Family)> Read()
    {
        var apps = new List<(string, string)>();
        try
        {
            dynamic shell = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application")!)!;
            foreach (dynamic item in shell.NameSpace("shell:AppsFolder").Items())
            {
                //only packaged apps carry an AppUserModelID, and only those can be uninstalled here
                string id = item.Path;
                var cut = id.IndexOf('!');
                if (cut > 0) apps.Add(((string)item.Name, id.Substring(0, cut)));
            }
        }
        catch { }
        return apps;
    }
}
