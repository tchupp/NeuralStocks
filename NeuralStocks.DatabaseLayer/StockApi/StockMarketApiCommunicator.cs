using System.Collections.Generic;
using Newtonsoft.Json;

namespace NeuralStocks.DatabaseLayer.StockApi
{
    public class StockMarketApiCommunicator : IStockMarketApiCommunicator
    {
        public static readonly IStockMarketApiCommunicator Singleton = new StockMarketApiCommunicator();
        public IStockMarketApi StockApi { get; set; }
        public ITimestampParser Parser { get; set; }

        private StockMarketApiCommunicator()
        {
            StockApi = StockMarketApi.Singleton;
            Parser = TimestampParser.Singleton;
        }

        public List<CompanyLookupResponse> CompanyLookup(CompanyLookupRequest request)
        {
            var lookup = StockApi.CompanyLookup(request.Company);
            var responses = JsonConvert.DeserializeObject<List<CompanyLookupResponse>>(lookup);
            return responses;
        }

        public QuoteLookupResponse QuoteLookup(QuoteLookupRequest lookupRequest)
        {
            var lookup = StockApi.QuoteLookup(lookupRequest.Company);
            var response = JsonConvert.DeserializeObject<QuoteLookupResponse>(lookup);
            response = Parser.Parse(response);
            return response;
        }
    }
}