using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralStocks.ApiCommunication;
using NeuralStocksTests.Testing;

namespace NeuralStocksTests.ApiCommunication
{
    [TestClass]
    public class StockMarketApiCommunicatorTest
    {
        [TestMethod]
        public void TestImplementsInterface()
        {
            MoreAssert.ImplementsInterface(typeof (IStockMarketApiCommunicator), typeof (StockMarketApiCommunicator));
        }

        [TestMethod]
        public void TestCompanyLookup()
        {
            const string company = "AAPL";
            const string expectedResponse =
                "[{\"Symbol\":\"AAPL\"," +
                "\"Name\":\"Apple Inc\"," +
                "\"Exchange\":\"NASDAQ\"}," +
                "{\"Symbol\":\"AVSPY\"," +
                "\"Name\":\"AAPL ALPHA INDEX\"," +
                "\"Exchange\":\"NASDAQ\"}," +
                "{\"Symbol\":\"AIX\"," +
                "\"Name\":\"NAS OMX Alpha   AAPL vs. SPY  Settle\"," +
                "\"Exchange\":\"NASDAQ\"}]";

            var mockApi = new Mock<IStockMarketApi>();
            mockApi.Setup(m => m.CompanyLookup(company)).Returns(expectedResponse);

            var communicator = new StockMarketApiCommunicator(mockApi.Object);

            var response = communicator.CompanyLookup(new CompanyLookupRequest(company));

            mockApi.Verify(m => m.CompanyLookup(company), Times.Once());

            Assert.AreEqual(3, response.Count);

            Assert.AreEqual("AAPL", response[0].Symbol);
            Assert.AreEqual("Apple Inc", response[0].Name);
            Assert.AreEqual("NASDAQ", response[0].Exchange);

            Assert.AreEqual("AVSPY", response[1].Symbol);
            Assert.AreEqual("AAPL ALPHA INDEX", response[1].Name);
            Assert.AreEqual("NASDAQ", response[1].Exchange);

            Assert.AreEqual("AIX", response[2].Symbol);
            Assert.AreEqual("NAS OMX Alpha   AAPL vs. SPY  Settle", response[2].Name);
            Assert.AreEqual("NASDAQ", response[2].Exchange);
        }

        [TestMethod]
        public void TestQuoteLookup()
        {
            const string expectedStatus = "SUCCESS";
            const string expectedName = "Apple Inc";
            const string expectedSymbol = "AAPL";
            const double expectedLastPrice = 130.23;
            const double expectedChange = -0.0500000000000114;
            const double expectedChangePercent = -0.0383788762665116;
            const string expectedTimestamp = "Fri May 29 15:59:00 UTC-04:00 2015";
            const double expectedMarketCap = 750258936900;
            const double expectedVolume = 2996541;
            const double expectedChangeYtd = 110.38;
            const double expectedChangePercentYtd = 17.9833303134626;
            const double expectedHigh = 131.45;
            const double expectedLow = 129.9;
            const double expectedOpen = 131.26;

            const string expectedResponse =
                "{\"Status\":\"SUCCESS\"" +
                ",\"Name\":\"Apple Inc\"," +
                "\"Symbol\":\"AAPL\"," +
                "\"LastPrice\":130.23," +
                "\"Change\":-0.0500000000000114," +
                "\"ChangePercent\":-0.0383788762665116," +
                "\"Timestamp\":\"Fri May 29 15:59:00 UTC-04:00 2015\"," +
                "\"MSDate\":42153.6659722222," +
                "\"MarketCap\":750258936900," +
                "\"Volume\":2996541," +
                "\"ChangeYTD\":110.38," +
                "\"ChangePercentYTD\":17.9833303134626," +
                "\"High\":131.45," +
                "\"Low\":129.9," +
                "\"Open\":131.26}";

            const string company = "AAPL";

            var mockApi = new Mock<IStockMarketApi>();
            mockApi.Setup(m => m.QuoteLookup(company)).Returns(expectedResponse);
            var communicator = new StockMarketApiCommunicator(mockApi.Object);

            var response = communicator.QuoteLookup(new QuoteLookupRequest(company));
            mockApi.Verify(m => m.QuoteLookup(company), Times.Once());

            Assert.AreEqual(expectedStatus, response.Status);
            Assert.AreEqual(expectedName, response.Name);
            Assert.AreEqual(expectedSymbol, response.Symbol);
            Assert.AreEqual(expectedLastPrice, response.LastPrice, 0.001);
            Assert.AreEqual(expectedChange, response.Change, 0.001);
            Assert.AreEqual(expectedChangePercent, response.ChangePercent, 0.001);
            Assert.AreEqual(expectedTimestamp, response.Timestamp);
            Assert.AreEqual(expectedMarketCap, response.MarketCap, 0.001);
            Assert.AreEqual(expectedVolume, response.Volume, 0.001);
            Assert.AreEqual(expectedChangeYtd, response.ChangeYtd, 0.001);
            Assert.AreEqual(expectedChangePercentYtd, response.ChangePercentYtd, 0.001);
            Assert.AreEqual(expectedHigh, response.High, 0.001);
            Assert.AreEqual(expectedLow, response.Low, 0.001);
            Assert.AreEqual(expectedOpen, response.Open, 0.001);
        }
    }
}