using System;
using System.Windows.Forms;
using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.StockApi;
using NeuralStocks.Frontend.Controller;
using NeuralStocks.Frontend.UI;

namespace NeuralStocks.Frontend.Launcher
{
    public class NeuralStocksFrontendLauncher : INeuralStocksFrontendLauncher
    {
        private const string DatabaseFileName = "NeuralStocksDatabase.sqlite";
        public IFrontendController FrontendController { get; private set; }
        public MainWindow MainWindow { get; private set; }

        public NeuralStocksFrontendLauncher()
        {
            var stockCommunicator = StockMarketApiCommunicator.Singleton;

            var databaseConnection = new DatabaseConnection(new DatabaseName { Name = DatabaseFileName });
            var databaseCommunicator = new DatabaseCommunicator(databaseConnection);
            FrontendController = new FrontendController(databaseCommunicator);
            MainWindow = new MainWindow(FrontendController);
        }

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var launcher = new NeuralStocksFrontendLauncher();
            Application.Run(launcher.MainWindow);
        }
    }
}