using System.Data;
using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.StockApi;

namespace NeuralStocks.Frontend.Controller
{
    public class FrontendController : IFrontendController
    {
        public IStockMarketApiCommunicator StockCommunicator { get; set; }
        public IDataTableFactory TableFactory { get; set; }
        public IDatabaseCommunicator DatabaseCommunicator { get; private set; }

        public FrontendController(IDatabaseCommunicator databaseCommunicator)
        {
            StockCommunicator = StockMarketApiCommunicator.Singleton;
            TableFactory = DataTableFactory.Factory;
            DatabaseCommunicator = databaseCommunicator;
        }

        public DataTable GetSearchResultsForNewCompany(string company)
        {
            return StockCommunicator.CompanyLookup(company);
        }

        public DataTable GetCompanyLookupTable()
        {
            return DatabaseCommunicator.SelectCompanyLookupTable();
        }

        public DataTable GetSummaryForCurrentCompany(CompanyLookupEntry company)
        {
            return DatabaseCommunicator.SelectCompanyQuoteHistoryTable(company);
        }
    }
}