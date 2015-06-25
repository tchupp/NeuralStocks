using System.Data.SQLite;
using System.Linq;
using NeuralStocks.Backend.ApiCommunication;
using NeuralStocks.Backend.SqlDatabase;

namespace NeuralStocks.Backend.Controller
{
    public class BackendController : IBackendController
    {
        public IStockMarketApiCommunicator StockCommunicator { get; private set; }
        public ISqlDatabaseCommandRunner CommandRunner { get; private set; }
        public string DatabaseFileName { get; private set; }
        public IBackendTimer BackendTimer { get; set; }

        public BackendController(IStockMarketApiCommunicator stockCommunicator, ISqlDatabaseCommandRunner commandRunner,
            string databaseFileName)
        {
            StockCommunicator = stockCommunicator;
            CommandRunner = commandRunner;
            DatabaseFileName = databaseFileName;
            BackendTimer = new BackendTimer(this);
        }

        public void UpdateCompanyQuotes()
        {
            var databaseConnectionString = "Data Source=" + DatabaseFileName + ";Version=3;";
            var connection = new SQLiteConnection(databaseConnectionString);
            var lookupsFromTable = CommandRunner.GetQuoteLookupsFromTable(connection);

            var responses =
                from lookup in lookupsFromTable
                let response = StockCommunicator.QuoteLookup(lookup)
                where response.Timestamp != lookup.Timestamp
                select response;
            foreach (var response in responses)
            {
                CommandRunner.UpdateCompanyTimestamp(connection, response);
                CommandRunner.AddQuoteResponseToTable(connection, response);
            }
        }

        public void StartTimer()
        {
            BackendTimer.Start();
        }

        public void Dispose()
        {
            BackendTimer.Stop();
        }
    }
}