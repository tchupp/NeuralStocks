using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.DatabaseLayer.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.DatabaseLayer.Tests.StockApi
{
    [TestClass]
    public class QuoteLookupResponseTest : AssertTestClass
    {
        [TestMethod, TestCategory("StockApi")]
        public void TestQuoteLookupResponsePropertiesAreSetCorrectly()
        {
            const string expectedStatus = "SUCCESS";
            const string expectedName = "Netflix Inc";
            const string expectedSymbol = "NFLX";
            const double expectedLastPrice = 524.49;
            const double expectedChange = 15.6;
            const double expectedChangePercent = 3.0655;
            const string expectedTimestamp = "Wed Oct 23 13:39:19";
            const double expectedMarketCap = 476497591530;
            const double expectedVolume = 397562;
            const double expectedChangeYtd = 532.1729;
            const double expectedChangePercentYtd = -1.443685;
            const double expectedHigh = 52499;
            const double expectedLow = 519.175;
            const double expectedOpen = 519.175;

            var response = new QuoteLookupResponse
            {
                Status = expectedStatus,
                Name = expectedName,
                Symbol = expectedSymbol,
                LastPrice = expectedLastPrice,
                Change = expectedChange,
                ChangePercent = expectedChangePercent,
                Timestamp = expectedTimestamp,
                MarketCap = expectedMarketCap,
                Volume = expectedVolume,
                ChangeYtd = expectedChangeYtd,
                ChangePercentYtd = expectedChangePercentYtd,
                High = expectedHigh,
                Low = expectedLow,
                Open = expectedOpen
            };

            Assert.AreEqual(expectedStatus, response.Status);
            Assert.AreEqual(expectedName, response.Name);
            Assert.AreEqual(expectedSymbol, response.Symbol);
            Assert.AreEqual(expectedLastPrice, response.LastPrice);
            Assert.AreEqual(expectedChange, response.Change);
            Assert.AreEqual(expectedChangePercent, response.ChangePercent);
            Assert.AreEqual(expectedTimestamp, response.Timestamp);
            Assert.AreEqual(expectedMarketCap, response.MarketCap);
            Assert.AreEqual(expectedVolume, response.Volume);
            Assert.AreEqual(expectedChangeYtd, response.ChangeYtd);
            Assert.AreEqual(expectedChangePercentYtd, response.ChangePercentYtd);
            Assert.AreEqual(expectedHigh, response.High);
            Assert.AreEqual(expectedLow, response.Low);
            Assert.AreEqual(expectedOpen, response.Open);
        }
    }
}