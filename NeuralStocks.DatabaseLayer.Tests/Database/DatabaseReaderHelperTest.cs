using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.DatabaseLayer.Tests.Database
{
    [TestClass]
    public class DatabaseReaderHelperTest : AssertTestClass
    {
        [TestMethod, TestCategory("Database")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IDatabaseReaderHelper), typeof (DatabaseReaderHelper));
        }

        [TestMethod, TestCategory("Database")]
        public void TestSingleton()
        {
            AssertPrivateContructor(typeof (DatabaseReaderHelper));
            Assert.AreSame(DatabaseReaderHelper.Singleton, DatabaseReaderHelper.Singleton);
            AssertIsOfTypeAndGet<DatabaseReaderHelper>(DatabaseReaderHelper.Singleton);
        }

        [TestMethod, TestCategory("Database")]
        public void TestCreateQuoteHistoryTable()
        {
            const int columnCount = 6;
            const int rowCount = 2;
            var names = new[] {"name", "symbol", "timestamp", "lastPrice", "change", "changePercent"};

            var mockReader = new Mock<IDatabaseReader>();
            mockReader.Setup(c => c.Read()).ReturnsInOrder(true, true, false);
            mockReader.Setup(c => c.FieldCount).Returns(columnCount);

            mockReader.Setup(c => c.GetColumnName(0)).Returns(names[0]);
            mockReader.Setup(c => c.GetColumnName(1)).Returns(names[1]);
            mockReader.Setup(c => c.GetColumnName(2)).Returns(names[2]);
            mockReader.Setup(c => c.GetColumnName(3)).Returns(names[3]);
            mockReader.Setup(c => c.GetColumnName(4)).Returns(names[4]);
            mockReader.Setup(c => c.GetColumnName(5)).Returns(names[5]);

            mockReader.Setup(c => c.GetFieldType(0)).Returns(typeof (string));
            mockReader.Setup(c => c.GetFieldType(1)).Returns(typeof (string));
            mockReader.Setup(c => c.GetFieldType(2)).Returns(typeof (string));
            mockReader.Setup(c => c.GetFieldType(3)).Returns(typeof (double));
            mockReader.Setup(c => c.GetFieldType(4)).Returns(typeof (double));
            mockReader.Setup(c => c.GetFieldType(5)).Returns(typeof (double));

            mockReader.Setup(c => c.Field(names[0])).ReturnsInOrder("Apple", "Netflix");
            mockReader.Setup(c => c.Field(names[1])).ReturnsInOrder("AAPL", "NFLX");
            mockReader.Setup(c => c.Field(names[2])).ReturnsInOrder("D20150503", "D20140308");
            mockReader.Setup(c => c.Field(names[3])).ReturnsInOrder(123.4, 567.8);
            mockReader.Setup(c => c.Field(names[4])).ReturnsInOrder(1.23, 4.56);
            mockReader.Setup(c => c.Field(names[5])).ReturnsInOrder(78.9, 89.0);

            var readerHelper = DatabaseReaderHelper.Singleton;
            var quoteHistoryDataTable = readerHelper.CreateQuoteHistoryTable(mockReader.Object);

            Assert.AreEqual(columnCount, quoteHistoryDataTable.Columns.Count);
            Assert.AreEqual(rowCount, quoteHistoryDataTable.Rows.Count);

            Assert.AreEqual("Apple", quoteHistoryDataTable.Rows[0][names[0]]);
            Assert.AreEqual("Netflix", quoteHistoryDataTable.Rows[1][names[0]]);

            Assert.AreEqual("AAPL", quoteHistoryDataTable.Rows[0][names[1]]);
            Assert.AreEqual("NFLX", quoteHistoryDataTable.Rows[1][names[1]]);

            Assert.AreEqual("D20150503", quoteHistoryDataTable.Rows[0][names[2]]);
            Assert.AreEqual("D20140308", quoteHistoryDataTable.Rows[1][names[2]]);

            Assert.AreEqual(123.4, quoteHistoryDataTable.Rows[0][names[3]]);
            Assert.AreEqual(567.8, quoteHistoryDataTable.Rows[1][names[3]]);

            Assert.AreEqual(1.23, quoteHistoryDataTable.Rows[0][names[4]]);
            Assert.AreEqual(4.56, quoteHistoryDataTable.Rows[1][names[4]]);

            Assert.AreEqual(78.9, quoteHistoryDataTable.Rows[0][names[5]]);
            Assert.AreEqual(89.0, quoteHistoryDataTable.Rows[1][names[5]]);
        }

        [TestMethod, TestCategory("Database")]
        public void TestCreateQuoteLookupTable()
        {
            var mockReader = new Mock<IDatabaseReader>();

            var readerHelper = DatabaseReaderHelper.Singleton;
            var quoteLookupDataTable = readerHelper.CreateQuoteLookupTable(mockReader.Object);

            Assert.AreEqual(0, quoteLookupDataTable.Rows.Count);
        }

        [TestMethod, TestCategory("Database")]
        public void TestCreateCompanyLookupTable()
        {
            var mockReader = new Mock<IDatabaseReader>();

            var readerHelper = DatabaseReaderHelper.Singleton;
            var companyLookupDataTable = readerHelper.CreateCompanyLookupTable(mockReader.Object);

            Assert.AreEqual(0, companyLookupDataTable.Rows.Count);
        }
    }
}