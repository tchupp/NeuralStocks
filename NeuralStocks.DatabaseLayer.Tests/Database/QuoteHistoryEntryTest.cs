using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NUnit.Framework;

namespace NeuralStocks.DatabaseLayer.Tests.Database
{
    [TestFixture]
    public class QuoteHistoryEntryTest : AssertTestClass
    {
        [Test]
        [Category("Database")]
        public void TestGetsValuesSet()
        {
            const string expectedSymbol = "AAPL";
            const string expectedName = "Apple";
            const string expectedTimestamp = "D20150630T12:54:24";
            const double expectedLastPrice = 125.64;
            const double expectedChange = 125.64;
            const double expectedChangePercent = 125.64;

            var quoteHistoryEntry = new QuoteHistoryEntry
            {
                Name = expectedName,
                Symbol = expectedSymbol,
                Timestamp = expectedTimestamp,
                LastPrice = expectedLastPrice,
                Change = expectedChange,
                ChangePercent = expectedChangePercent
            };

            Assert.AreEqual(expectedName, quoteHistoryEntry.Name);
            Assert.AreEqual(expectedSymbol, quoteHistoryEntry.Symbol);
            Assert.AreEqual(expectedTimestamp, quoteHistoryEntry.Timestamp);
            Assert.AreEqual(expectedLastPrice, quoteHistoryEntry.LastPrice);
            Assert.AreEqual(expectedChange, quoteHistoryEntry.Change);
            Assert.AreEqual(expectedChangePercent, quoteHistoryEntry.ChangePercent);
        }
    }
}