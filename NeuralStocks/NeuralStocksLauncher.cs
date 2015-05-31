using System;
using NeuralStocks.ApiCommunication;

namespace NeuralStocks
{
    public class NeuralStocksLauncher : INeuralStocksLauncher
    {
        private static void Main(string[] args)
        {
            var api = new StockMarketApi();
            var lookup = api.CompanyLookup("AAPL");

            Console.WriteLine(lookup);
        }
    }
}