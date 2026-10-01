namespace NixThis;

//how much of a decision uninstalling an app really is
internal enum Safety
{
    //useful to some, clutter to others, so NixThis stays out of the judgement.
    //first on purpose: it is the default, and a rule that is not an app must not read as protected
    Personal,
    //nobody misses it, it was put there to sell you something
    Safe,
    //Windows itself needs it: NixThis shows it but does not offer to remove it
    Protected
}

//one row of the filter list: either a rule from the ini or an app that is installed
internal sealed class Found
{
    public string Name = "";
    public string Where = "";
    public string Detail = "";
    public Rule? Rule;          //a value to write
    public string Package = ""; //or a package family name to uninstall
    public Safety Safety;       //which only matters for a package
    public bool Blocked;        //what the tick in the list shows, and what Apply compares against

    //what a preset writes down. The ini section and the package family name are the two things
    //that mean the same on every PC - the name in the list is translated and would not travel.
    public string Key => Rule?.Name ?? Package;
}
