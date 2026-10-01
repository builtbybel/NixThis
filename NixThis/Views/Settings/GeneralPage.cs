namespace NixThis;

//the one page that is not about filtering: what NixThis speaks
internal sealed partial class GeneralPage : SettingsPage
{
    public GeneralPage()
    {
        InitializeComponent();
        languageLabel.Text = Loc.Get("Settings_Language");

        languageBox.DisplayMember = "Value";
        languageBox.ValueMember = "Key";

        //an empty key means whatever Windows is set to, so the choice can be taken back
        var choices = Loc.Languages();
        choices.Insert(0, new KeyValuePair<string, string>("", Loc.Get("Settings_SystemDefault")));
        foreach (var choice in choices) languageBox.Items.Add(choice);
        languageBox.SelectedIndex = Math.Max(0, choices.FindIndex(item => item.Key == AppSettings.Current.Language));
    }

    public override string Title => Loc.Get("Settings_Title");

    //picking a language is only half of it, the strings have to be read again
    public override void Save()
    {
        var chosen = (KeyValuePair<string, string>)languageBox.SelectedItem;
        AppSettings.Current.Language = chosen.Key;
        Loc.Init(chosen.Key);
    }
}
