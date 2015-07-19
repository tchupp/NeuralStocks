using System;
using System.IO;
using NeuralStocks.Backend.Controller;
using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.StockApi;

namespace NeuralStocks.Backend.Launcher
{
    public class NeuralStocksBackendLauncher : INeuralStocksBackendLauncher
    {
        private readonly string _databaseFileName = DatabaseConfiguration.FullDatabaseFileName;
        public IDatabaseSetupManager SetupManager { get; set; }
        public IBackendController BackendController { get; set; }
        public IBackendLock BackendLock { get; set; }

        public NeuralStocksBackendLauncher()
        {
            var databaseConnection = new DatabaseConnection(new DatabaseName {Name = _databaseFileName});
            var databaseCommunicator = new DatabaseCommunicator(databaseConnection);
            SetupManager = new DatabaseSetupManager(databaseCommunicator);

            var stockCommunicator = StockMarketApiCommunicator.Singleton;
            BackendController = new BackendController(stockCommunicator, databaseCommunicator);

            BackendLock = new BackendLock(58525);
        }

        public void StartBackend()
        {
            if (BackendLock.Lock())
            {
                if (!File.Exists(_databaseFileName)) SetupManager.InitializeDatabase(_databaseFileName);
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