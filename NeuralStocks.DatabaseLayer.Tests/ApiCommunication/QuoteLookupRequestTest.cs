using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.DatabaseLayer.ApiCommunication;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.DatabaseLayer.Tests.ApiCommunication
{
    [TestClass]
    public class QuoteLookupRequestTest : AssertTestClass
    {
        [TestMethod]
        public void TestStockQuoteRequestConstructedWithCorrectCompany()
        {
            const string company1 = "NFLX";
            const string company2 = "AAPL";
            const string timestamp1 = "Tues Jun 16";
            const string timestamp2 = "Wed Jun 17";

            var stockLookupRequest1 = new QuoteLookupRequest(company1, timestamp1);
            var stockLookupRequest2 = new QuoteLookupRequest(company2, timestamp2);

            Assert.AreEqual(company1, stockLookupRequest1.Company);
            Assert.AreEqual(timestamp1, stockLookupRequest1.Timestamp);
            Assert.AreEqual(company2, stockLookupRequest2.Company);
            Assert.AreEqual(timestamp2, stockLookupRequest2.Timestamp);
        }
    }
}