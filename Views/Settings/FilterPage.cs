namespace NixThis;

//everything NixThis knows sits in one file, and this page is the way to it
internal sealed partial class FilterPage : SettingsPage
{
    private static readonly string File = System.IO.Path.Combine(AppContext.BaseDirectory, "Data", "Filters.ini");

    public FilterPage()
    {
        InitializeComponent();
        info.Text = Loc.Format("Settings_FilterInfo", RuleDatabase.Rules.Count, RuleDatabase.Version, File);
        openButton.Text = Loc.Get("Settings_OpenFile");
    }

    public override string Title => Loc.Get("Settings_Filters");

    private void OpenButton_Click(object? sender, EventArgs e) => Launch.Open(File);
}
