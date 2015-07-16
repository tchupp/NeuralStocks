using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.DatabaseLayer.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.DatabaseLayer.Tests.StockApi
{
    [TestClass]
    public class CompanyLookupResponseTest : AssertTestClass
    {
        [TestMethod, TestCategory("StockApi")]
        public void TestCompanyLookupResponseConstructedWithCorrectParameters()
        {
            const string expectedSymbol = "NFLX";
            const string expectedName = "Netflix Inc";
            const string expectedExchange = "NASDAQ";

            var response = new CompanyLookupResponse
            {
                Symbol = expectedSymbol,
                Name = expectedName,
                Exchange = expectedExchange
            };

            Assert.AreSame(expectedSymbol, response.Symbol);
            Assert.AreSame(expectedName, response.Name);
            Assert.AreSame(expectedExchange, response.Exchange);
        }
    }
}