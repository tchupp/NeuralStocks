using System.Data.SQLite;
using System.Linq;
using NeuralStocks.Backend.ApiCommunication;
using NeuralStocks.Backend.Database;

namespace NeuralStocks.Backend.Controller
{
    public class BackendController : IBackendController
    {
        public IStockMarketApiCommunicator StockCommunicator { get; private set; }
        public IDatabaseCommunicator DatabaseCommunicator { get; private set; }
        public string DatabaseFileName { get; private set; }
        public IBackendTimer BackendTimer { get; set; }

        public BackendController(IStockMarketApiCommunicator stockCommunicator,
            IDatabaseCommunicator databaseCommunicator,
            string databaseFileName)
        {
            StockCommunicator = stockCommunicator;
            DatabaseCommunicator = databaseCommunicator;
            DatabaseFileName = databaseFileName;
            BackendTimer = new BackendTimer(this);
        }

        public void UpdateCompanyQuotes()
        {
            var databaseConnectionString = "Data Source=" + DatabaseFileName + ";Version=3;";
            var connection = new SQLiteConnection(databaseConnectionString);
            var lookupFromTableList = DatabaseCommunicator.GetQuoteLookupsFromTable(connection);

            var responseList =
                from lookup in lookupFromTableList
                let response = StockCommunicator.QuoteLookup(lookup)
                where response.Timestamp != lookup.Timestamp
                select response;
            foreach (var response in responseList)
            {
                DatabaseCommunicator.UpdateCompanyTimestamp(connection, response);
                DatabaseCommunicator.AddQuoteResponseToTable(connection, response);
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