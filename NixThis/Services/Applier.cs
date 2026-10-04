using System.Diagnostics;
using Microsoft.Win32;

namespace NixThis;

//Carries out the changes, and takes them back. The only class that touches the PC.
//Writes the registry value a rule names, uninstalls apps, and restarts Explorer where a setting
//only takes effect that way. Before every write the old value is saved to History.tsv, so undo
//puts back exactly what was there. A write Windows refuses is reported by name, not swallowed.
internal static class Applier
{
    private static readonly string File = System.IO.Path.Combine(AppContext.BaseDirectory, "Data", "History.tsv");

    //an entry with this instead of a registry path is an uninstalled app, not a written value
    public const string Appx = "appx";

    //some tweaks have no Windows default to write back, they only exist once someone sets them
    private const string Delete = "<deletevalue>";

    public static bool IsAdmin { get; } = new System.Security.Principal.WindowsPrincipal(
        System.Security.Principal.WindowsIdentity.GetCurrent())
        .IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);

    //machine-wide keys and policies are closed to a normal user, so NixThis starts itself again
    public static bool Elevate()
    {
        try
        {
            Process.Start(new ProcessStartInfo(Application.ExecutablePath) { UseShellExecute = true, Verb = "runas" });
            return true;
        }
        catch { return false; }
    }

    public static List<HistoryEntry> History { get; } = Load();

    //a whole list at once, because ten rules must not restart Explorer ten times,
    //and the names of the ones that were refused come back to be shown
    public static List<string> Apply(IEnumerable<Rule> rules)
    {
        var refused = new List<string>();
        var restart = false;
        var redraw = false;

        foreach (var rule in rules)
        {
            //a rule can stand for an app instead of a value, and then there is nothing to write
            if (rule.Action == "uninstall")
            {
                var name = Loc.Rule("Name", rule.Name);
                refused.AddRange(Remove(new Found { Name = name, Package = Scanner.PackageOf(name) }));
                continue;
            }

            //read before writing, and record only once the value really went in.
            //a value that was not there is put back by being removed again, not by writing
            //the default over it - those are two different states of the registry
            var previous = Read(rule.Path, rule.ValueName) ?? Delete;
            if (!Write(rule.Path, rule.ValueName, rule.ValueType, rule.RecommendedValue))
            {
                refused.Add(rule.Name);
                continue;
            }

            History.Insert(0, new HistoryEntry
            {
                Name = rule.Name,
                Path = rule.Path,
                ValueName = rule.ValueName,
                ValueType = rule.ValueType,
                Applied = rule.RecommendedValue,
                Previous = previous,
                When = DateTime.Now
            });
            restart |= rule.Restart == "explorer";
            redraw |= rule.Restart == "desktop";
        }

        Save();
        //a restart redraws the desktop on its way back up, so it is never needed on top
        if (restart) RestartExplorer();
        else if (redraw) RedrawDesktop();
        return refused;
    }

    //a list of rows whose tick changed, counted off as it goes. The values are collected and
    //written together at the end, so Explorer is restarted once and not once per row.
    public static List<string> Apply(List<(Found Found, bool Block)> todo, IProgress<int> step)
    {
        var refused = new List<string>();
        var rules = new List<Rule>();

        for (var index = 0; index < todo.Count; index++)
        {
            step.Report(index + 1);
            var (found, block) = todo[index];
            //an undo is refused just as a write is, and a tick that quietly comes back is a lie
            if (!block) { if (!Restore(found)) refused.Add(found.Name); }
            //an app is gone the moment it is uninstalled, a value waits for the others
            else if (found.Rule != null) rules.Add(found.Rule);
            else refused.AddRange(Remove(found));
        }

        refused.AddRange(Apply(rules));
        return refused;
    }

    //taking an installed app away, and its name back if Windows refused
    public static List<string> Remove(Found found)
    {
        if (!Uninstall(found.Package)) return new List<string> { found.Name };

        History.Insert(0, new HistoryEntry
        {
            Name = found.Name,
            Path = Appx,
            ValueName = found.Package,
            When = DateTime.Now
        });
        Save();
        return new List<string>();
    }

    //PowerShell reports its own failures badly, so the answer is whether the package is still there
    private static bool Uninstall(string family)
    {
        var name = family.Split('_')[0];
        if (name.Length == 0 || name.Any(letter =>
            !char.IsLetterOrDigit(letter) && letter != '.' && letter != '-')) return false;

        try
        {
            using var powershell = Process.Start(new ProcessStartInfo("powershell",
                "-NoProfile -Command \"Get-AppxPackage -Name '" + name + "' | Remove-AppxPackage; " +
                "if (Get-AppxPackage -Name '" + name + "') { exit 1 }\"")
            { UseShellExecute = false, CreateNoWindow = true });

            powershell!.WaitForExit();
            return powershell.ExitCode == 0;
        }
        catch { return false; }
    }

    //false means Windows refused the write - a policy key needs an administrator - and then the
    //entry stays, so the same undo can be tried again once NixThis has been restarted elevated
    public static bool Undo(HistoryEntry entry)
    {
        //a removed app cannot be written back, the Store page is the only honest way back.
        //the address goes to the shell itself - handed to explorer.exe it reads as a path,
        //fails, and opens the Documents folder instead
        if (entry.Path == Appx)
        {
            if (entry.ValueName.Length == 0) return false;
            Launch.Open("ms-windows-store://pdp/?PFN=" + entry.ValueName);
        }
        else if (!Write(entry.Path, entry.ValueName, entry.ValueType, entry.Previous)) return false;
        else Refresh(entry.Name);

        History.Remove(entry);
        Save();
        return true;
    }

    //taking a tick away from something NixThis never blocked has no old value to go back to,
    //only the default the rule names
    public static bool Restore(Found found)
    {
        var entry = History.FirstOrDefault(old => old.Name == (found.Rule?.Name ?? found.Name));
        if (entry != null) return Undo(entry);
        //an app that was never uninstalled has nothing to put back
        if (found.Rule == null) return true;
        if (!Write(found.Rule.Path, found.Rule.ValueName, found.Rule.ValueType, found.Rule.DefaultValue)) return false;
        Refresh(found.Rule.Name);
        return true;
    }

    //only a rule that needed Explorer restarted to take effect needs it to take it back
    private static void Refresh(string name)
    {
        var restart = RuleDatabase.ByName(name)?.Restart;
        if (restart == "explorer") RestartExplorer();
        else if (restart == "desktop") RedrawDesktop();
    }

    //a value that already says what the rule wants is nothing left to remove
    public static bool Done(Rule rule) => Read(rule.Path, rule.ValueName) == rule.RecommendedValue;

    //Windows updates like to put their own buttons back, and this is how you notice
    public static bool CameBack(HistoryEntry entry) =>
        entry.Path != Appx && Read(entry.Path, entry.ValueName) != entry.Applied;

    private static string? Read(string path, string valueName)
    {
        try
        {
            using var key = Open(path, false);
            return key?.GetValue(valueName)?.ToString();
        }
        catch { return null; }
    }

    //some keys, above all everything under Policies, are closed to a normal user even in HKCU,
    //and a refused write must not take the whole app down
    private static bool Write(string path, string valueName, string type, string value)
    {
        try
        {
            using var key = Open(path, true);
            if (key == null) return false;
            if (value == Delete) { key.DeleteValue(valueName, false); return true; }
            key.SetValue(valueName, type == "DWORD" ? (object)int.Parse(value) : value,
                type == "DWORD" ? RegistryValueKind.DWord : RegistryValueKind.String);
            return true;
        }
        catch (UnauthorizedAccessException) { return false; }
        catch (System.Security.SecurityException) { return false; }
    }

    private static RegistryKey? Open(string path, bool write)
    {
        var cut = path.IndexOf('\\');
        if (cut < 0) return null;

        var root = path.Substring(0, cut).ToUpperInvariant() switch
        {
            "HKEY_LOCAL_MACHINE" => Registry.LocalMachine,
            "HKEY_CLASSES_ROOT" => Registry.ClassesRoot,
            _ => Registry.CurrentUser
        };
        var sub = path.Substring(cut + 1);
        return write ? root.CreateSubKey(sub) : root.OpenSubKey(sub);
    }

    //the taskbar only reads these values when it starts, and Windows brings Explorer back by itself
    private static void RestartExplorer()
    {
        foreach (var shell in Process.GetProcessesByName("explorer")) shell.Kill();
    }

    //telling the shell that associations changed makes it draw the desktop icons again.
    //Restarting Explorer would show the change too - and would throw every icon on a second
    //monitor back onto the first one, which is a high price for one icon less.
    private static void RedrawDesktop() => SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);

    [System.Runtime.InteropServices.DllImport("shell32.dll")]
    private static extern void SHChangeNotify(int what, uint flags, IntPtr first, IntPtr second);

    private static List<HistoryEntry> Load()
    {
        if (!System.IO.File.Exists(File)) return new List<HistoryEntry>();
        return System.IO.File.ReadAllLines(File).Select(HistoryEntry.Parse).OfType<HistoryEntry>().ToList();
    }

    private static void Save() =>
        System.IO.File.WriteAllLines(File, History.Select(entry => entry.Line));
}
