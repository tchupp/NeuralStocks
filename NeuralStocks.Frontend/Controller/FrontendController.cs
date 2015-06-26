using System.Data;
using NeuralStocks.Backend.ApiCommunication;

namespace NeuralStocks.Frontend.Controller
{
    public class FrontendController : IFrontendController
    {
        public IStockMarketApiCommunicator StockCommunicator { get; private set; }
        public IDataTableFactory TableFactory { get; private set; }

        public FrontendController(IStockMarketApiCommunicator stockCommunicator, IDataTableFactory tableFactory)
        {
            StockCommunicator = stockCommunicator;
            TableFactory = tableFactory;
        }

        public DataTable GetSearchResultsForCompany(string company)
        {
            var lookupRequest = new CompanyLookupRequest(company);
            var lookupResponseList = StockCommunicator.CompanyLookup(lookupRequest);
            var buildCompanySearchTable = TableFactory.BuildCompanySearchTable(lookupResponseList);

            return buildCompanySearchTable;
        }
    }
}