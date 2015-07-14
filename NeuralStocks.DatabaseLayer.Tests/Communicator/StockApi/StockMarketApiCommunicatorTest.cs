using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralStocks.DatabaseLayer.Communicator.StockApi;
using NeuralStocks.DatabaseLayer.Model.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.DatabaseLayer.Tests.Communicator.StockApi
{
    [TestClass]
    public class StockMarketApiCommunicatorTest : AssertTestClass
    {
        [TestMethod, TestCategory("StockApi")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IStockMarketApiCommunicator), typeof (StockMarketApiCommunicator));
        }

        [TestMethod]
        public void TestSingleton()
        {
            AssertPrivateContructor(typeof (StockMarketApiCommunicator));
            Assert.AreSame(StockMarketApiCommunicator.Singleton, StockMarketApiCommunicator.Singleton);
            AssertIsOfTypeAndGet<StockMarketApiCommunicator>(StockMarketApiCommunicator.Singleton);
        }

        [TestMethod, TestCategory("StockApi")]
        public void TestConstructedWithTimestampParserAndStockMarketApi()
        {
            var communicator = AssertIsOfTypeAndGet<StockMarketApiCommunicator>(StockMarketApiCommunicator.Singleton);

            Assert.AreSame(StockMarketApi.Singleton, communicator.StockMarketApi);
            Assert.AreSame(TimestampParser.Singleton, communicator.TimestampParser);
        }

        [TestMethod, TestCategory("StockApi")]
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

            var communicator = AssertIsOfTypeAndGet<StockMarketApiCommunicator>(StockMarketApiCommunicator.Singleton);
            communicator.StockMarketApi = mockApi.Object;

            var response = communicator.CompanyLookup(new CompanyLookupRequest {Company = company});

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

        [TestMethod, TestCategory("StockApi")]
        public void TestQuoteLookup()
        {
            const string expectedStatus = "SUCCESS";
            const string expectedName = "Apple Inc";
            const string expectedSymbol = "AAPL";
            const double expectedLastPrice = 130.23;
            const double expectedChange = -0.0500000000000114;
            const double expectedChangePercent = -0.0383788762665116;
            const string expectedTimestamp = "Fri May 29 15:59:00 UTC-04:00 2015";
            const string expectedParsedTimestamp = "D20150529T15:59:00";
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

            var lookupResponse = new QuoteLookupResponse
            {
                Status = expectedStatus,
                Name = expectedName,
                Symbol = expectedSymbol,
                LastPrice = expectedLastPrice,
                Change = expectedChange,
                ChangePercent = expectedChangePercent,
                Timestamp = expectedParsedTimestamp,
                MarketCap = expectedMarketCap,
                Volume = expectedVolume,
                ChangeYtd = expectedChangeYtd,
                ChangePercentYtd = expectedChangePercentYtd,
                High = expectedHigh,
                Low = expectedLow,
                Open = expectedOpen
            };

            const string company = "AAPL";

            var mockApi = new Mock<IStockMarketApi>();
            var mockTimestampParser = new Mock<ITimestampParser>();
            mockApi.Setup(m => m.QuoteLookup(company)).Returns(expectedResponse);
            mockTimestampParser.Setup(m => m.Parse(It.Is<QuoteLookupResponse>(
                r => r.Timestamp == expectedTimestamp))).Returns(lookupResponse);

            var communicator = AssertIsOfTypeAndGet<StockMarketApiCommunicator>(StockMarketApiCommunicator.Singleton);
            communicator.StockMarketApi = mockApi.Object;
            communicator.TimestampParser = mockTimestampParser.Object;

            var response = communicator.QuoteLookup(new QuoteLookupRequest {Company = company});
            mockApi.Verify(m => m.QuoteLookup(company), Times.Once());
            mockTimestampParser.VerifyAll();

            Assert.AreEqual(expectedStatus, response.Status);
            Assert.AreEqual(expectedName, response.Name);
            Assert.AreEqual(expectedSymbol, response.Symbol);
            Assert.AreEqual(expectedLastPrice, response.LastPrice, 0.001);
            Assert.AreEqual(expectedChange, response.Change, 0.001);
            Assert.AreEqual(expectedChangePercent, response.ChangePercent, 0.001);
            Assert.AreEqual(expectedParsedTimestamp, response.Timestamp);
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