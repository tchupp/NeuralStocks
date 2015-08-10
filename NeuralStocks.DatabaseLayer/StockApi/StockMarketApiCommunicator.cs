using System.Data;
using Newtonsoft.Json;

namespace NeuralStocks.DatabaseLayer.StockApi
{
    public class StockMarketApiCommunicator : IStockMarketApiCommunicator
    {
        public static readonly IStockMarketApiCommunicator Singleton = new StockMarketApiCommunicator();
        public IStockMarketApi StockApi { get; set; }
        public ITimestampParser Parser { get; set; }
        public IJsonConversionHelper Helper { get; set; }

        private StockMarketApiCommunicator()
        {
            StockApi = StockMarketApi.Singleton;
            Parser = TimestampParser.Singleton;
            Helper = JsonConversionHelper.Singleton;
        }

        public DataTable CompanyLookup(string company)
        {
            var lookupJson = StockApi.CompanyLookup(company);
            var lookupTable = Helper.Deserialize<DataTable>(lookupJson);
            return lookupTable;
        }

        public QuoteLookupResponse QuoteLookup(QuoteLookupRequest lookupRequest)
        {
            var lookupJson = StockApi.QuoteLookup(lookupRequest.Company);
            var response = JsonConvert.DeserializeObject<QuoteLookupResponse>(lookupJson);
            response = Parser.Parse(response);
            return response;
        }
    }
}