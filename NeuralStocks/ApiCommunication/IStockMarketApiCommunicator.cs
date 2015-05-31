using System.Collections.Generic;

namespace NeuralStocks.ApiCommunication
{
    public interface IStockMarketApiCommunicator
    {
        List<CompanyLookupResponse> CompanyLookup(CompanyLookupRequest request);
        QuoteLookupResponse QuoteLookup(QuoteLookupRequest lookupRequest);
    }
}