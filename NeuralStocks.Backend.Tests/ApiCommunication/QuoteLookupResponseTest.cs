using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.Backend.ApiCommunication;
using NeuralStocks.Backend.Tests.Testing;

namespace NeuralStocks.Backend.Tests.ApiCommunication
{
    [TestClass]
    public class QuoteLookupResponseTest : AssertTestClass
    {
        [TestMethod]
        public void TestQuoteLookupResponseConstructorHasDefaultProperties()
        {
            var response = new QuoteLookupResponse();

            Assert.AreEqual("", response.Name);
            Assert.AreEqual("", response.Symbol);
            Assert.AreEqual(0, response.LastPrice);
            Assert.AreEqual(0, response.Change);
            Assert.AreEqual(0, response.ChangePercent);
            Assert.AreEqual("", response.Timestamp);
            Assert.AreEqual(0, response.MarketCap);
            Assert.AreEqual(0, response.Volume);
            Assert.AreEqual(0, response.ChangeYtd);
            Assert.AreEqual(0, response.ChangePercentYtd);
            Assert.AreEqual(0, response.High);
            Assert.AreEqual(0, response.Low);
            Assert.AreEqual(0, response.Open);
        }

        [TestMethod]
        public void TestQuoteLookupResponsePropertiesAreSetCorrectly()
        {
            var response = new QuoteLookupResponse();

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

            response.Status = expectedStatus;
            response.Name = expectedName;
            response.Symbol = expectedSymbol;
            response.LastPrice = expectedLastPrice;
            response.Change = expectedChange;
            response.ChangePercent = expectedChangePercent;
            response.Timestamp = expectedTimestamp;
            response.MarketCap = expectedMarketCap;
            response.Volume = expectedVolume;
            response.ChangeYtd = expectedChangeYtd;
            response.ChangePercentYtd = expectedChangePercentYtd;
            response.High = expectedHigh;
            response.Low = expectedLow;
            response.Open = expectedOpen;

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