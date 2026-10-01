namespace NixThis;

//one thing NixThis made disappear, with the value it found there before
internal sealed class HistoryEntry
{
    public string Name = "";
    public string Path = "";
    public string ValueName = "";
    public string ValueType = "DWORD";
    public string Applied = "";
    public string Previous = "";
    public DateTime When;

    public string Line => string.Join("\t", Name, Path, ValueName, ValueType, Applied, Previous, When.ToString("s"));

    public static HistoryEntry? Parse(string line)
    {
        var part = line.Split('\t');
        if (part.Length < 7) return null;
        return new HistoryEntry
        {
            Name = part[0],
            Path = part[1],
            ValueName = part[2],
            ValueType = part[3],
            Applied = part[4],
            Previous = part[5],
            When = DateTime.TryParse(part[6], out var when) ? when : DateTime.MinValue
        };
    }
}
