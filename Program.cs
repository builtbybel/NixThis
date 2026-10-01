namespace NixThis;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Loc.Init(AppSettings.Current.Language);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}
