namespace NixThis;

//draws what the app is and where to reach it as normal movable labels and links
partial class AboutPage
{
    private System.ComponentModel.IContainer components = null!;
    private Label titleLabel = null!;
    private Label creditLabel = null!;
    private Label text = null!;
    private LinkLabel githubLink = null!;
    private LinkLabel issueLink = null!;
    private LinkLabel donateLink = null!;
    private LinkLabel translatorLink = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.titleLabel = new System.Windows.Forms.Label();
            this.creditLabel = new System.Windows.Forms.Label();
            this.text = new System.Windows.Forms.Label();
            this.githubLink = new System.Windows.Forms.LinkLabel();
            this.issueLink = new System.Windows.Forms.LinkLabel();
            this.donateLink = new System.Windows.Forms.LinkLabel();
            this.translatorLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(25)))), ((int)(((byte)(48)))));
            this.titleLabel.Location = new System.Drawing.Point(18, 16);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(116, 21);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "NixThis 0.40.2";
            // 
            // creditLabel
            // 
            this.creditLabel.AutoSize = true;
            this.creditLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.creditLabel.Location = new System.Drawing.Point(38, 50);
            this.creditLabel.Name = "creditLabel";
            this.creditLabel.Size = new System.Drawing.Size(137, 13);
            this.creditLabel.TabIndex = 1;
            this.creditLabel.Text = "A Belim app creation (2026)";
            // 
            // text
            // 
            this.text.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.text.Location = new System.Drawing.Point(18, 78);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(549, 96);
            this.text.TabIndex = 2;
            this.text.Text = "The ad blocker for Windows itself: point at something Windows added without askin" +
    "g, and it goes away. Everything it took can be brought back.";
            // 
            // githubLink
            // 
            this.githubLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.githubLink.AutoSize = true;
            this.githubLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.githubLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.githubLink.Location = new System.Drawing.Point(18, 190);
            this.githubLink.Name = "githubLink";
            this.githubLink.Size = new System.Drawing.Size(88, 13);
            this.githubLink.TabIndex = 3;
            this.githubLink.TabStop = true;
            this.githubLink.Text = "Follow on GitHub";
            this.githubLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_LinkClicked);
            // 
            // issueLink
            // 
            this.issueLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.issueLink.AutoSize = true;
            this.issueLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.issueLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.issueLink.Location = new System.Drawing.Point(18, 215);
            this.issueLink.Name = "issueLink";
            this.issueLink.Size = new System.Drawing.Size(81, 13);
            this.issueLink.TabIndex = 4;
            this.issueLink.TabStop = true;
            this.issueLink.Text = "Report an issue";
            this.issueLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_LinkClicked);
            // 
            // donateLink
            // 
            this.donateLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.donateLink.AutoSize = true;
            this.donateLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.donateLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.donateLink.Location = new System.Drawing.Point(18, 240);
            this.donateLink.Name = "donateLink";
            this.donateLink.Size = new System.Drawing.Size(98, 13);
            this.donateLink.TabIndex = 5;
            this.donateLink.TabStop = true;
            this.donateLink.Text = "Support this project";
            this.donateLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_LinkClicked);
            // 
            // translatorLink
            // 
            this.translatorLink.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.translatorLink.AutoSize = true;
            this.translatorLink.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.translatorLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.translatorLink.Location = new System.Drawing.Point(18, 276);
            this.translatorLink.Name = "translatorLink";
            this.translatorLink.Size = new System.Drawing.Size(90, 13);
            this.translatorLink.TabIndex = 6;
            this.translatorLink.TabStop = true;
            this.translatorLink.Text = "Translation: Belim";
            this.translatorLink.Visible = false;
            this.translatorLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_LinkClicked);
            // 
            // AboutPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.creditLabel);
            this.Controls.Add(this.text);
            this.Controls.Add(this.githubLink);
            this.Controls.Add(this.issueLink);
            this.Controls.Add(this.donateLink);
            this.Controls.Add(this.translatorLink);
            //the same size the tab strip above these pages already uses
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "AboutPage";
            this.Size = new System.Drawing.Size(585, 494);
            this.ResumeLayout(false);
            this.PerformLayout();

    }
}
