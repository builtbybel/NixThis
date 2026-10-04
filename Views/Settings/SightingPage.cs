namespace NixThis;

//everything you pointed at that NixThis did not know - the raw material for new filters
internal sealed partial class SightingPage : SettingsPage
{
    private static readonly string Folder = System.IO.Path.Combine(AppContext.BaseDirectory, "Data");
    private static readonly string Log = System.IO.Path.Combine(Folder, "Sightings.log");

    public SightingPage()
    {
        InitializeComponent();
        intro.Text = Loc.Get("Settings_SightingsIntro");
        openButton.Text = Loc.Get("Settings_OpenFolder");
        clearButton.Text = Loc.Get("Settings_Clear");
        Fill();
    }

    public override string Title => Loc.Get("Settings_Sightings");

    private void OpenButton_Click(object? sender, EventArgs e) => Launch.Open(Folder);

    //an emptied log is not a loss: it fills up again the moment you point at something unknown
    private void ClearButton_Click(object? sender, EventArgs e)
    {
        try { System.IO.File.Delete(Log); } catch { }
        Fill();
    }

    private void Fill() => box.Text = System.IO.File.Exists(Log)
        ? System.IO.File.ReadAllText(Log)
        : Loc.Get("Settings_Empty");
}
