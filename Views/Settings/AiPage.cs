namespace NixThis;

//who is asked when you press Explain, and with which key
internal sealed partial class AiPage : SettingsPage
{
    //a key typed but not saved yet still has to survive switching the provider back and forth
    private readonly Dictionary<string, string> _keys =
        new Dictionary<string, string>(AppSettings.Current.ApiKeys, StringComparer.OrdinalIgnoreCase);

    public AiPage()
    {
        InitializeComponent();
        intro.Text = Loc.Get("Settings_AiIntro");
        providerLabel.Text = Loc.Get("Settings_Provider");
        keyLabel.Text = Loc.Get("Settings_ApiKey");
        getKeyLink.Text = Loc.Get("Settings_GetKey");
        endpointLabel.Text = Loc.Get("Settings_Endpoint");
        modelLabel.Text = Loc.Get("Settings_Model");
        testButton.Text = Loc.Get("Settings_Test");

        endpointBox.Text = AppSettings.Current.Endpoint;
        modelBox.Text = AppSettings.Current.Model;

        providerBox.Items.AddRange(Explainer.Providers);
        providerBox.SelectedItem = AppSettings.Current.AiProvider;
        if (providerBox.SelectedIndex < 0) providerBox.SelectedIndex = 0;
    }

    public override string Title => Loc.Get("Settings_AiTitle");

    private string Provider => (string)providerBox.SelectedItem;

    //every provider but the compatible one has its address and model built in
    private void ProviderBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        keyBox.Text = _keys.TryGetValue(Provider, out var key) ? key : "";
        resultLabel.Text = "";

        var own = Provider == Explainer.Compatible;
        endpointLabel.Visible = endpointBox.Visible = own;
        modelLabel.Visible = modelBox.Visible = own;
    }

    private void GetKeyLink_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e) =>
        Launch.Open(Explainer.KeyPage(Provider));

    private async void TestButton_Click(object? sender, EventArgs e)
    {
        Remember();
        if (keyBox.Text.Trim().Length == 0) { resultLabel.Text = Loc.Format("Ai_NoKey", Provider); return; }

        testButton.Enabled = false;
        resultLabel.Text = Loc.Get("Settings_Testing");
        resultLabel.Text = await Explainer.TestAsync(
            Provider, keyBox.Text.Trim(), endpointBox.Text.Trim(), modelBox.Text.Trim());
        testButton.Enabled = true;
    }

    public override void Save()
    {
        Remember();
        AppSettings.Current.AiProvider = Provider;
        AppSettings.Current.Endpoint = endpointBox.Text.Trim();
        AppSettings.Current.Model = modelBox.Text.Trim();
        AppSettings.Current.ApiKeys = _keys;
    }

    private void Remember() => _keys[Provider] = keyBox.Text.Trim();
}
