namespace NixThis;

//draws the provider, the key and the two fields only the custom provider needs
partial class AiPage
{
    private System.ComponentModel.IContainer components = null!;
    private Label intro = null!;
    private Label providerLabel = null!;
    private ComboBox providerBox = null!;
    private Label keyLabel = null!;
    private TextBox keyBox = null!;
    private Button testButton = null!;
    private LinkLabel getKeyLink = null!;
    private Label endpointLabel = null!;
    private TextBox endpointBox = null!;
    private Label modelLabel = null!;
    private TextBox modelBox = null!;
    private Label resultLabel = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.intro = new System.Windows.Forms.Label();
            this.providerLabel = new System.Windows.Forms.Label();
            this.providerBox = new System.Windows.Forms.ComboBox();
            this.keyLabel = new System.Windows.Forms.Label();
            this.keyBox = new System.Windows.Forms.TextBox();
            this.testButton = new System.Windows.Forms.Button();
            this.getKeyLink = new System.Windows.Forms.LinkLabel();
            this.endpointLabel = new System.Windows.Forms.Label();
            this.endpointBox = new System.Windows.Forms.TextBox();
            this.modelLabel = new System.Windows.Forms.Label();
            this.modelBox = new System.Windows.Forms.TextBox();
            this.resultLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // intro
            //
            this.intro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.intro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.intro.Location = new System.Drawing.Point(18, 16);
            this.intro.Name = "intro";
            this.intro.Size = new System.Drawing.Size(549, 44);
            this.intro.TabIndex = 1;
            this.intro.Text = "With a key, the Explain button asks an AI what an entry really does. NixThis never " +
    "sends what is on your PC, only the name of the setting.";
            //
            // providerLabel
            //
            this.providerLabel.Location = new System.Drawing.Point(18, 72);
            this.providerLabel.Name = "providerLabel";
            this.providerLabel.Size = new System.Drawing.Size(140, 28);
            this.providerLabel.TabIndex = 2;
            this.providerLabel.Text = "Provider";
            this.providerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // providerBox
            //
            this.providerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.providerBox.Location = new System.Drawing.Point(164, 75);
            this.providerBox.Name = "providerBox";
            this.providerBox.Size = new System.Drawing.Size(240, 23);
            this.providerBox.TabIndex = 3;
            this.providerBox.SelectedIndexChanged += new System.EventHandler(this.ProviderBox_SelectedIndexChanged);
            //
            // keyLabel
            //
            this.keyLabel.Location = new System.Drawing.Point(18, 112);
            this.keyLabel.Name = "keyLabel";
            this.keyLabel.Size = new System.Drawing.Size(140, 28);
            this.keyLabel.TabIndex = 4;
            this.keyLabel.Text = "API key";
            this.keyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // keyBox
            //
            this.keyBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyBox.Location = new System.Drawing.Point(164, 115);
            this.keyBox.Name = "keyBox";
            this.keyBox.Size = new System.Drawing.Size(285, 23);
            this.keyBox.TabIndex = 5;
            this.keyBox.UseSystemPasswordChar = true;
            //
            // testButton
            //
            this.testButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testButton.Location = new System.Drawing.Point(457, 112);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(110, 28);
            this.testButton.TabIndex = 6;
            this.testButton.Text = "Test";
            this.testButton.Click += new System.EventHandler(this.TestButton_Click);
            //
            // getKeyLink
            //
            this.getKeyLink.AutoSize = true;
            this.getKeyLink.Location = new System.Drawing.Point(164, 146);
            this.getKeyLink.Name = "getKeyLink";
            this.getKeyLink.Size = new System.Drawing.Size(130, 15);
            this.getKeyLink.TabIndex = 7;
            this.getKeyLink.TabStop = true;
            this.getKeyLink.Text = "Where do I get a key?";
            this.getKeyLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GetKeyLink_LinkClicked);
            //
            // endpointLabel
            //
            this.endpointLabel.Location = new System.Drawing.Point(18, 182);
            this.endpointLabel.Name = "endpointLabel";
            this.endpointLabel.Size = new System.Drawing.Size(140, 28);
            this.endpointLabel.TabIndex = 8;
            this.endpointLabel.Text = "Address";
            this.endpointLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // endpointBox
            //
            this.endpointBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.endpointBox.Location = new System.Drawing.Point(164, 185);
            this.endpointBox.Name = "endpointBox";
            this.endpointBox.Size = new System.Drawing.Size(403, 23);
            this.endpointBox.TabIndex = 9;
            //
            // modelLabel
            //
            this.modelLabel.Location = new System.Drawing.Point(18, 222);
            this.modelLabel.Name = "modelLabel";
            this.modelLabel.Size = new System.Drawing.Size(140, 28);
            this.modelLabel.TabIndex = 10;
            this.modelLabel.Text = "Model";
            this.modelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // modelBox
            //
            this.modelBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modelBox.Location = new System.Drawing.Point(164, 225);
            this.modelBox.Name = "modelBox";
            this.modelBox.Size = new System.Drawing.Size(403, 23);
            this.modelBox.TabIndex = 11;
            //
            // resultLabel
            //
            this.resultLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.resultLabel.Location = new System.Drawing.Point(18, 264);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(549, 60);
            this.resultLabel.TabIndex = 12;
            //
            // AiPage
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.intro);
            this.Controls.Add(this.providerLabel);
            this.Controls.Add(this.providerBox);
            this.Controls.Add(this.keyLabel);
            this.Controls.Add(this.keyBox);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.getKeyLink);
            this.Controls.Add(this.endpointLabel);
            this.Controls.Add(this.endpointBox);
            this.Controls.Add(this.modelLabel);
            this.Controls.Add(this.modelBox);
            this.Controls.Add(this.resultLabel);
            //the same size the tab strip above these pages already uses
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "AiPage";
            this.Size = new System.Drawing.Size(585, 494);
            this.ResumeLayout(false);
            this.PerformLayout();

    }
}
