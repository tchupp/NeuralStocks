using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.ApiCommunication;

namespace NeuralStocksTests.ApiCommunication
{
    [TestClass]
    public class QuoteLookupRequestTest
    {
        [TestMethod]
        public void TestStockQuoteRequestConstructedWithCorrectCompany()
        {
            const string company1 = "NFLX";
            const string company2 = "AAPL";

            var stockLookupRequest1 = new QuoteLookupRequest(company1);
            var stockLookupRequest2 = new QuoteLookupRequest(company2);

            Assert.AreEqual(company1, stockLookupRequest1.Company);
            Assert.AreEqual(company2, stockLookupRequest2.Company);
        }
    }
}