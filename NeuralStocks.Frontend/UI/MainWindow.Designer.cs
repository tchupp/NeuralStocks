using System.ComponentModel;
using System.Windows.Forms;

namespace NeuralStocks.Frontend.UI
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.currentCompanySummaryTabelPanel = new System.Windows.Forms.Panel();
            this.currentCompanySummaryTable = new System.Windows.Forms.DataGridView();
            this.currentCompanySearchTablePanel = new System.Windows.Forms.Panel();
            this.currentCompanySearchTable = new System.Windows.Forms.DataGridView();
            this.turnCollectionOffButton = new System.Windows.Forms.Button();
            this.turnCollectionOnButton = new System.Windows.Forms.Button();
            this.currentCompanySummaryTableLabel = new System.Windows.Forms.Label();
            this.currentCompanySearchTableLabel = new System.Windows.Forms.Label();
            this.currentCompanySearchButton = new System.Windows.Forms.Button();
            this.currentCompanySearchTextBox = new System.Windows.Forms.TextBox();
            this.currentCompanySearchLabel = new System.Windows.Forms.Label();
            this.newCompanyPanel = new System.Windows.Forms.Panel();
            this.newCompanySearchTablePanel = new System.Windows.Forms.Panel();
            this.newCompanySearchTable = new System.Windows.Forms.DataGridView();
            this.newCompanySearchTableLabel = new System.Windows.Forms.Label();
            this.addNewCompaniesButton = new System.Windows.Forms.Button();
            this.newCompanySearchButton = new System.Windows.Forms.Button();
            this.newCompanySearchTextBox = new System.Windows.Forms.TextBox();
            this.newCompanySearchLabel = new System.Windows.Forms.Label();
            this.mainWindowTabControl.SuspendLayout();
            this.companyTab.SuspendLayout();
            this.currentCompanyPanel.SuspendLayout();
            this.currentCompanySummaryTabelPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentCompanySummaryTable)).BeginInit();
            this.currentCompanySearchTablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentCompanySearchTable)).BeginInit();
            this.newCompanyPanel.SuspendLayout();
            this.newCompanySearchTablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newCompanySearchTable)).BeginInit();
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
            this.mainWindowHeaderLabel.Text = "Neural Stocks";
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
            this.currentCompanyPanel.Controls.Add(this.currentCompanySummaryTabelPanel);
            this.currentCompanyPanel.Controls.Add(this.currentCompanySearchTablePanel);
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
            // currentCompanySummaryTabelPanel
            // 
            this.currentCompanySummaryTabelPanel.Controls.Add(this.currentCompanySummaryTable);
            this.currentCompanySummaryTabelPanel.Location = new System.Drawing.Point(548, 115);
            this.currentCompanySummaryTabelPanel.Name = "currentCompanySummaryTabelPanel";
            this.currentCompanySummaryTabelPanel.Size = new System.Drawing.Size(500, 553);
            this.currentCompanySummaryTabelPanel.TabIndex = 12;
            // 
            // currentCompanySummaryTable
            // 
            this.currentCompanySummaryTable.AllowUserToAddRows = false;
            this.currentCompanySummaryTable.AllowUserToDeleteRows = false;
            this.currentCompanySummaryTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.currentCompanySummaryTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentCompanySummaryTable.Location = new System.Drawing.Point(0, 0);
            this.currentCompanySummaryTable.Name = "currentCompanySummaryTable";
            this.currentCompanySummaryTable.ReadOnly = true;
            this.currentCompanySummaryTable.Size = new System.Drawing.Size(500, 553);
            this.currentCompanySummaryTable.TabIndex = 1;
            // 
            // currentCompanySearchTablePanel
            // 
            this.currentCompanySearchTablePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.currentCompanySearchTablePanel.Controls.Add(this.currentCompanySearchTable);
            this.currentCompanySearchTablePanel.Location = new System.Drawing.Point(11, 115);
            this.currentCompanySearchTablePanel.Name = "currentCompanySearchTablePanel";
            this.currentCompanySearchTablePanel.Size = new System.Drawing.Size(500, 553);
            this.currentCompanySearchTablePanel.TabIndex = 11;
            // 
            // currentCompanySearchTable
            // 
            this.currentCompanySearchTable.AllowUserToAddRows = false;
            this.currentCompanySearchTable.AllowUserToDeleteRows = false;
            this.currentCompanySearchTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.currentCompanySearchTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentCompanySearchTable.Location = new System.Drawing.Point(0, 0);
            this.currentCompanySearchTable.Name = "currentCompanySearchTable";
            this.currentCompanySearchTable.ReadOnly = true;
            this.currentCompanySearchTable.Size = new System.Drawing.Size(498, 551);
            this.currentCompanySearchTable.TabIndex = 0;
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
            // currentCompanySummaryTableLabel
            // 
            this.currentCompanySummaryTableLabel.AutoSize = true;
            this.currentCompanySummaryTableLabel.Location = new System.Drawing.Point(544, 90);
            this.currentCompanySummaryTableLabel.Name = "currentCompanySummaryTableLabel";
            this.currentCompanySummaryTableLabel.Size = new System.Drawing.Size(166, 22);
            this.currentCompanySummaryTableLabel.TabIndex = 8;
            this.currentCompanySummaryTableLabel.Text = "Current Company Summary:";
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
            // currentCompanySearchTextBox
            // 
            this.currentCompanySearchTextBox.Location = new System.Drawing.Point(10, 38);
            this.currentCompanySearchTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.currentCompanySearchTextBox.Name = "currentCompanySearchTextBox";
            this.currentCompanySearchTextBox.Size = new System.Drawing.Size(116, 28);
            this.currentCompanySearchTextBox.TabIndex = 1;
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
            // newCompanyPanel
            // 
            this.newCompanyPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.newCompanyPanel.Controls.Add(this.newCompanySearchTablePanel);
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
            // newCompanySearchTablePanel
            // 
            this.newCompanySearchTablePanel.Controls.Add(this.newCompanySearchTable);
            this.newCompanySearchTablePanel.Location = new System.Drawing.Point(8, 115);
            this.newCompanySearchTablePanel.Name = "newCompanySearchTablePanel";
            this.newCompanySearchTablePanel.Size = new System.Drawing.Size(436, 553);
            this.newCompanySearchTablePanel.TabIndex = 13;
            // 
            // newCompanySearchTable
            // 
            this.newCompanySearchTable.AllowUserToAddRows = false;
            this.newCompanySearchTable.AllowUserToDeleteRows = false;
            this.newCompanySearchTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.newCompanySearchTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newCompanySearchTable.Location = new System.Drawing.Point(0, 0);
            this.newCompanySearchTable.Name = "newCompanySearchTable";
            this.newCompanySearchTable.ReadOnly = true;
            this.newCompanySearchTable.Size = new System.Drawing.Size(436, 553);
            this.newCompanySearchTable.TabIndex = 1;
            // 
            // newCompanySearchTableLabel
            // 
            this.newCompanySearchTableLabel.AutoSize = true;
            this.newCompanySearchTableLabel.Location = new System.Drawing.Point(7, 90);
            this.newCompanySearchTableLabel.Name = "newCompanySearchTableLabel";
            this.newCompanySearchTableLabel.Size = new System.Drawing.Size(148, 22);
            this.newCompanySearchTableLabel.TabIndex = 9;
            this.newCompanySearchTableLabel.Text = "New Company Summary:";
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
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1584, 862);
            this.Controls.Add(this.mainWindowTabControl);
            this.Controls.Add(this.mainWindowHeaderLabel);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Neural Stocks";
            this.mainWindowTabControl.ResumeLayout(false);
            this.companyTab.ResumeLayout(false);
            this.currentCompanyPanel.ResumeLayout(false);
            this.currentCompanyPanel.PerformLayout();
            this.currentCompanySummaryTabelPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.currentCompanySummaryTable)).EndInit();
            this.currentCompanySearchTablePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.currentCompanySearchTable)).EndInit();
            this.newCompanyPanel.ResumeLayout(false);
            this.newCompanyPanel.PerformLayout();
            this.newCompanySearchTablePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.newCompanySearchTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label mainWindowHeaderLabel;
        private TabControl mainWindowTabControl;
        private TabPage summaryTab;
        private TabPage companyTab;
        private Panel currentCompanyPanel;
        private Panel newCompanyPanel;
        private Button newCompanySearchButton;
        private TextBox newCompanySearchTextBox;
        private Label newCompanySearchLabel;
        private Button addNewCompaniesButton;
        private Button currentCompanySearchButton;
        private TextBox currentCompanySearchTextBox;
        private Label currentCompanySearchLabel;
        private Label currentCompanySearchTableLabel;
        private Label currentCompanySummaryTableLabel;
        private Label newCompanySearchTableLabel;
        private Button turnCollectionOffButton;
        private Button turnCollectionOnButton;
        private Panel currentCompanySummaryTabelPanel;
        private Panel currentCompanySearchTablePanel;
        private DataGridView currentCompanySearchTable;
        private Panel newCompanySearchTablePanel;
        private DataGridView currentCompanySummaryTable;
        private DataGridView newCompanySearchTable;
    }
}

