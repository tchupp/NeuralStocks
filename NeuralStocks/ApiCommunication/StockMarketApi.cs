using System.IO;
using System.Net;
using System.Text;

namespace NeuralStocks.ApiCommunication
{
    public class StockMarketApi : IStockMarketApi
    {
        public string CompanyLookup(string company)
        {
            var url = "http://dev.markitondemand.com/Api/v2/Lookup/jsonp?input=" + company;

            var request = WebRequest.Create(url);
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            if (responseStream == null) return "";

            var streamReader = new StreamReader(responseStream, Encoding.UTF8);

            var read = streamReader.ReadToEnd();
            read = read.Remove(0, 19);
            read = read.Remove(read.Length - 2, 2);

            response.Close();
            streamReader.Close();

            return read;
        }

        public string StockQuote(string company)
        {
            var url = "http://dev.markitondemand.com/Api/v2/Quote/jsonp?symbol=" + company;

            var request = WebRequest.Create(url);
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            if (responseStream == null) return "";

            var streamReader = new StreamReader(responseStream, Encoding.UTF8);

            var read = streamReader.ReadToEnd();
            read = read.Remove(0, 19);
            read = read.Remove(read.Length - 2, 2);

            response.Close();
            streamReader.Close();

            return read;
        }

        public string StockRange(string parameters)
        {
            return "";
        }
    }
}