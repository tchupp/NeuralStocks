using System.Collections.Generic;
using System.Data;

namespace NeuralStocks.DatabaseLayer.StockApi
{
    public interface IStockMarketApiCommunicator
    {
        DataTable CompanyLookup(string company);
        QuoteLookupResponse QuoteLookup(QuoteLookupRequest lookupRequest);
    }
}