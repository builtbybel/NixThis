namespace NixThis;

//one thing on screen that NixThis knows how to turn off
internal sealed class Rule
{
    public string Name = "";
    public string[] Match = Array.Empty<string>();
    public string[] Names = Array.Empty<string>();   //only for things that have no id worth matching
    public string Process = "";
    //the programs it belongs to, for when the element itself is a miss. More than one, because
    //the taskbar search box is part of Search and part of the taskbar alike.
    public string[] App = Array.Empty<string>();
    //which corner of Windows this sits in, as the list's Where column says it
    public string Where = "";
    public string Action = "";   //empty = write the value, "scan" = open the list,
                                 //"uninstall" = the app, "apps" = a package list, no row of its own
    public List<string> Packages = new List<string>();  //only for "apps": what the list holds
    public Safety Safety;                               //only for "apps": what it says about them
    public string Question = "";
    public string Detail = "";
    public string Path = "";
    public string ValueName = "";
    public string ValueType = "DWORD";
    public string RecommendedValue = "0";
    public string DefaultValue = "1";
    public string Restart = "";
}
