using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.ApiCommunication;

namespace NeuralStocksTests.ApiCommunication
{
    [TestClass]
    public class QuoteLookupResponseTest
    {
        [TestMethod]
        public void TestQuoteLookupResponseConstructorHasDefaultProperties()
        {
            var response = new QuoteLookupResponse();

            Assert.AreEqual("", response.Name);
            Assert.AreEqual("", response.Symbol);
            Assert.AreEqual(0f, response.LastPrice);
            Assert.AreEqual(0f, response.Change);
            Assert.AreEqual(0f, response.ChangePercent);
            Assert.AreEqual("", response.Timestamp);
            Assert.AreEqual(0f, response.MsDate);
            Assert.AreEqual(0f, response.MarketCap);
            Assert.AreEqual(0f, response.Volume);
            Assert.AreEqual(0f, response.ChangeYtd);
            Assert.AreEqual(0f, response.ChangePercentYtd);
            Assert.AreEqual(0f, response.High);
            Assert.AreEqual(0f, response.Low);
            Assert.AreEqual(0f, response.Open);
        }

        [TestMethod]
        public void TestQuoteLookupResponsePropertiesAreSetCorrectly()
        {
            var response = new QuoteLookupResponse();

            const string expectedStatus = "SUCCESS";
            const string expectedName = "Netflix Inc";
            const string expectedSymbol = "NFLX";
            const float expectedLastPrice = 524.49f;
            const float expectedChange = 15.6f;
            const float expectedChangePercent = 3.0655f;
            const string expectedTimestamp = "Wed Oct 23 13:39:19";
            const float expectedMsDate = 41570.57f;
            const float expectedMarketCap = 476497591530f;
            const float expectedVolume = 397562f;
            const float expectedChangeYtd = 532.1729f;
            const float expectedChangePercentYtd = -1.443685f;
            const float expectedHigh = 52499f;
            const float expectedLow = 519.175f;
            const float expectedOpen = 519.175f;

            response.Status = expectedStatus;
            response.Name = expectedName;
            response.Symbol = expectedSymbol;
            response.LastPrice = expectedLastPrice;
            response.Change = expectedChange;
            response.ChangePercent = expectedChangePercent;
            response.Timestamp = expectedTimestamp;
            response.MsDate = expectedMsDate;
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
            Assert.AreEqual(expectedMsDate, response.MsDate);
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