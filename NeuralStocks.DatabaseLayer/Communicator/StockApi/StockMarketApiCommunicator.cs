using System.Collections.Generic;
using NeuralStocks.DatabaseLayer.Model.StockApi;
using Newtonsoft.Json;

namespace NeuralStocks.DatabaseLayer.Communicator.StockApi
{
    public class StockMarketApiCommunicator : IStockMarketApiCommunicator
    {
        public static readonly IStockMarketApiCommunicator Singleton = new StockMarketApiCommunicator();
        public IStockMarketApi StockMarketApi { get; set; }
        public ITimestampParser TimestampParser { get; set; }

        private StockMarketApiCommunicator()
        {
            StockMarketApi = StockApi.StockMarketApi.Singleton;
            TimestampParser = StockApi.TimestampParser.Singleton;
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