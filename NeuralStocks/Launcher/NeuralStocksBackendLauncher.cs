using System;
using System.IO;
using NeuralStocks.ApiCommunication;
using NeuralStocks.Controller;
using NeuralStocks.SqlDatabase;

namespace NeuralStocks.Launcher
{
    public class NeuralStocksBackendLauncher : INeuralStocksBackendLauncher
    {
        private const string DatabaseFileName = "NeuralStocksDatabase.sqlite";
        public ISqlDatabaseSetupManager SetupManager { get; set; }
        public IBackendController BackendController { get; set; }

        public NeuralStocksBackendLauncher()
        {
            SetupManager = new SqlDatabaseSetupManager(SqlDatabaseCommandRunner.Singleton);
            BackendController = new BackendController(new StockMarketApiCommunicator(StockMarketApi.Singleton),
                SqlDatabaseCommandRunner.Singleton, DatabaseFileName);
        }

        public void StartBackend()
        {
            if (!File.Exists(DatabaseFileName)) SetupManager.InitializeDatabase(DatabaseFileName);
            BackendController.StartTimer();
        }

        private static void Main(string[] args)
        {
            var launcher = new NeuralStocksBackendLauncher();
            launcher.StartBackend();

            Console.WriteLine("Press the Enter key to exit the program... ");
            Console.ReadLine();
            Console.WriteLine("Terminating the application...");
        }
    }
}