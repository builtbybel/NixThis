namespace NixThis;

//everything NixThis made disappear, whether it is still gone, and the way back
internal sealed partial class HistoryForm : Form
{
    public HistoryForm()
    {
        InitializeComponent();

        Text = Loc.Get("History_Title");
        whatColumn.Text = Loc.Get("Scan_ColumnWhat");
        whenColumn.Text = Loc.Get("History_ColumnWhen");
        stateColumn.Text = Loc.Get("History_ColumnState");
        undoButton.Text = Loc.Get("History_Undo");

        Fill();
    }

    private void Fill()
    {
        historyList.Items.Clear();
        foreach (var entry in Applier.History)
        {
            //a value that no longer matches means something wrote over it, usually a Windows update
            var state = Loc.Get(Applier.CameBack(entry) ? "History_CameBack" : "History_Gone");
            historyList.Items.Add(new ListViewItem(
                new[] { Loc.Rule("Name", entry.Name), entry.When.ToString("g"), state }) { Tag = entry });
        }

        //column widths are pixels, and pixels do not scale, so the list measures its own text instead
        historyList.AutoResizeColumns(historyList.Items.Count == 0
            ? ColumnHeaderAutoResizeStyle.HeaderSize : ColumnHeaderAutoResizeStyle.ColumnContent);
    }

    //a setting really goes back to its old value, an app can only be fetched from the Store again
    private void HistoryList_SelectedIndexChanged(object? sender, EventArgs e) =>
        undoButton.Text = Loc.Get(Selected()?.Path == Applier.Appx ? "History_Reinstall" : "History_Undo");

    private void UndoButton_Click(object? sender, EventArgs e)
    {
        var entry = Selected();
        if (entry == null) return;

        //a policy key needs an administrator, and a row that stays put without a word looks broken
        if (!Applier.Undo(entry)) ConfirmCard.Refused(Loc.Rule("Name", entry.Name));
        Fill();
    }

    private HistoryEntry? Selected() =>
        historyList.SelectedItems.Count == 0 ? null : (HistoryEntry)historyList.SelectedItems[0].Tag;
}
