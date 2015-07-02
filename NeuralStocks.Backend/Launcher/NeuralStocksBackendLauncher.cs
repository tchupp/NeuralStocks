using System;
using System.IO;
using NeuralStocks.Backend.Controller;
using NeuralStocks.DatabaseLayer.ApiCommunication;
using NeuralStocks.DatabaseLayer.Database;

namespace NeuralStocks.Backend.Launcher
{
    public class NeuralStocksBackendLauncher : INeuralStocksBackendLauncher
    {
        private const string DatabaseFileName = "NeuralStocksDatabase.sqlite";
        public IDatabaseSetupManager SetupManager { get; set; }
        public IBackendController BackendController { get; set; }
        public IBackendLock BackendLock { get; set; }

        public NeuralStocksBackendLauncher()
        {
            SetupManager = new DatabaseSetupManager(DatabaseCommunicator.Singleton);

            var stockCommunicator = new StockMarketApiCommunicator(
                StockMarketApi.Singleton, TimestampParser.Singleton);
            BackendController = new BackendController(
                stockCommunicator, DatabaseCommunicator.Singleton, DatabaseFileName);

            BackendLock = new BackendLock(58525);
        }

        public void StartBackend()
        {
            if (BackendLock.Lock())
            {
                if (!File.Exists(DatabaseFileName)) SetupManager.InitializeDatabase(DatabaseFileName);
                BackendController.StartTimer();
                Console.WriteLine("Backend Started");
            }
            else
            {
                Console.WriteLine("Backend already started. Application is locked");
            }
        }

        private static void Main(string[] args)
        {
            var launcher = new NeuralStocksBackendLauncher();
            launcher.StartBackend();

            Console.WriteLine("Press Enter to exit the program... ");
            Console.ReadLine();
            Console.WriteLine("Terminating the application...");
        }
    }
}