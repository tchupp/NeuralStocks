using System.IO;
using System.Net;
using System.Text;

namespace NeuralStocks.ApiCommunication
{
    public class StockMarketApiCommunicator : IStockMarketApiCommunicator
    {
        public void GetResponse()
        {
            const string uriString =
                "http://dev.markitondemand.com/Api/v2/Quote/jsonp?symbol=AAPL";

            var request = WebRequest.Create(uriString);

            var response = request.GetResponse();

            var responseStream = response.GetResponseStream();

            if (responseStream == null) return;

            var streamReader = new StreamReader(responseStream, Encoding.UTF8);

            streamReader.ReadToEnd();

            response.Close();
            streamReader.Close();
        }
    }
}