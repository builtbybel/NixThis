namespace NixThis;

//what the picker saw around the pointer, kept in two kinds
internal sealed class Sighting
{
    public string Process = "";
    public List<string> Ids = new List<string>();     //AutomationId and ClassName: the same on every Windows
    public List<string> Names = new List<string>();   //what the user reads, and therefore translated
    public Point At;                                  //where the click landed, so the card can open there
}
