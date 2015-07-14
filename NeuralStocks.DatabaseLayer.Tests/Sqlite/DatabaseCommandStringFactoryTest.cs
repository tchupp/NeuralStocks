using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.DatabaseLayer.Model.Database;
using NeuralStocks.DatabaseLayer.Model.StockApi;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.DatabaseLayer.Tests.Sqlite
{
    [TestClass]
    public class DatabaseCommandStringFactoryTest : AssertTestClass
    {
        [TestMethod, TestCategory("Database")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IDatabaseCommandStringFactory), typeof (DatabaseCommandStringFactory));
        }

        [TestMethod, TestCategory("Database")]
        public void TestSingleton()
        {
            AssertPrivateContructor(typeof (DatabaseCommandStringFactory));
            AssertIsOfTypeAndGet<DatabaseCommandStringFactory>(DatabaseCommandStringFactory.Singleton);
            Assert.AreSame(DatabaseCommandStringFactory.Singleton, DatabaseCommandStringFactory.Singleton);
        }

        [TestMethod, TestCategory("Database")]
        public void TestBuildCreateCompanyLookupTableCommandString()
        {
            const string expectedCommand =
                "CREATE TABLE Company (name TEXT, symbol TEXT, firstDate TEXT, recentDate TEXT, collect INTEGER)";

            var factory = DatabaseCommandStringFactory.Singleton;
            Assert.AreEqual(expectedCommand, factory.BuildCreateCompanyLookupTableCommandString());
        }

        [TestMethod, TestCategory("Database")]
        public void TestBuildCreateQuoteHistoryTableCommandString()
        {
            var company1 = new CompanyLookupResponse {Symbol = "NFLX"};
            var company2 = new CompanyLookupResponse {Symbol = "AAPL"};

            var expectedCommand1 =
                string.Format("CREATE TABLE {0} (name TEXT, symbol TEXT, timestamp TEXT, " +
                              "lastPrice REAL, change REAL, changePercent REAL)", company1.Symbol);
            var expectedCommand2 =
                string.Format("CREATE TABLE {0} (name TEXT, symbol TEXT, timestamp TEXT, " +
                              "lastPrice REAL, change REAL, changePercent REAL)", company2.Symbol);

            var factory = DatabaseCommandStringFactory.Singleton;
            Assert.AreEqual(expectedCommand1, factory.BuildCreateQuoteHistoryTableCommandString(company1));
            Assert.AreEqual(expectedCommand2, factory.BuildCreateQuoteHistoryTableCommandString(company2));
        }

        [TestMethod, TestCategory("Database")]
        public void TestBuildInsertCompanyToLookupTableCommandString()
        {
            var company1 = new CompanyLookupResponse {Symbol = "NFLX", Name = "Netflix"};
            var company2 = new CompanyLookupResponse {Symbol = "AAPL", Name = "Apple"};

            var expectedCommand1 =
                string.Format("INSERT INTO Company VALUES ('{0}', '{1}', 'null', 'null', 1)",
                    company1.Name, company1.Symbol);
            var expectedCommand2 =
                string.Format("INSERT INTO Company VALUES ('{0}', '{1}', 'null', 'null', 1)",
                    company2.Name, company2.Symbol);

            var factory = DatabaseCommandStringFactory.Singleton;
            Assert.AreEqual(expectedCommand1, factory.BuildInsertCompanyToLookupTableCommandString(company1));
            Assert.AreEqual(expectedCommand2, factory.BuildInsertCompanyToLookupTableCommandString(company2));
        }

        [TestMethod, TestCategory("Database")]
        public void TestBuildInsertQuoteToHistoryTableCommandString()
        {
            var response1 = new QuoteLookupResponse
            {
                Name = "Apple Inc",
                Symbol = "AAPL",
                Timestamp = "Jun 4 00:00:00",
                LastPrice = 127.1f,
                Change = 0.52f,
                ChangePercent = 0.0062f
            };
            var response2 = new QuoteLookupResponse
            {
                Name = "Netflix",
                Symbol = "NFLX",
                Timestamp = "Jun 1 12:51:43",
                LastPrice = 293.4f,
                Change = 0.3f,
                ChangePercent = 0.0045f
            };

            var expectedCommand1 =
                string.Format(
                    "INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', {4}, {5}, {6})",
                    response1.Symbol, response1.Name, response1.Symbol, response1.Timestamp,
                    response1.LastPrice, response1.Change, response1.ChangePercent);
            var expectedCommand2 =
                string.Format(
                    "INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', {4}, {5}, {6})",
                    response2.Symbol, response2.Name, response2.Symbol, response2.Timestamp,
                    response2.LastPrice, response2.Change, response2.ChangePercent);

            var factory = DatabaseCommandStringFactory.Singleton;
            Assert.AreEqual(expectedCommand1, factory.BuildInsertQuoteToHistoryTableCommandString(response1));
            Assert.AreEqual(expectedCommand2, factory.BuildInsertQuoteToHistoryTableCommandString(response2));
        }

        [TestMethod, TestCategory("Database")]
        public void TestBuildUpdateCompanyFirstTimestampCommandString()
        {
            var response1 = new QuoteLookupResponse {Symbol = "AAPL", Timestamp = "D06042012T00:00:00"};
            var response2 = new QuoteLookupResponse {Symbol = "NFLX", Timestamp = "D06012012T12:51:43"};

            var expectedCommand1 =
                string.Format(
                    "UPDATE Company SET firstDate = '{0}' WHERE Symbol = '{1}' AND firstDate = 'null'",
                    response1.Timestamp, response1.Symbol);
            var expectedCommand2 =
                string.Format(
                    "UPDATE Company SET firstDate = '{0}' WHERE Symbol = '{1}' AND firstDate = 'null'",
                    response2.Timestamp, response2.Symbol);

            var factory = DatabaseCommandStringFactory.Singleton;
            Assert.AreEqual(expectedCommand1, factory.BuildUpdateCompanyFirstDateCommandString(response1));
            Assert.AreEqual(expectedCommand2, factory.BuildUpdateCompanyFirstDateCommandString(response2));
        }

        [TestMethod, TestCategory("Database")]
        public void TestBuildUpdateCompanyRecentTimestampCommandString()
        {
            var response1 = new QuoteLookupResponse {Symbol = "AAPL", Timestamp = "D06042012T00:00:00"};
            var response2 = new QuoteLookupResponse {Symbol = "NFLX", Timestamp = "D06012012T12:51:43"};

            var expectedCommand1 =
                string.Format(
                    "UPDATE Company SET recentDate = '{0}' WHERE Symbol = '{1}'",
                    response1.Timestamp, response1.Symbol);
            var expectedCommand2 =
                string.Format(
                    "UPDATE Company SET recentDate = '{0}' WHERE Symbol = '{1}'",
                    response2.Timestamp, response2.Symbol);

            var factory = DatabaseCommandStringFactory.Singleton;
            Assert.AreEqual(expectedCommand1, factory.BuildUpdateCompanyRecentTimestampCommandString(response1));
            Assert.AreEqual(expectedCommand2, factory.BuildUpdateCompanyRecentTimestampCommandString(response2));
        }

        [TestMethod, TestCategory("Database")]
        public void TestBuildSelectAllCompaniesFromLookupTableCommandString()
        {
            const string expectedCommand = "SELECT * FROM Company";

            var factory = DatabaseCommandStringFactory.Singleton;
            Assert.AreEqual(expectedCommand, factory.BuildSelectAllCompaniesFromLookupTableCommandString());
        }

        [TestMethod, TestCategory("Database")]
        public void TestBuildSelectAllQuotesFromHistoryTableCommandString()
        {
            var company1 = new CompanyLookupEntry {Symbol = "NFLX"};
            var company2 = new CompanyLookupEntry {Symbol = "AAPL"};

            var expectedCommand1 =
                string.Format("SELECT * FROM {0}", company1.Symbol);
            var expectedCommand2 =
                string.Format("SELECT * FROM {0}", company2.Symbol);

            var factory = DatabaseCommandStringFactory.Singleton;
            Assert.AreEqual(expectedCommand1, factory.BuildSelectAllQuotesFromHistoryTableCommandString(company1));
            Assert.AreEqual(expectedCommand2, factory.BuildSelectAllQuotesFromHistoryTableCommandString(company2));
        }
    }
}