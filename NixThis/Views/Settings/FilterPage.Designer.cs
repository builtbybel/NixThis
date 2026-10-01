namespace NixThis;

//draws what the filter file holds and the one button that opens it
partial class FilterPage
{
    private System.ComponentModel.IContainer components = null!;
    private Label info = null!;
    private Button openButton = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.info = new System.Windows.Forms.Label();
            this.openButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // info
            // 
            this.info.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.info.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.info.Location = new System.Drawing.Point(18, 16);
            this.info.Name = "info";
            //six lines at the larger font: a count, the sentence below, and the path
            this.info.Size = new System.Drawing.Size(549, 140);
            this.info.TabIndex = 1;
            //the runtime text names the count, the version and the path as well
            this.info.Text = "Everything NixThis knows is in this one file. Swap it and NixThis knows different " +
    "things, no new build and no installer. Changes take effect on the next start.";
            // 
            // openButton
            // 
            this.openButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.openButton.Location = new System.Drawing.Point(447, 168);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(120, 28);
            this.openButton.TabIndex = 2;
            this.openButton.Text = "Open file";
            this.openButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // FilterPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.info);
            this.Controls.Add(this.openButton);
            //the same size the tab strip above these pages already uses
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FilterPage";
            this.Size = new System.Drawing.Size(585, 494);
            this.ResumeLayout(false);

    }
}
