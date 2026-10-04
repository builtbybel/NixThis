namespace NixThis;

//Hands a file, a folder or an address to Windows to open.
//A shell that refuses is not worth a dialog, so a failure is simply swallowed.
internal static class Launch
{
    public static void Open(string path)
    {
        try
        {
            System.Diagnostics.Process.Start(
                new System.Diagnostics.ProcessStartInfo(path) { UseShellExecute = true });
        }
        catch { }
    }
}
