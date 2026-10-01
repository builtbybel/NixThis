namespace NixThis;

//the list of everything NixThis removed, and the one button that puts it back
partial class HistoryForm
{
    private System.ComponentModel.IContainer components = null!;
    private ListView historyList = null!;
    private ColumnHeader whatColumn = null!;
    private ColumnHeader whenColumn = null!;
    private ColumnHeader stateColumn = null!;
    private Button undoButton = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.historyList = new System.Windows.Forms.ListView();
            this.whatColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.whenColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stateColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.undoButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // historyList
            // 
            this.historyList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.historyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.whatColumn,
            this.whenColumn,
            this.stateColumn});
            this.historyList.FullRowSelect = true;
            this.historyList.HideSelection = false;
            this.historyList.Location = new System.Drawing.Point(12, 12);
            this.historyList.Name = "historyList";
            this.historyList.Size = new System.Drawing.Size(536, 240);
            this.historyList.TabIndex = 1;
            this.historyList.UseCompatibleStateImageBehavior = false;
            this.historyList.View = System.Windows.Forms.View.Details;
            this.historyList.SelectedIndexChanged += new System.EventHandler(this.HistoryList_SelectedIndexChanged);
            // 
            // whatColumn
            // 
            this.whatColumn.Text = "What";
            this.whatColumn.Width = 260;
            // 
            // whenColumn
            // 
            this.whenColumn.Text = "When";
            this.whenColumn.Width = 130;
            // 
            // stateColumn
            // 
            this.stateColumn.Text = "State";
            this.stateColumn.Width = 150;
            // 
            // undoButton
            // 
            this.undoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.undoButton.Location = new System.Drawing.Point(428, 262);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(120, 26);
            this.undoButton.TabIndex = 2;
            this.undoButton.Text = "Bring it back";
            this.undoButton.Click += new System.EventHandler(this.UndoButton_Click);
            // 
            // HistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(560, 300);
            this.Controls.Add(this.undoButton);
            this.Controls.Add(this.historyList);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimizeBox = false;
            this.Name = "HistoryForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NixThis - what you removed";
            this.ResumeLayout(false);

    }
}
