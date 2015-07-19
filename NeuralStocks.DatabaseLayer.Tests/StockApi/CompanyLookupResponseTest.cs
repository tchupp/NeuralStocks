using NeuralStocks.DatabaseLayer.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace NeuralStocks.DatabaseLayer.Tests.StockApi
{
    [TestFixture]
    public class CompanyLookupResponseTest : AssertTestClass
    {
        [Test]
        [Category("StockApi")]
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