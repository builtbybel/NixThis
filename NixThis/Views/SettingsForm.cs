namespace NixThis;

//only the shell: the tabs on top pick one of the pages, and each page knows itself
internal sealed partial class SettingsForm : Form
{
    private readonly SettingsPage[] _pages =
    {
        new GeneralPage(), new AiPage(), new FilterPage(), new SightingPage(), new AboutPage()
    };

    public SettingsForm()
    {
        InitializeComponent();
        Text = Loc.Get("Settings_Title");
        closeButton.Text = Loc.Get("Common_Close");
        tabs.Renderer = new TabRenderer();

        foreach (var page in _pages)
        {
            page.Dock = DockStyle.Fill;
            host.Controls.Add(page);
            //the tab says the same as the heading, because it is the same thing
            tabs.Items.Add(new ToolStripButton(page.Title)
            {
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                Tag = page
            });
        }

        //picking the first tab is also what hides the other four
        Show((ToolStripButton)tabs.Items[0]);
    }

    private void Tabs_ItemClicked(object? sender, ToolStripItemClickedEventArgs e) =>
        Show((ToolStripButton)e.ClickedItem);

    //a toolbar has no selection of its own, so the open tab is marked here
    private void Show(ToolStripButton tab)
    {
        foreach (ToolStripButton item in tabs.Items)
        {
            item.Checked = item == tab;
            item.ForeColor = item.Checked ? TabRenderer.Accent : Color.FromArgb(55, 65, 81);
        }
        foreach (var page in _pages) page.Visible = page == tab.Tag;
    }

    //the built-in renderers draw a toolbar: a raised edge and a gradient. A browser draws a
    //hairline under the row and a bar under the open tab, and that is all this does.
    private sealed class TabRenderer : ToolStripProfessionalRenderer
    {
        public static readonly Color Accent = Color.FromArgb(99, 102, 241);

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using var surface = new SolidBrush(e.ToolStrip.BackColor);
            e.Graphics.FillRectangle(surface, e.AffectedBounds);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            using var pen = new Pen(Color.FromArgb(221, 222, 238));
            var line = e.AffectedBounds.Bottom - 1;
            e.Graphics.DrawLine(pen, 0, line, e.AffectedBounds.Right, line);
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            var tab = (ToolStripButton)e.Item;
            var box = new Rectangle(Point.Empty, tab.Size);

            if (tab.Selected && !tab.Checked)
            {
                using var hover = new SolidBrush(Color.FromArgb(238, 239, 254));
                e.Graphics.FillRectangle(hover, box);
            }
            if (tab.Checked)
            {
                using var bar = new SolidBrush(Accent);
                e.Graphics.FillRectangle(bar, box.Left, box.Bottom - 2, box.Width, 2);
            }
        }
    }

    //no save button: closing is the save. Nothing here is worth asking a second time about,
    //and a language that only takes effect after a click would be one step too many
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        foreach (var page in _pages) page.Save();
        AppSettings.Current.Save();
        base.OnFormClosing(e);
    }
}
