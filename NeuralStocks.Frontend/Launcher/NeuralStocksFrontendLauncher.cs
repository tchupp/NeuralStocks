using System;
using System.Windows.Forms;
using NeuralStocks.Backend.ApiCommunication;
using NeuralStocks.Backend.Database;
using NeuralStocks.Frontend.Controller;
using NeuralStocks.Frontend.UI;

namespace NeuralStocks.Frontend.Launcher
{
    public class NeuralStocksFrontendLauncher : INeuralStocksFrontendLauncher
    {
        public IFrontendController FrontendController { get; private set; }
        public MainWindow MainWindow { get; private set; }

        public NeuralStocksFrontendLauncher()
        {
            var stockCommunicator = new StockMarketApiCommunicator(
                StockMarketApi.Singleton, TimestampParser.Singleton);

            FrontendController = new FrontendController(stockCommunicator,
                DataTableFactory.Factory, DatabaseCommunicator.Singleton);
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