using System;
using System.Windows.Forms;
using NeuralStocks.Frontend.Controller;

namespace NeuralStocks.Frontend.UI
{
    public partial class MainWindow : Form
    {
        public IFrontendController FrontendController { get; private set; }

        public MainWindow(IFrontendController frontendController)
        {
            FrontendController = frontendController;
            InitializeComponent();
        }

        private void currentCompanySearchButton_Click(object sender, EventArgs e)
        {
        }

        private void turnCollectionOnButton_Click(object sender, EventArgs e)
        {
        }

        private void turnCollectionOffButton_Click(object sender, EventArgs e)
        {
        }

        private void newCompanySearchButton_Click(object sender, EventArgs e)
        {
            newCompanySearchTable.DataSource =
                FrontendController.GetSearchResultsForCompany(newCompanySearchTextBox.Text);
        }

        private void addNewCompaniesButton_Click(object sender, EventArgs e)
        {
        }
    }
}