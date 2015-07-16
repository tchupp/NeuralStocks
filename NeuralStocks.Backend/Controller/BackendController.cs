using System.Linq;
using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.StockApi;

namespace NeuralStocks.Backend.Controller
{
    public class BackendController : IBackendController
    {
        public IStockMarketApiCommunicator StockCommunicator { get; private set; }
        public IDatabaseCommunicator DatabaseCommunicator { get; private set; }
        public IBackendTimer BackendTimer { get; set; }

        public BackendController(IStockMarketApiCommunicator stockCommunicator, IDatabaseCommunicator databaseCommunicator)
        {
            StockCommunicator = stockCommunicator;
            DatabaseCommunicator = databaseCommunicator;
            BackendTimer = new BackendTimer(this);
        }

        public void UpdateCompanyQuotes()
        {
            var lookupFromTableList = DatabaseCommunicator.SelectQuoteLookupList();

            var responseList =
                from lookup in lookupFromTableList
                let response = StockCommunicator.QuoteLookup(lookup)
                where response.Timestamp != lookup.Timestamp
                select response;
            foreach (var response in responseList)
            {
                DatabaseCommunicator.UpdateCompanyTimestamp(response);
                DatabaseCommunicator.InsertQuoteResponseToTable(response);
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