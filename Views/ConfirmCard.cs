namespace NixThis;

//the card that opens where you clicked: what it is, what happens, and nothing else
internal sealed partial class ConfirmCard : Form
{
    private readonly Rule? _rule;

    public static void Ask(Sighting seen)
    {
        var rule = RuleDatabase.Find(seen);
        if (rule == null) RuleDatabase.Remember(seen);

        using var card = new ConfirmCard(rule, seen);
        card.Location = Fit(seen.At, card.Size);
        card.ShowDialog();
    }

    //Windows guards machine-wide keys against a normal user: say so, and offer the way up
    public static void Refused(string name)
    {
        if (Applier.IsAdmin)
        {
            MessageBox.Show(Loc.Format("Refused_Text", name),
                AppInfo.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (MessageBox.Show(Loc.Format("Refused_Elevate", name), AppInfo.Name,
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

        if (Applier.Elevate()) Application.Exit();
    }

    //the card opens at the pointer, but never half outside the screen it was opened on
    private static Point Fit(Point at, Size size)
    {
        var screen = Screen.FromPoint(at).WorkingArea;
        return new Point(
            Math.Min(at.X + 12, screen.Right - size.Width - 8),
            Math.Min(at.Y + 12, screen.Bottom - size.Height - 8));
    }

    //the designer needs an empty card, Ask supplies the rule that was pointed at
    public ConfirmCard() : this(null, new Sighting()) { }

    private ConfirmCard(Rule? rule, Sighting seen)
    {
        InitializeComponent();
        _rule = rule;

        Text = AppInfo.Name;
        revealLink.Text = Loc.Get("Card_Reveal");
        blockButton.Text = Loc.Get("Common_Block");
        cancelButton.Text = Loc.Get("Common_Keep");

        questionLabel.Text = rule == null
            ? Loc.Get("Card_Unknown")
            : Loc.Rule("Question", rule.Name, rule.Question);
        //a miss is still worth showing: these are the words that went into the database
        detailLabel.Text = rule == null
            ? Loc.Format("Card_Noted", string.Join(", ", seen.Ids.Take(3)))
            : Loc.Rule("Detail", rule.Name, rule.Detail);

        //nothing to change means nothing to reveal and nothing to confirm
        revealLink.Visible = rule != null;
        blockButton.Visible = rule != null;
    }

    //a value says where it is written, an app says which package is about to go, and looking
    //that one up walks the Start menu, so it waits until someone actually asks
    private void RevealLink_Click(object? sender, EventArgs e)
    {
        pathLabel.Text = _rule!.Action == "uninstall"
            ? Scanner.PackageOf(Label(_rule))
            : _rule.Path + "\\" + _rule.ValueName + " = " + _rule.RecommendedValue;
        pathLabel.Visible = true;
        revealLink.Visible = false;
    }

    //uninstalling waits for PowerShell, so the card stays and says how it went
    private async void BlockButton_Click(object? sender, EventArgs e)
    {
        if (_rule == null) return;

        blockButton.Enabled = false;
        cancelButton.Text = Loc.Get("Common_Close");
        detailLabel.Text = Loc.Get("Card_Working");

        var refused = await Task.Run(() => Applier.Apply(new[] { _rule }));
        detailLabel.Text = Loc.Format(refused.Count == 0 ? "Card_Done" : "Card_Failed", Label(_rule));

        //a machine-wide value can be retried with more rights, a package cannot
        if (refused.Count > 0 && _rule.Action != "uninstall") Refused(_rule.Name);
    }

    private static string Label(Rule rule) => Loc.Rule("Name", rule.Name);
}
