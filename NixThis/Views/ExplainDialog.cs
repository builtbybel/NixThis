namespace NixThis;

//what an entry really does, in the words of the AI that was asked about it
internal sealed partial class ExplainDialog : Form
{
    public ExplainDialog() : this("", "") { }

    public ExplainDialog(string name, string answer)
    {
        InitializeComponent();
        Text = name.Length == 0 ? AppInfo.Name : Loc.Format("Explain_Title", name);
        answerBox.Text = answer;
        closeButton.Text = Loc.Get("Common_Close");
    }
}
