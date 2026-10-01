namespace NixThis;

//docking lets the log box take whatever is left, so no size has to be kept in step.
//only the two buttons are anchored, because docking cannot put a gap between them.
partial class SightingPage
{
    private System.ComponentModel.IContainer components = null!;
    private Label intro = null!;
    private TextBox box = null!;
    private Panel bar = null!;
    private Button clearButton = null!;
    private Button openButton = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.intro = new System.Windows.Forms.Label();
            this.box = new System.Windows.Forms.TextBox();
            this.bar = new System.Windows.Forms.Panel();
            this.openButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.bar.SuspendLayout();
            this.SuspendLayout();
            // 
            // intro
            // 
            this.intro.Dock = System.Windows.Forms.DockStyle.Top;
            this.intro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.intro.Location = new System.Drawing.Point(18, 16);
            this.intro.Name = "intro";
            this.intro.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.intro.Size = new System.Drawing.Size(549, 46);
            this.intro.TabIndex = 1;
            this.intro.Text = "Everything you pointed at that NixThis did not recognise. This is where new filter" +
    "s come from.";
            // 
            // box
            // 
            this.box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.box.Font = new System.Drawing.Font("Consolas", 9F);
            this.box.Location = new System.Drawing.Point(18, 62);
            this.box.Multiline = true;
            this.box.Name = "box";
            this.box.ReadOnly = true;
            this.box.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.box.Size = new System.Drawing.Size(549, 378);
            this.box.TabIndex = 2;
            this.box.Text = "Nothing yet.";
            this.box.WordWrap = false;
            // 
            // bar
            // 
            this.bar.Controls.Add(this.openButton);
            this.bar.Controls.Add(this.clearButton);
            this.bar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bar.Location = new System.Drawing.Point(18, 440);
            this.bar.Name = "bar";
            this.bar.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.bar.Size = new System.Drawing.Size(549, 38);
            this.bar.TabIndex = 3;
            // 
            // openButton
            // 
            this.openButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.openButton.Location = new System.Drawing.Point(301, 10);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(120, 28);
            this.openButton.TabIndex = 0;
            this.openButton.Text = "Open folder";
            this.openButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // clearButton
            //
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            //the 8 pixels to the button on its left are the gap, which docking cannot express
            this.clearButton.Location = new System.Drawing.Point(429, 10);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(120, 28);
            this.clearButton.TabIndex = 1;
            this.clearButton.Text = "Clear";
            this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // SightingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.box);
            this.Controls.Add(this.bar);
            this.Controls.Add(this.intro);
            //the same size the tab strip above these pages already uses
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "SightingPage";
            this.Padding = new System.Windows.Forms.Padding(18, 16, 18, 16);
            this.Size = new System.Drawing.Size(585, 494);
            this.bar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }
}
