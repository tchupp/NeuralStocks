using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.DatabaseLayer.Tests.Database
{
    [TestClass]
    public class DatabaseCommunicatorTest : AssertTestClass
    {
        [TestMethod, TestCategory("Database")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(
                typeof (IDatabaseCommunicator), typeof (DatabaseCommunicator));
        }

        [TestMethod, TestCategory("Database")]
        public void TestGetsDatabaseConnection()
        {
            var mockConnection = new Mock<IDatabaseConnection>();

            var connection = mockConnection.Object;
            var communicator = new DatabaseCommunicator(connection);

            Assert.AreSame(connection, communicator.Connection);
        }

        [TestMethod, TestCategory("Database")]
        public void TestGetsDatabaseConnectionStringFactory()
        {
            var communicator = new DatabaseCommunicator(null);

            Assert.AreSame(DatabaseCommandStringFactory.Singleton, communicator.Factory);
        }

        [TestMethod, TestCategory("Database")]
        public void TestCreateCompanyTable_CallsExecuteNonQuery_WithCorrectCommandString()
        {
            var mockFactory = new Mock<IDatabaseCommandStringFactory>();
            var mockConnection = new Mock<IDatabaseConnection>(MockBehavior.Strict);
            var mockCommand = new Mock<IDatabaseCommand>(MockBehavior.Strict);
            var seq = new MockSequence();

            const string createCompanyTable = "createCompanyTable";
            mockFactory.Setup(f => f.BuildCreateCompanyLookupTableCommandString()).Returns(createCompanyTable);

            mockConnection.InSequence(seq)
                .Setup(c => c.CreateCommand(createCompanyTable)).Returns(mockCommand.Object);
            mockConnection.InSequence(seq).Setup(c => c.Open());
            mockCommand.InSequence(seq).Setup(c => c.ExecuteNonQuery());
            mockConnection.InSequence(seq).Setup(c => c.Close());

            var communicator = new DatabaseCommunicator(mockConnection.Object) {Factory = mockFactory.Object};

            communicator.CreateCompanyTable();

            mockFactory.VerifyAll();
            mockConnection.VerifyAll();
            mockCommand.VerifyAll();
        }

        [TestMethod, TestCategory("Database")]
        public void TestInsertCompanyToTable_CallsExecuteNonQuery_WithInsertToLookup_AndCreateHistoryTable()
        {
            var mockFactory = new Mock<IDatabaseCommandStringFactory>();
            var mockConnection = new Mock<IDatabaseConnection>(MockBehavior.Strict);
            var mockCommand1 = new Mock<IDatabaseCommand>(MockBehavior.Strict);
            var mockCommand2 = new Mock<IDatabaseCommand>(MockBehavior.Strict);
            var seq = new MockSequence();

            var response = new CompanyLookupResponse();

            const string createQuoteTable = "createQuoteTable";
            const string insertCompanyLookup = "insertCompanyLookup";
            mockFactory.Setup(f => f.BuildCreateQuoteHistoryTableCommandString(response)).Returns(createQuoteTable);
            mockFactory.Setup(f => f.BuildInsertCompanyToLookupTableCommandString(response))
                .Returns(insertCompanyLookup);

            mockConnection.InSequence(seq)
                .Setup(c => c.CreateCommand(createQuoteTable)).Returns(mockCommand1.Object);
            mockConnection.InSequence(seq)
                .Setup(c => c.CreateCommand(insertCompanyLookup)).Returns(mockCommand2.Object);

            mockConnection.InSequence(seq).Setup(c => c.Open());
            mockCommand1.InSequence(seq).Setup(c => c.ExecuteNonQuery());
            mockCommand2.InSequence(seq).Setup(c => c.ExecuteNonQuery());
            mockConnection.InSequence(seq).Setup(c => c.Close());


            var communicator = new DatabaseCommunicator(mockConnection.Object) {Factory = mockFactory.Object};

            communicator.InsertCompanyToTable(response);

            mockFactory.VerifyAll();
            mockConnection.VerifyAll();
            mockCommand1.VerifyAll();
            mockCommand2.VerifyAll();
        }

        [TestMethod, TestCategory("Database")]
        public void TestInsertCompanyToTable_WritesToConsole()
        {
            var mockWriter = new Mock<TextWriter>();
            Console.SetOut(mockWriter.Object);

            var mockFactory = new Mock<IDatabaseCommandStringFactory>();
            var mockConnection = new Mock<IDatabaseConnection>();
            var mockCommand = new Mock<IDatabaseCommand>();
            var response = new CompanyLookupResponse {Name = "Apple", Symbol = "AAPL"};

            mockConnection.Setup(c => c.CreateCommand(It.IsAny<string>())).Returns(mockCommand.Object);

            var communicator = new DatabaseCommunicator(mockConnection.Object) {Factory = mockFactory.Object};

            mockWriter.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);

            communicator.InsertCompanyToTable(response);

            mockWriter.Verify(
                m => m.WriteLine("Added {0} to company lookup table, and added a quote history table : {1}.",
                    response.Name, response.Symbol), Times.Once);
        }

        [TestMethod, TestCategory("Database")]
        public void TestInsertQuoteResponseToTable()
        {
            var mockFactory = new Mock<IDatabaseCommandStringFactory>();
            var mockConnection = new Mock<IDatabaseConnection>(MockBehavior.Strict);
            var mockCommand = new Mock<IDatabaseCommand>(MockBehavior.Strict);
            var seq = new MockSequence();

            var response = new QuoteLookupResponse();

            const string insertQuote = "insertQuote";
            mockFactory.Setup(f => f.BuildInsertQuoteToHistoryTableCommandString(response)).Returns(insertQuote);

            mockConnection.InSequence(seq).Setup(c => c.CreateCommand(insertQuote)).Returns(mockCommand.Object);
            mockConnection.InSequence(seq).Setup(c => c.Open());
            mockCommand.InSequence(seq).Setup(c => c.ExecuteNonQuery());
            mockConnection.InSequence(seq).Setup(c => c.Close());

            var communicator = new DatabaseCommunicator(mockConnection.Object) {Factory = mockFactory.Object};

            communicator.InsertQuoteResponseToTable(response);

            mockFactory.VerifyAll();
            mockConnection.VerifyAll();
            mockCommand.VerifyAll();
        }

        [TestMethod, TestCategory("Database")]
        public void TestInsertQuoteResponseToTable_WriteToConsole()
        {
            var mockWriter = new Mock<TextWriter>();
            Console.SetOut(mockWriter.Object);

            var mockFactory = new Mock<IDatabaseCommandStringFactory>();
            var mockConnection = new Mock<IDatabaseConnection>();
            var mockCommand = new Mock<IDatabaseCommand>();

            var response = new QuoteLookupResponse {Symbol = "AAPL", Timestamp = "Jun 4 00:00:00", LastPrice = 127.1f};

            mockConnection.Setup(c => c.CreateCommand(It.IsAny<string>())).Returns(mockCommand.Object);

            var communicator = new DatabaseCommunicator(mockConnection.Object) {Factory = mockFactory.Object};

            mockWriter.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);

            communicator.InsertQuoteResponseToTable(response);

            mockWriter.Verify(m => m.WriteLine("Adding Quote: Company: {0}. Time: {1}. Amount: {2}.",
                response.Symbol, response.Timestamp, response.LastPrice), Times.Once);
        }

        [TestMethod, TestCategory("Database")]
        public void TestUpdateCompanyTimestamp_CallsCommands_UpdateFirstDate_AndUpdateRecentDate()
        {
            var mockFactory = new Mock<IDatabaseCommandStringFactory>();
            var mockConnection = new Mock<IDatabaseConnection>(MockBehavior.Strict);
            var mockCommand1 = new Mock<IDatabaseCommand>(MockBehavior.Strict);
            var mockCommand2 = new Mock<IDatabaseCommand>(MockBehavior.Strict);
            var seq = new MockSequence();

            var response = new QuoteLookupResponse();

            const string updateFirstDate = "updateFirstDate";
            const string updateRecentDate = "updateRecentDate";
            mockFactory.Setup(f => f.BuildUpdateCompanyFirstDateCommandString(response))
                .Returns(updateFirstDate);
            mockFactory.Setup(f => f.BuildUpdateCompanyRecentTimestampCommandString(response))
                .Returns(updateRecentDate);


            mockConnection.InSequence(seq)
                .Setup(c => c.CreateCommand(updateFirstDate))
                .Returns(mockCommand1.Object);
            mockConnection.InSequence(seq)
                .Setup(c => c.CreateCommand(updateRecentDate))
                .Returns(mockCommand2.Object);

            mockConnection.InSequence(seq).Setup(c => c.Open());
            mockCommand1.InSequence(seq).Setup(c => c.ExecuteNonQuery());
            mockCommand2.InSequence(seq).Setup(c => c.ExecuteNonQuery());
            mockConnection.InSequence(seq).Setup(c => c.Close());

            var communicator = new DatabaseCommunicator(mockConnection.Object) {Factory = mockFactory.Object};

            communicator.UpdateCompanyTimestamp(response);

            mockFactory.VerifyAll();
            mockConnection.VerifyAll();
            mockCommand1.VerifyAll();
            mockCommand2.VerifyAll();
        }

        [TestMethod, TestCategory("Database")]
        public void TestUpdateCompanyTimestamp_WritesToConsole()
        {
            var mockWriter = new Mock<TextWriter>();
            Console.SetOut(mockWriter.Object);

            var mockFactory = new Mock<IDatabaseCommandStringFactory>();
            var mockConnection = new Mock<IDatabaseConnection>();
            var mockCommand = new Mock<IDatabaseCommand>();

            var response = new QuoteLookupResponse {Name = "Apple", Symbol = "AAPL", Timestamp = "Jun 4 00:00:00"};

            mockConnection.Setup(c => c.CreateCommand(It.IsAny<string>())).Returns(mockCommand.Object);

            var communicator = new DatabaseCommunicator(mockConnection.Object) {Factory = mockFactory.Object};

            mockWriter.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);

            communicator.UpdateCompanyTimestamp(response);

            mockWriter.Verify(m => m.WriteLine("Updating Timestamp: Company: {0}. Time: {1}",
                response.Symbol, response.Timestamp), Times.Once);
        }

        [TestMethod, TestCategory("Database")]
        public void TestSelectCompanyLookupList_ReturnsCorrectCompanyLookupRequestList()
        {
            var mockFactory = new Mock<IDatabaseCommandStringFactory>();
            var mockConnection = new Mock<IDatabaseConnection>();
            var mockCommand = new Mock<IDatabaseCommand>();
            var mockReader = new Mock<IDatabaseReader>();

            const string selectAllLookup = "selectAllLookup";
            mockFactory.Setup(f => f.BuildSelectAllCompaniesFromLookupTableCommandString()).Returns(selectAllLookup);

            mockConnection.Setup(c => c.CreateCommand(selectAllLookup))
                .Returns(mockCommand.Object);
            mockCommand.Setup(c => c.ExecuteReader()).Returns(mockReader.Object);

            mockReader.Setup(r => r.Read()).ReturnsInOrder(true, true, false);
            mockReader.Setup(r => r.Field<string>("symbol")).ReturnsInOrder("AAPL", "NFLX");
            mockReader.Setup(r => r.Field<string>("recentDate")).ReturnsInOrder("20150709", "20150714");

            var communicator = new DatabaseCommunicator(mockConnection.Object) { Factory = mockFactory.Object };
            var lookupList = communicator.SelectQuoteLookupList();

            Assert.AreEqual(2, lookupList.Count);
            Assert.AreEqual("AAPL", lookupList[0].Company);
            Assert.AreEqual("NFLX", lookupList[1].Company);
            Assert.AreEqual("20150709", lookupList[0].Timestamp);
            Assert.AreEqual("20150714", lookupList[1].Timestamp);

            mockFactory.VerifyAll();
            mockConnection.VerifyAll();
            mockCommand.VerifyAll();
            mockReader.VerifyAll();
        }

        [TestMethod, TestCategory("Database")]
        public void TestSelectCompanyLookupEntryList()
        {
            var count = 0;
            var mockFactory = new Mock<IDatabaseCommandStringFactory>();
            var mockConnection = new Mock<IDatabaseConnection>();
            var mockCommand = new Mock<IDatabaseCommand>();
            var mockReader = new Mock<IDatabaseReader>();

            const string selectAllLookup = "selectAllLookup";
            mockFactory.Setup(f => f.BuildSelectAllCompaniesFromLookupTableCommandString()).Returns(selectAllLookup);
            mockConnection.Setup(c => c.CreateCommand(selectAllLookup)).Returns(mockCommand.Object);
            mockCommand.Setup(c => c.ExecuteReader()).Returns(mockReader.Object);
            mockReader.Setup(r => r.Read()).Returns(() => count < 2).Callback(() => count++);
            mockReader.Setup(r => r.Field<string>("name")).Returns("Apple");
            mockReader.Setup(r => r.Field<string>("symbol")).Returns("AAPL");
            mockReader.Setup(r => r.Field<string>("firstDate")).Returns("20150704");
            mockReader.Setup(r => r.Field<string>("recentDate")).Returns("20150709");
            mockReader.Setup(r => r.Field<int>("collect")).Returns(() => count - 1);

            var communicator = new DatabaseCommunicator(mockConnection.Object) {Factory = mockFactory.Object};
            var lookupList = communicator.SelectCompanyLookupEntryList();

            Assert.AreEqual(2, lookupList.Count);
            Assert.AreEqual("Apple", lookupList[0].Name);
            Assert.AreEqual("AAPL", lookupList[0].Symbol);
            Assert.AreEqual(false, lookupList[0].Collection);
            Assert.AreEqual("20150704", lookupList[0].FirstDate);
            Assert.AreEqual("20150709", lookupList[0].RecentDate);

            Assert.AreEqual("Apple", lookupList[1].Name);
            Assert.AreEqual("AAPL", lookupList[1].Symbol);
            Assert.AreEqual(true, lookupList[1].Collection);
            Assert.AreEqual("20150704", lookupList[1].FirstDate);
            Assert.AreEqual("20150709", lookupList[1].RecentDate);

            mockFactory.VerifyAll();
            mockConnection.VerifyAll();
            mockCommand.VerifyAll();
            mockReader.VerifyAll();
        }

        [TestMethod, TestCategory("Database")]
        public void TestSelectQuoteHistoryEntryListForCompany()
        {
            var mockFactory = new Mock<IDatabaseCommandStringFactory>();
            var mockConnection = new Mock<IDatabaseConnection>();
            var mockCommand = new Mock<IDatabaseCommand>();
            var mockReader = new Mock<IDatabaseReader>();
            var entry = new CompanyLookupEntry {Symbol = "AAPL"};

            const string selectAllLookup = "selectAllLookup";
            mockFactory.Setup(f => f.BuildSelectAllQuotesFromHistoryTableCommandString(entry)).Returns(selectAllLookup);
            mockConnection.Setup(c => c.CreateCommand(selectAllLookup)).Returns(mockCommand.Object);
            mockCommand.Setup(c => c.ExecuteReader()).Returns(mockReader.Object);

            mockReader.Setup(r => r.Read()).ReturnsInOrder(true, true, false);
            mockReader.Setup(r => r.Field<string>("name")).Returns("Apple");
            mockReader.Setup(r => r.Field<string>("symbol")).Returns("AAPL");
            mockReader.Setup(r => r.Field<string>("timestamp")).Returns("20150704");
            mockReader.Setup(r => r.Field<double>("lastPrice")).Returns(0.158);
            mockReader.Setup(r => r.Field<double>("change")).Returns(0.86);
            mockReader.Setup(r => r.Field<double>("changePercent")).Returns(0.763);

            var communicator = new DatabaseCommunicator(mockConnection.Object) {Factory = mockFactory.Object};
            var lookupList = communicator.SelectQuoteHistoryEntryList(entry);

            Assert.AreEqual(2, lookupList.Count);
            Assert.AreEqual("Apple", lookupList[0].Name);
            Assert.AreEqual("AAPL", lookupList[0].Symbol);
            Assert.AreEqual("20150704", lookupList[0].Timestamp);
            Assert.AreEqual(0.158, lookupList[0].LastPrice);
            Assert.AreEqual(0.86, lookupList[0].Change);
            Assert.AreEqual(0.763, lookupList[0].ChangePercent);

            Assert.AreEqual("Apple", lookupList[1].Name);
            Assert.AreEqual("AAPL", lookupList[1].Symbol);
            Assert.AreEqual("20150704", lookupList[1].Timestamp);
            Assert.AreEqual(0.158, lookupList[1].LastPrice);
            Assert.AreEqual(0.86, lookupList[1].Change);
            Assert.AreEqual(0.763, lookupList[1].ChangePercent);

            mockFactory.VerifyAll();
            mockConnection.VerifyAll();
            mockCommand.VerifyAll();
            mockReader.VerifyAll();
        }
    }
}