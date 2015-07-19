using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace NeuralStocks.DatabaseLayer.Tests.Database
{
    [TestFixture]
    public class CompanyLookupEntryTest : AssertTestClass
    {
        [Test]
        [Category("Database")]
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