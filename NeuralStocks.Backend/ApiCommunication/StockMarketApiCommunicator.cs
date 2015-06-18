using System.Collections.Generic;
using Newtonsoft.Json;

namespace NeuralStocks.Backend.ApiCommunication
{
    public class StockMarketApiCommunicator : IStockMarketApiCommunicator
    {
        public IStockMarketApi StockMarketApi { get; private set; }
        public ITimestampParser TimestampParser { get; private set; }

        public StockMarketApiCommunicator(IStockMarketApi marketApi, ITimestampParser timestampParser)
        {
            StockMarketApi = marketApi;
            TimestampParser = timestampParser;
        }

        public List<CompanyLookupResponse> CompanyLookup(CompanyLookupRequest request)
        {
            var lookup = StockMarketApi.CompanyLookup(request.Company);
            var responses = JsonConvert.DeserializeObject<List<CompanyLookupResponse>>(lookup);
            return responses;
        }

        public QuoteLookupResponse QuoteLookup(QuoteLookupRequest lookupRequest)
        {
            var lookup = StockMarketApi.QuoteLookup(lookupRequest.Company);
            var response = JsonConvert.DeserializeObject<QuoteLookupResponse>(lookup);
            response = TimestampParser.Parse(response);
            return response;
        }
    }
}