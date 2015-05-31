using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.ApiCommunication;

namespace NeuralStocksTests.ApiCommunication
{
    [TestClass]
    public class CompanyLookupResponseTest
    {
        [TestMethod]
        public void TestCompanyLookupResponseDefaultConstructor_SettersWorksCorrectly()
        {
            const string expectedSymbol = "NFLX";
            const string expectedName = "Netflix Inc";
            const string expectedExchange = "NASDAQ";

            var response = new CompanyLookupResponse();

            Assert.AreSame("", response.Symbol);
            Assert.AreSame("", response.Name);
            Assert.AreSame("", response.Exchange);

            response.Symbol = expectedSymbol;
            response.Name = expectedName;
            response.Exchange = expectedExchange;

            Assert.AreSame(expectedSymbol, response.Symbol);
            Assert.AreSame(expectedName, response.Name);
            Assert.AreSame(expectedExchange, response.Exchange);
        }

        [TestMethod]
        public void TestCompanyLookupResponseConstructedWithCorrectParameters()
        {
            const string expectedSymbol = "NFLX";
            const string expectedName = "Netflix Inc";
            const string expectedExchange = "NASDAQ";

            var response = new CompanyLookupResponse(expectedSymbol, expectedName, expectedExchange);

            Assert.AreSame(expectedSymbol, response.Symbol);
            Assert.AreSame(expectedName, response.Name);
            Assert.AreSame(expectedExchange, response.Exchange);
        }
    }
}