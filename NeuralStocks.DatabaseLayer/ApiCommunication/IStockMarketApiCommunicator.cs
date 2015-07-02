using System.Collections.Generic;

namespace NeuralStocks.DatabaseLayer.ApiCommunication
{
    public interface IStockMarketApiCommunicator
    {
        List<CompanyLookupResponse> CompanyLookup(CompanyLookupRequest request);
        QuoteLookupResponse QuoteLookup(QuoteLookupRequest lookupRequest);
    }
}