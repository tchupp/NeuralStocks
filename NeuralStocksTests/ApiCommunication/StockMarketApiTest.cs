using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.ApiCommunication;

namespace NeuralStocksTests.ApiCommunication
{
    [TestClass]
    public class StockMarketApiTest
    {
        [TestMethod]
        public void TestCompanyLookup()
        {
            const string expectedLookupNetflix = "{\"Symbol\":\"NFLX\",\"Name\":\"Netflix Inc\",\"Exchange\":\"NASDAQ\"}";

            var stockMarketApi = new StockMarketApi();

            var lookup = stockMarketApi.CompanyLookup("NFLX");

            Assert.AreEqual(expectedLookupNetflix, lookup);
        }

        [TestMethod]
        public void TestStockQuote()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestStockRange()
        {
            Assert.Fail();
        }
    }
}
