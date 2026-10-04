namespace NixThis;

//browser shape: the tabs on top pick a page, the page fills the rest.
//The pages themselves are separate controls, so this file only holds the frame.
partial class SettingsForm
{
    private System.ComponentModel.IContainer components = null!;
    private ToolStrip tabs = null!;
    private Panel host = null!;
    private Panel footerBar = null!;
    private Button closeButton = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.tabs = new System.Windows.Forms.ToolStrip();
            this.host = new System.Windows.Forms.Panel();
            this.footerBar = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.Button();
            this.footerBar.SuspendLayout();
            this.SuspendLayout();
            //
            // tabs
            //
            this.tabs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.tabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabs.Font = new System.Drawing.Font("Segoe UI", 10F);
            //a toolbar you cannot drag anywhere, because there is nowhere to drag it to
            this.tabs.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tabs.Location = new System.Drawing.Point(12, 12);
            this.tabs.Name = "tabs";
            this.tabs.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            //a tooltip that only repeats the label the mouse is already on
            this.tabs.ShowItemToolTips = false;
            this.tabs.Size = new System.Drawing.Size(736, 31);
            this.tabs.TabIndex = 0;
            this.tabs.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Tabs_ItemClicked);
            //
            // host
            //
            this.host.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.host.Dock = System.Windows.Forms.DockStyle.Fill;
            this.host.Location = new System.Drawing.Point(12, 43);
            this.host.Name = "host";
            //the tabs are the only edge in here, so the page keeps its distance from them
            this.host.Padding = new System.Windows.Forms.Padding(0, 14, 0, 0);
            this.host.Size = new System.Drawing.Size(736, 463);
            this.host.TabIndex = 1;
            //
            // footerBar
            //
            this.footerBar.Controls.Add(this.closeButton);
            this.footerBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerBar.Location = new System.Drawing.Point(12, 506);
            this.footerBar.Name = "footerBar";
            this.footerBar.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.footerBar.Size = new System.Drawing.Size(736, 42);
            this.footerBar.TabIndex = 2;
            //
            // closeButton
            //
            //the same button as Apply on the main window, so the two windows read as one app
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.closeButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.ForeColor = System.Drawing.Color.White;
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Location = new System.Drawing.Point(626, 10);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(110, 32);
            this.closeButton.TabIndex = 0;
            //
            // SettingsForm
            //
            this.AcceptButton = this.closeButton;
            //one surface for the whole dialog: the tabs and the page must not read as two boxes
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(760, 560);
            //docking runs back to front: the footer takes the bottom edge, the tabs the top one,
            //and the host gets whatever is left
            this.Controls.Add(this.host);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.footerBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.footerBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }
}
