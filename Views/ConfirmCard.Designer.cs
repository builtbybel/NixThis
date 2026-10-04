namespace NixThis;

//the card: one question, one consequence, the registry path on demand, two buttons
partial class ConfirmCard
{
    private System.ComponentModel.IContainer components = null!;
    private Label questionLabel = null!;
    private Label detailLabel = null!;
    private Label pathLabel = null!;
    private LinkLabel revealLink = null!;
    private Button blockButton = null!;
    private Button cancelButton = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.questionLabel = new System.Windows.Forms.Label();
            this.detailLabel = new System.Windows.Forms.Label();
            this.pathLabel = new System.Windows.Forms.Label();
            this.revealLink = new System.Windows.Forms.LinkLabel();
            this.blockButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // questionLabel
            // 
            this.questionLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.questionLabel.Location = new System.Drawing.Point(14, 14);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(392, 42);
            this.questionLabel.TabIndex = 1;
            this.questionLabel.Text = "Remove this?";
            // 
            // detailLabel
            // 
            this.detailLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.detailLabel.Location = new System.Drawing.Point(14, 60);
            this.detailLabel.Name = "detailLabel";
            this.detailLabel.Size = new System.Drawing.Size(392, 42);
            this.detailLabel.TabIndex = 2;
            // 
            // pathLabel
            // 
            this.pathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathLabel.AutoEllipsis = true;
            this.pathLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.pathLabel.Location = new System.Drawing.Point(14, 104);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(392, 20);
            this.pathLabel.TabIndex = 4;
            this.pathLabel.Visible = false;
            // 
            // revealLink
            // 
            this.revealLink.AutoSize = true;
            this.revealLink.Location = new System.Drawing.Point(14, 104);
            this.revealLink.Name = "revealLink";
            this.revealLink.Size = new System.Drawing.Size(126, 15);
            this.revealLink.TabIndex = 3;
            this.revealLink.TabStop = true;
            this.revealLink.Text = "What exactly changes?";
            this.revealLink.Click += new System.EventHandler(this.RevealLink_Click);
            // 
            // blockButton
            // 
            this.blockButton.Location = new System.Drawing.Point(206, 130);
            this.blockButton.Name = "blockButton";
            this.blockButton.Size = new System.Drawing.Size(96, 26);
            this.blockButton.TabIndex = 5;
            this.blockButton.Text = "Block it";
            this.blockButton.Click += new System.EventHandler(this.BlockButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(310, 130);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(96, 26);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            // 
            // ConfirmCard
            // 
            this.AcceptButton = this.blockButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(420, 168);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.blockButton);
            this.Controls.Add(this.revealLink);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.detailLabel);
            this.Controls.Add(this.questionLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ConfirmCard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

    }
}
