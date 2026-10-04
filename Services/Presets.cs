using System.Text.Json;

namespace NixThis;

//Loads and saves presets, the ready made selections that can be carried to another PC.
//A preset is nothing but a list of rule names in a json file, and this is the only class that
//knows that. Everyone else asks for a Preset and gets one, or hands over names and is done.
//Someone else's file can be anything, so a broken one is a message and not a crash.
internal static class Presets
{
    private static readonly JsonSerializerOptions Pretty = new JsonSerializerOptions { WriteIndented = true };

    //null means nothing was chosen, or the file was not a preset - either way the caller stops
    public static Preset? Load(IWin32Window owner)
    {
        var path = Ask(new OpenFileDialog(), owner);
        if (path == null) return null;

        //anyone can hand you a file, so a broken one is a message and not a crash
        try
        {
            var preset = JsonSerializer.Deserialize<Preset>(File.ReadAllText(path));
            //a file may name no rules at all, and null is not an empty list
            if (preset?.Rules != null) return preset;
        }
        catch { }

        MessageBox.Show(owner, Loc.Get("Main_PresetBad"), AppInfo.Name);
        return null;
    }

    //the file names itself after itself, which is the one thing the saver cannot get wrong
    public static void Save(IWin32Window owner, IEnumerable<string> rules)
    {
        var path = Ask(new SaveFileDialog { FileName = "preset.json" }, owner);
        if (path == null) return;

        var preset = new Preset { Name = Path.GetFileNameWithoutExtension(path), Rules = rules.ToList() };
        try { File.WriteAllText(path, JsonSerializer.Serialize(preset, Pretty)); }
        catch { MessageBox.Show(owner, Loc.Get("Main_PresetBad"), AppInfo.Name); }
    }

    private static string? Ask(FileDialog dialog, IWin32Window owner)
    {
        using (dialog)
        {
            dialog.Filter = Loc.Get("Main_PresetFilter");
            return dialog.ShowDialog(owner) == DialogResult.OK ? dialog.FileName : null;
        }
    }
}
