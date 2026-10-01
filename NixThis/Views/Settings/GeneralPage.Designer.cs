namespace NixThis;

//draws the one general option as normal movable controls
partial class GeneralPage
{
    private System.ComponentModel.IContainer components = null!;
    private Label languageLabel = null!;
    private ComboBox languageBox = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.languageLabel = new System.Windows.Forms.Label();
            this.languageBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            //
            // languageLabel
            //
            this.languageLabel.Location = new System.Drawing.Point(18, 16);
            this.languageLabel.Name = "languageLabel";
            this.languageLabel.Size = new System.Drawing.Size(140, 28);
            this.languageLabel.TabIndex = 1;
            this.languageLabel.Text = "Language";
            this.languageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // languageBox
            //
            this.languageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageBox.Location = new System.Drawing.Point(164, 19);
            this.languageBox.Name = "languageBox";
            this.languageBox.Size = new System.Drawing.Size(240, 23);
            this.languageBox.TabIndex = 2;
            //
            // GeneralPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.languageLabel);
            this.Controls.Add(this.languageBox);
            //the same size the tab strip above these pages already uses
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "GeneralPage";
            this.Size = new System.Drawing.Size(585, 494);
            this.ResumeLayout(false);

    }
}
