using System.Collections.Generic;
using Newtonsoft.Json;

namespace NeuralStocks.ApiCommunication
{
    public class StockMarketApiCommunicator : IStockMarketApiCommunicator
    {
        private readonly IStockMarketApi _marketApi;

        public StockMarketApiCommunicator(IStockMarketApi marketApi)
        {
            _marketApi = marketApi;
        }

        public List<CompanyLookupResponse> CompanyLookup(CompanyLookupRequest request)
        {
            var lookup = _marketApi.CompanyLookup(request.Company);
            var responses = JsonConvert.DeserializeObject<List<CompanyLookupResponse>>(lookup);
            return responses;
        }

        public QuoteLookupResponse QuoteLookup(QuoteLookupRequest lookupRequest)
        {
            var lookup = _marketApi.QuoteLookup(lookupRequest.Company);
            var response = JsonConvert.DeserializeObject<QuoteLookupResponse>(lookup);
            return response;
        }
    }
}