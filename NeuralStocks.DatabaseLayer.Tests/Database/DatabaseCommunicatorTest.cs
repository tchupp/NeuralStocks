using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Moq;
using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NUnit.Framework;

namespace NeuralStocks.DatabaseLayer.Tests.Database
{
    [TestFixture]
    public class DatabaseCommunicatorTest : AssertTestClass
    {
        [Test]
        [Category("Database")]
        public void TestCompanyLookupTable()
        {
            var mockFactory = new Mock<IDatabaseCommandStringFactory>();
            var mockConnection = new Mock<IDatabaseConnection>(MockBehavior.Strict);
            var mockCommand = new Mock<IDatabaseCommand>(MockBehavior.Strict);
            var mockReader = new Mock<IDatabaseReader>();
            var mockReaderHelper = new Mock<IDatabaseReaderHelper>(MockBehavior.Strict);
            var seq = new MockSequence();
            var databaseReader = mockReader.Object;

            var expectedTable = new DataTable();

            const string selectAllLookup = "selectAllLookup";
            mockFactory.Setup(f => f.BuildSelectAllCompaniesFromLookupTableCommandString()).Returns(selectAllLookup);

            mockConnection.InSequence(seq).Setup(c => c.CreateCommand(selectAllLookup)).Returns(mockCommand.Object);

            mockConnection.InSequence(seq).Setup(c => c.Open());
            mockCommand.InSequence(seq).Setup(c => c.ExecuteReader()).Returns(databaseReader);
            mockReaderHelper.InSequence(seq)
                .Setup(h => h.CreateCompanyLookupTable(databaseReader))
                .Returns(expectedTable);
            mockConnection.InSequence(seq).Setup(c => c.Close());

            var communicator = new DatabaseCommunicator(mockConnection.Object)
            {
                Factory = mockFactory.Object,
                ReaderHelper = mockReaderHelper.Object
            };
            var lookupTable = communicator.SelectCompanyLookupTable();

            Assert.AreSame(expectedTable, lookupTable);

            mockFactory.VerifyAll();
            mockConnection.VerifyAll();
            mockCommand.VerifyAll();
            mockReader.VerifyAll();
        }

        [Test]
        [Category("Database")]
        public void TestCreateCompanyTable_CallsCommands_WithCorrectCommandString()
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

        [Test]
        [Category("Database")]
        public void TestGetsDatabaseConnection()
        {
            var mockConnection = new Mock<IDatabaseConnection>();

            var connection = mockConnection.Object;
            var communicator = new DatabaseCommunicator(connection);

            Assert.AreSame(connection, communicator.Connection);
        }

        [Test]
        [Category("Database")]
        public void TestGetsDatabaseConnectionStringFactory()
        {
            var communicator = new DatabaseCommunicator(null);

            Assert.AreSame(DatabaseCommandStringFactory.Singleton, communicator.Factory);
        }

        [Test]
        [Category("Database")]
        public void TestGetsDatabaseReaderFactory()
        {
            var communicator = new DatabaseCommunicator(null);

            Assert.AreSame(DatabaseReaderHelper.Singleton, communicator.ReaderHelper);
        }

        [Test]
        [Category("Database")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(
                typeof (IDatabaseCommunicator), typeof (DatabaseCommunicator));
        }

        [Test]
        [Category("Database")]
        public void TestInsertCompanyToTable_CallsCommands_WithInsertToLookup_AndCreateHistoryTable()
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

        [Test]
        [Category("Database")]
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

        [Test]
        [Category("Database")]
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

        [Test]
        [Category("Database")]
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

        [Test]
        [Category("Database")]
        public void TestSelectCompanyQuoteHistoryTable()
        {
            var mockFactory = new Mock<IDatabaseCommandStringFactory>();
            var mockConnection = new Mock<IDatabaseConnection>(MockBehavior.Strict);
            var mockCommand = new Mock<IDatabaseCommand>(MockBehavior.Strict);
            var mockReader = new Mock<IDatabaseReader>();
            var mockReaderHelper = new Mock<IDatabaseReaderHelper>(MockBehavior.Strict);
            var seq = new MockSequence();
            var databaseReader = mockReader.Object;

            var entry = new CompanyLookupEntry {Symbol = "AAPL"};
            var expectedTable = new DataTable();

            const string selectAllLookup = "selectAllLookup";
            mockFactory.Setup(f => f.BuildSelectAllQuotesFromHistoryTableCommandString(entry)).Returns(selectAllLookup);

            mockConnection.InSequence(seq).Setup(c => c.CreateCommand(selectAllLookup)).Returns(mockCommand.Object);

            mockConnection.InSequence(seq).Setup(c => c.Open());
            mockCommand.InSequence(seq).Setup(c => c.ExecuteReader()).Returns(databaseReader);
            mockReaderHelper.InSequence(seq)
                .Setup(h => h.CreateQuoteHistoryTable(databaseReader))
                .Returns(expectedTable);
            mockConnection.InSequence(seq).Setup(c => c.Close());

            var communicator = new DatabaseCommunicator(mockConnection.Object)
            {
                Factory = mockFactory.Object,
                ReaderHelper = mockReaderHelper.Object
            };
            var lookupTable = communicator.SelectCompanyQuoteHistoryTable(entry);

            Assert.AreSame(expectedTable, lookupTable);

            mockFactory.VerifyAll();
            mockConnection.VerifyAll();
            mockCommand.VerifyAll();
            mockReader.VerifyAll();
            mockReaderHelper.VerifyAll();
        }

        [Test]
        [Category("Database")]
        public void TestSelectQuoteLookupTable()
        {
            var mockFactory = new Mock<IDatabaseCommandStringFactory>();
            var mockConnection = new Mock<IDatabaseConnection>();
            var mockCommand = new Mock<IDatabaseCommand>();
            var mockReader = new Mock<IDatabaseReader>();
            var mockReaderHelper = new Mock<IDatabaseReaderHelper>(MockBehavior.Strict);
            var seq = new MockSequence();
            var databaseReader = mockReader.Object;

            var expectedRequests = new List<QuoteLookupRequest>();

            const string selectAllLookup = "selectAllLookup";
            mockFactory.Setup(f => f.BuildSelectAllCompaniesFromLookupTableCommandString()).Returns(selectAllLookup);

            mockConnection.InSequence(seq).Setup(c => c.CreateCommand(selectAllLookup)).Returns(mockCommand.Object);
            mockConnection.InSequence(seq).Setup(c => c.Open());
            mockCommand.InSequence(seq).Setup(c => c.ExecuteReader()).Returns(databaseReader);
            mockReaderHelper.InSequence(seq)
                .Setup(h => h.CreateQuoteLookupList(databaseReader))
                .Returns(expectedRequests);
            mockConnection.InSequence(seq).Setup(c => c.Close());

            var communicator = new DatabaseCommunicator(mockConnection.Object)
            {
                Factory = mockFactory.Object,
                ReaderHelper = mockReaderHelper.Object
            };
            var lookupRequests = communicator.SelectQuoteLookupTable();
            Assert.AreSame(expectedRequests, lookupRequests);

            mockFactory.VerifyAll();
            mockConnection.VerifyAll();
            mockCommand.VerifyAll();
            mockReader.VerifyAll();
        }

        [Test]
        [Category("Database")]
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

        [Test]
        [Category("Database")]
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
    }
}