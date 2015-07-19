using System;
using System.Net.Http;

namespace NeuralStocks.DatabaseLayer.StockApi
{
    public class StockMarketApi : IStockMarketApi
    {
        public static readonly StockMarketApi Singleton = new StockMarketApi();

        private StockMarketApi()
        {
        }

        public string CompanyLookup(string company)
        {
            var url = GetBaseUrl() + "Lookup/jsonp?input=" + company;
            return ReadUrl(url);
        }

        public string QuoteLookup(string company)
        {
            var url = GetBaseUrl() + "Quote/jsonp?symbol=" + company;
            return ReadUrl(url);
        }

        public string RangeLookup(string parameters)
        {
            return "";
        }

        private static string GetBaseUrl()
        {
            return "http://dev.markitondemand.com/Api/v2/";
        }

        private static string ReadUrl(string url)
        {
            using (var client = new HttpClient())
            {
                var json = client.GetStringAsync(new Uri(url)).Result;
                json = json.Remove(0, 18);
                json = json.Remove(json.Length - 1, 1);
                return json;
            }
        }
    }
}