namespace NixThis;

//a selection somebody else made, in the shape it travels in. It names rules and nothing else:
//no registry paths, no values, no text. What this PC does not know is dropped when it is read,
//so the worst a file from a stranger can do is tick boxes you can already see for yourself.
internal sealed class Preset
{
    public string Name { get; set; } = "";
    public string Author { get; set; } = "";
    public List<string> Rules { get; set; } = new List<string>();
}
