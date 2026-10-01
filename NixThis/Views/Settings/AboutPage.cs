namespace NixThis;

//what NixThis is, in the words it would use itself
internal sealed partial class AboutPage : SettingsPage
{
    public AboutPage()
    {
        InitializeComponent();
        titleLabel.Text = AppInfo.Name + " " + AppInfo.Version;
        creditLabel.Text = AppInfo.Credit;
        text.Text = Loc.Get("Settings_AboutText");

        //every link carries its address in the Tag, so one handler serves them all
        githubLink.Text = Loc.Get("About_Github");
        githubLink.Tag = AppInfo.Repo;
        issueLink.Text = Loc.Get("About_Issue");
        issueLink.Tag = AppInfo.Issues;
        donateLink.Text = Loc.Get("About_Donate");
        donateLink.Tag = AppInfo.Donate;

        //a language file that names nobody credits nobody
        translatorLink.Visible = Loc.Translator.Length > 0;
        translatorLink.Text = Loc.Format("About_Translation", Loc.Translator);
        translatorLink.Tag = Loc.TranslatorUrl;

        //a translator who left no address is credited all the same, just without a link
        if (Loc.TranslatorUrl.Length == 0) translatorLink.LinkArea = new LinkArea(0, 0);
    }

    private void Link_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e) =>
        Launch.Open((string)((Control)sender!).Tag);

    public override string Title => Loc.Get("Settings_About");
}
