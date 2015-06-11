using System.IO;
using System.Net;
using System.Text;

namespace NeuralStocks.ApiCommunication
{
    public class StockMarketApi : IStockMarketApi
    {
        public static StockMarketApi Singleton = new StockMarketApi();

        private StockMarketApi()
        {
        }

        public string CompanyLookup(string company)
        {
            var url = "http://dev.markitondemand.com/Api/v2/Lookup/jsonp?input=" + company;
            return ReadUrl(url);
        }

        public string QuoteLookup(string company)
        {
            var url = "http://dev.markitondemand.com/Api/v2/Quote/jsonp?symbol=" + company;
            return ReadUrl(url);
        }

        public string RangeLookup(string parameters)
        {
            return "";
        }

        private static string ReadUrl(string url)
        {
            var request = WebRequest.Create(url);
            using (var response = request.GetResponse())
            {
                var responseStream = response.GetResponseStream();
                if (responseStream == null) return "";

                using (var streamReader = new StreamReader(responseStream, Encoding.UTF8))
                {
                    var read = streamReader.ReadToEnd();
                    read = read.Remove(0, 18);
                    read = read.Remove(read.Length - 1, 1);
                    return read;
                }
            }
        }
    }
}