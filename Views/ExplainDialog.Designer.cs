namespace NixThis;

partial class ExplainDialog
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.answerBox = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // answerBox
            // 
            this.answerBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.answerBox.BackColor = System.Drawing.SystemColors.Window;
            this.answerBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.answerBox.Location = new System.Drawing.Point(12, 12);
            this.answerBox.Multiline = true;
            this.answerBox.Name = "answerBox";
            this.answerBox.ReadOnly = true;
            this.answerBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.answerBox.Size = new System.Drawing.Size(456, 180);
            this.answerBox.TabIndex = 0;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(383, 204);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(85, 27);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Close";
            // 
            // ExplainDialog
            // 
            this.AcceptButton = this.closeButton;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(480, 243);
            this.Controls.Add(this.answerBox);
            this.Controls.Add(this.closeButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExplainDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NixThis";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    private System.Windows.Forms.TextBox answerBox;
    private System.Windows.Forms.Button closeButton;
}
