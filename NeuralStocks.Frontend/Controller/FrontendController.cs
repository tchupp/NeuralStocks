using System.Data;
using System.Data.SQLite;
using NeuralStocks.DatabaseLayer.Communicator.Database;
using NeuralStocks.DatabaseLayer.Communicator.StockApi;
using NeuralStocks.DatabaseLayer.Model.Database;
using NeuralStocks.DatabaseLayer.Model.StockApi;

namespace NeuralStocks.Frontend.Controller
{
    public class FrontendController : IFrontendController
    {
        public IStockMarketApiCommunicator StockCommunicator { get; private set; }
        public IDataTableFactory TableFactory { get; private set; }
        public IDatabaseCommunicator DatabaseCommunicator { get; private set; }

        public FrontendController(IStockMarketApiCommunicator stockCommunicator, IDataTableFactory tableFactory,
            IDatabaseCommunicator databaseCommunicator)
        {
            StockCommunicator = stockCommunicator;
            TableFactory = tableFactory;
            DatabaseCommunicator = databaseCommunicator;
        }

        public DataTable GetSearchResultsForNewCompany(string company)
        {
            var lookupRequest = new CompanyLookupRequest {Company = company};
            var lookupResponseList = StockCommunicator.CompanyLookup(lookupRequest);
            var buildCompanySearchTable = TableFactory.BuildNewCompanySearchTable(lookupResponseList);

            return buildCompanySearchTable;
        }

        public DataTable GetSearchResultsForCurrentCompany(CompanyLookupEntry company)
        {
            var quoteHistoryEntryList = DatabaseCommunicator.GetQuoteHistoryEntryList(company);
            var buildCompanySearchTable = TableFactory.BuildCurrentCompanySearchTable(quoteHistoryEntryList);

            return buildCompanySearchTable;
        }
    }
}