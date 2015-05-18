using System;
using System.IO;
using System.Net;
using System.Text;

namespace NeuralStocks.ApiCommunication
{
    public class StocksApiCommunicator : IStocksApiCommunicator
    {
        public void GetResponse()
        {
            const string uriString =
                "http://dev.markitondemand.com/Api/v2/Quote/jsonp?symbol=AAPL";
            var request = WebRequest.Create(uriString);

            var response = request.GetResponse();

            Console.WriteLine("Content length is {0}", response.ContentLength);
            Console.WriteLine("Content type is {0}", response.ContentType);

            var responseStream = response.GetResponseStream();

            if (responseStream == null) return;

            var streamReader = new StreamReader(responseStream, Encoding.UTF8);

            Console.WriteLine("Response stream recieved.");
            Console.WriteLine(streamReader.ReadToEnd());

            response.Close();
            streamReader.Close();
        }
    }
}