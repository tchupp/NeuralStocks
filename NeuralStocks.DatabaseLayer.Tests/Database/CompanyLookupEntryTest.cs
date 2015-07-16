using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.DatabaseLayer.Tests.Database
{
    [TestClass]
    public class CompanyLookupEntryTest : AssertTestClass
    {
        [TestMethod, TestCategory("Database")]
        public void TestGetsValuesSet()
        {
            const string expectedSymbol = "AAPL";
            const string expectedName = "Apple";
            const string expectedFirstDate = "D20150624T10:16:42";
            const string expectedRecentDate = "D20150630T16:47:28";
            const bool expectedCollection = true;

            var companyLookupEntry = new CompanyLookupEntry
            {
                Symbol = expectedSymbol,
                Name = expectedName,
                FirstDate = expectedFirstDate,
                RecentDate = expectedRecentDate,
                Collection = expectedCollection
            };

            Assert.AreEqual(expectedSymbol, companyLookupEntry.Symbol);
            Assert.AreEqual(expectedName, companyLookupEntry.Name);
            Assert.AreEqual(expectedFirstDate, companyLookupEntry.FirstDate);
            Assert.AreEqual(expectedRecentDate, companyLookupEntry.RecentDate);
            Assert.AreEqual(expectedCollection, companyLookupEntry.Collection);
        }
    }
}

