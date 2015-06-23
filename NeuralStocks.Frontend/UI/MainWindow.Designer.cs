namespace NeuralStocks.Frontend.UI
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing); 
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainWindowHeaderLabel = new System.Windows.Forms.Label();
            this.mainWindowTabControl = new System.Windows.Forms.TabControl();
            this.summaryTab = new System.Windows.Forms.TabPage();
            this.companyTab = new System.Windows.Forms.TabPage();
            this.currentCompanyPanel = new System.Windows.Forms.Panel();
            this.newCompanyPanel = new System.Windows.Forms.Panel();
            this.addNewCompaniesButton = new System.Windows.Forms.Button();
            this.newCompanySearchButton = new System.Windows.Forms.Button();
            this.newCompanySearchTextBox = new System.Windows.Forms.TextBox();
            this.newCompanySearchLabel = new System.Windows.Forms.Label();
            this.currentCompanySearchLabel = new System.Windows.Forms.Label();
            this.currentCompanySearchTextBox = new System.Windows.Forms.TextBox();
            this.currentCompanySearchButton = new System.Windows.Forms.Button();
            this.currentCompanySearchTableLabel = new System.Windows.Forms.Label();
            this.currentCompanySummaryTableLabel = new System.Windows.Forms.Label();
            this.newCompanySearchTableLabel = new System.Windows.Forms.Label();
            this.turnCollectionOnButton = new System.Windows.Forms.Button();
            this.turnCollectionOffButton = new System.Windows.Forms.Button();
            this.mainWindowMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainWindowTabControl.SuspendLayout();
            this.companyTab.SuspendLayout();
            this.currentCompanyPanel.SuspendLayout();
            this.newCompanyPanel.SuspendLayout();
            this.mainWindowMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainWindowHeaderLabel
            // 
            this.mainWindowHeaderLabel.AutoSize = true;
            this.mainWindowHeaderLabel.Font = new System.Drawing.Font("Vani", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainWindowHeaderLabel.Location = new System.Drawing.Point(12, 24);
            this.mainWindowHeaderLabel.Name = "mainWindowHeaderLabel";
            this.mainWindowHeaderLabel.Size = new System.Drawing.Size(127, 29);
            this.mainWindowHeaderLabel.TabIndex = 0;
            this.mainWindowHeaderLabel.Text = "Nueral Stocks";
            // 
            // mainWindowTabControl
            // 
            this.mainWindowTabControl.Controls.Add(this.summaryTab);
            this.mainWindowTabControl.Controls.Add(this.companyTab);
            this.mainWindowTabControl.Font = new System.Drawing.Font("Vani", 9F);
            this.mainWindowTabControl.Location = new System.Drawing.Point(12, 57);
            this.mainWindowTabControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mainWindowTabControl.Name = "mainWindowTabControl";
            this.mainWindowTabControl.SelectedIndex = 0;
            this.mainWindowTabControl.Size = new System.Drawing.Size(1560, 792);
            this.mainWindowTabControl.TabIndex = 1;
            this.mainWindowTabControl.Tag = "";
            // 
            // summaryTab
            // 
            this.summaryTab.Location = new System.Drawing.Point(4, 31);
            this.summaryTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.summaryTab.Name = "summaryTab";
            this.summaryTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.summaryTab.Size = new System.Drawing.Size(1552, 757);
            this.summaryTab.TabIndex = 0;
            this.summaryTab.Text = "Summary";
            this.summaryTab.UseVisualStyleBackColor = true;
            // 
            // companyTab
            // 
            this.companyTab.Controls.Add(this.currentCompanyPanel);
            this.companyTab.Controls.Add(this.newCompanyPanel);
            this.companyTab.Location = new System.Drawing.Point(4, 31);
            this.companyTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.companyTab.Name = "companyTab";
            this.companyTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.companyTab.Size = new System.Drawing.Size(1552, 757);
            this.companyTab.TabIndex = 1;
            this.companyTab.Text = "Company";
            this.companyTab.UseVisualStyleBackColor = true;
            // 
            // currentCompanyPanel
            // 
            this.currentCompanyPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.currentCompanyPanel.Controls.Add(this.turnCollectionOffButton);
            this.currentCompanyPanel.Controls.Add(this.turnCollectionOnButton);
            this.currentCompanyPanel.Controls.Add(this.currentCompanySummaryTableLabel);
            this.currentCompanyPanel.Controls.Add(this.currentCompanySearchTableLabel);
            this.currentCompanyPanel.Controls.Add(this.currentCompanySearchButton);
            this.currentCompanyPanel.Controls.Add(this.currentCompanySearchTextBox);
            this.currentCompanyPanel.Controls.Add(this.currentCompanySearchLabel);
            this.currentCompanyPanel.Location = new System.Drawing.Point(6, 7);
            this.currentCompanyPanel.Name = "currentCompanyPanel";
            this.currentCompanyPanel.Size = new System.Drawing.Size(1078, 743);
            this.currentCompanyPanel.TabIndex = 4;
            // 
            // newCompanyPanel
            // 
            this.newCompanyPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.newCompanyPanel.Controls.Add(this.newCompanySearchTableLabel);
            this.newCompanyPanel.Controls.Add(this.addNewCompaniesButton);
            this.newCompanyPanel.Controls.Add(this.newCompanySearchButton);
            this.newCompanyPanel.Controls.Add(this.newCompanySearchTextBox);
            this.newCompanyPanel.Controls.Add(this.newCompanySearchLabel);
            this.newCompanyPanel.Location = new System.Drawing.Point(1090, 7);
            this.newCompanyPanel.Name = "newCompanyPanel";
            this.newCompanyPanel.Size = new System.Drawing.Size(452, 743);
            this.newCompanyPanel.TabIndex = 3;
            // 
            // addNewCompaniesButton
            // 
            this.addNewCompaniesButton.Location = new System.Drawing.Point(11, 703);
            this.addNewCompaniesButton.Name = "addNewCompaniesButton";
            this.addNewCompaniesButton.Size = new System.Drawing.Size(88, 23);
            this.addNewCompaniesButton.TabIndex = 6;
            this.addNewCompaniesButton.Text = "Add Selected";
            this.addNewCompaniesButton.UseVisualStyleBackColor = true;
            this.addNewCompaniesButton.Click += new System.EventHandler(this.addNewCompaniesButton_Click);
            // 
            // newCompanySearchButton
            // 
            this.newCompanySearchButton.Location = new System.Drawing.Point(147, 41);
            this.newCompanySearchButton.Name = "newCompanySearchButton";
            this.newCompanySearchButton.Size = new System.Drawing.Size(75, 23);
            this.newCompanySearchButton.TabIndex = 5;
            this.newCompanySearchButton.Text = "Search";
            this.newCompanySearchButton.UseVisualStyleBackColor = true;
            this.newCompanySearchButton.Click += new System.EventHandler(this.newCompanySearchButton_Click);
            // 
            // newCompanySearchTextBox
            // 
            this.newCompanySearchTextBox.Location = new System.Drawing.Point(10, 38);
            this.newCompanySearchTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.newCompanySearchTextBox.Name = "newCompanySearchTextBox";
            this.newCompanySearchTextBox.Size = new System.Drawing.Size(116, 28);
            this.newCompanySearchTextBox.TabIndex = 4;
            // 
            // newCompanySearchLabel
            // 
            this.newCompanySearchLabel.AutoSize = true;
            this.newCompanySearchLabel.Location = new System.Drawing.Point(7, 10);
            this.newCompanySearchLabel.Name = "newCompanySearchLabel";
            this.newCompanySearchLabel.Size = new System.Drawing.Size(131, 22);
            this.newCompanySearchLabel.TabIndex = 3;
            this.newCompanySearchLabel.Text = "New Company Search:";
            // 
            // currentCompanySearchLabel
            // 
            this.currentCompanySearchLabel.AutoSize = true;
            this.currentCompanySearchLabel.Location = new System.Drawing.Point(7, 10);
            this.currentCompanySearchLabel.Name = "currentCompanySearchLabel";
            this.currentCompanySearchLabel.Size = new System.Drawing.Size(104, 22);
            this.currentCompanySearchLabel.TabIndex = 0;
            this.currentCompanySearchLabel.Text = "Company Search:";
            // 
            // currentCompanySearchTextBox
            // 
            this.currentCompanySearchTextBox.Location = new System.Drawing.Point(10, 38);
            this.currentCompanySearchTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.currentCompanySearchTextBox.Name = "currentCompanySearchTextBox";
            this.currentCompanySearchTextBox.Size = new System.Drawing.Size(116, 28);
            this.currentCompanySearchTextBox.TabIndex = 1;
            // 
            // currentCompanySearchButton
            // 
            this.currentCompanySearchButton.Location = new System.Drawing.Point(147, 41);
            this.currentCompanySearchButton.Name = "currentCompanySearchButton";
            this.currentCompanySearchButton.Size = new System.Drawing.Size(75, 23);
            this.currentCompanySearchButton.TabIndex = 6;
            this.currentCompanySearchButton.Text = "Search";
            this.currentCompanySearchButton.UseVisualStyleBackColor = true;
            this.currentCompanySearchButton.Click += new System.EventHandler(this.currentCompanySearchButton_Click);
            // 
            // currentCompanySearchTableLabel
            // 
            this.currentCompanySearchTableLabel.AutoSize = true;
            this.currentCompanySearchTableLabel.Location = new System.Drawing.Point(7, 90);
            this.currentCompanySearchTableLabel.Name = "currentCompanySearchTableLabel";
            this.currentCompanySearchTableLabel.Size = new System.Drawing.Size(119, 22);
            this.currentCompanySearchTableLabel.TabIndex = 7;
            this.currentCompanySearchTableLabel.Text = "Current Companies:";
            // 
            // currentCompanySummaryTableLabel
            // 
            this.currentCompanySummaryTableLabel.AutoSize = true;
            this.currentCompanySummaryTableLabel.Location = new System.Drawing.Point(444, 90);
            this.currentCompanySummaryTableLabel.Name = "currentCompanySummaryTableLabel";
            this.currentCompanySummaryTableLabel.Size = new System.Drawing.Size(166, 22);
            this.currentCompanySummaryTableLabel.TabIndex = 8;
            this.currentCompanySummaryTableLabel.Text = "Current Company Summary:";
            // 
            // newCompanySearchTableLabel
            // 
            this.newCompanySearchTableLabel.AutoSize = true;
            this.newCompanySearchTableLabel.Location = new System.Drawing.Point(7, 90);
            this.newCompanySearchTableLabel.Name = "newCompanySearchTableLabel";
            this.newCompanySearchTableLabel.Size = new System.Drawing.Size(166, 22);
            this.newCompanySearchTableLabel.TabIndex = 9;
            this.newCompanySearchTableLabel.Text = "Current Company Summary:";
            // 
            // turnCollectionOnButton
            // 
            this.turnCollectionOnButton.Location = new System.Drawing.Point(11, 674);
            this.turnCollectionOnButton.Name = "turnCollectionOnButton";
            this.turnCollectionOnButton.Size = new System.Drawing.Size(120, 23);
            this.turnCollectionOnButton.TabIndex = 9;
            this.turnCollectionOnButton.Text = "Turn Collection On";
            this.turnCollectionOnButton.UseVisualStyleBackColor = true;
            this.turnCollectionOnButton.Click += new System.EventHandler(this.turnCollectionOnButton_Click);
            // 
            // turnCollectionOffButton
            // 
            this.turnCollectionOffButton.Location = new System.Drawing.Point(11, 703);
            this.turnCollectionOffButton.Name = "turnCollectionOffButton";
            this.turnCollectionOffButton.Size = new System.Drawing.Size(120, 23);
            this.turnCollectionOffButton.TabIndex = 10;
            this.turnCollectionOffButton.Text = "Turn Collection Off";
            this.turnCollectionOffButton.UseVisualStyleBackColor = true;
            this.turnCollectionOffButton.Click += new System.EventHandler(this.turnCollectionOffButton_Click);
            // 
            // mainWindowMenuStrip
            // 
            this.mainWindowMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainWindowMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainWindowMenuStrip.Name = "mainWindowMenuStrip";
            this.mainWindowMenuStrip.Size = new System.Drawing.Size(1584, 24);
            this.mainWindowMenuStrip.TabIndex = 2;
            this.mainWindowMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 862);
            this.Controls.Add(this.mainWindowTabControl);
            this.Controls.Add(this.mainWindowHeaderLabel);
            this.Controls.Add(this.mainWindowMenuStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mainWindowMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Neural Stocks";
            this.mainWindowTabControl.ResumeLayout(false);
            this.companyTab.ResumeLayout(false);
            this.currentCompanyPanel.ResumeLayout(false);
            this.currentCompanyPanel.PerformLayout();
            this.newCompanyPanel.ResumeLayout(false);
            this.newCompanyPanel.PerformLayout();
            this.mainWindowMenuStrip.ResumeLayout(false);
            this.mainWindowMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mainWindowHeaderLabel;
        private System.Windows.Forms.TabControl mainWindowTabControl;
        private System.Windows.Forms.TabPage summaryTab;
        private System.Windows.Forms.TabPage companyTab;
        private System.Windows.Forms.Panel currentCompanyPanel;
        private System.Windows.Forms.Panel newCompanyPanel;
        private System.Windows.Forms.Button newCompanySearchButton;
        private System.Windows.Forms.TextBox newCompanySearchTextBox;
        private System.Windows.Forms.Label newCompanySearchLabel;
        private System.Windows.Forms.Button addNewCompaniesButton;
        private System.Windows.Forms.Button currentCompanySearchButton;
        private System.Windows.Forms.TextBox currentCompanySearchTextBox;
        private System.Windows.Forms.Label currentCompanySearchLabel;
        private System.Windows.Forms.Label currentCompanySearchTableLabel;
        private System.Windows.Forms.Label currentCompanySummaryTableLabel;
        private System.Windows.Forms.Label newCompanySearchTableLabel;
        private System.Windows.Forms.Button turnCollectionOffButton;
        private System.Windows.Forms.Button turnCollectionOnButton;
        private System.Windows.Forms.MenuStrip mainWindowMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    }
}

