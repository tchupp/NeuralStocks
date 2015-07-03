using System.Collections.Generic;
using System.Data.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralStocks.Backend.Controller;
using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.Model.StockApi;
using NeuralStocks.DatabaseLayer.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.Backend.Tests.Controller
{
    [TestClass]
    public class BackendControllerTest : AssertTestClass
    {
        [TestMethod, TestCategory("Backend")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IBackendController), typeof (BackendController));
        }

        [TestMethod, TestCategory("Backend")]
        public void TestGetsStockMarketApiCommunicatorPassedIn()
        {
            var mockCommunicator = new Mock<IStockMarketApiCommunicator>();

            var controller = new BackendController(mockCommunicator.Object, null, null);

            Assert.AreSame(mockCommunicator.Object, controller.StockCommunicator);
        }

        [TestMethod, TestCategory("Backend")]
        public void TestGetsSqlDatabaseCommandRunnerPassedIn()
        {
            var mockCommandRunner = new Mock<IDatabaseCommunicator>();

            var controller = new BackendController(null, mockCommandRunner.Object, null);

            Assert.AreSame(mockCommandRunner.Object, controller.DatabaseCommunicator);
        }

        [TestMethod, TestCategory("Backend")]
        public void TestGetsCorrectDatabaseFileName()
        {
            const string databaseFileName = "TestStocksDatabase.sqlite";
            var controller = new BackendController(null, null, databaseFileName);

            Assert.AreEqual(databaseFileName, controller.DatabaseFileName);
        }

        [TestMethod, TestCategory("Backend")]
        public void TestConstructorSetsUpBackendTimer()
        {
            var controller = new BackendController(null, null, null);

            var timer = AssertIsOfTypeAndGet<BackendTimer>(controller.BackendTimer);
            Assert.AreSame(controller, timer.Controller);

            Assert.AreEqual(60000, timer.Interval);
        }

        [TestMethod, TestCategory("Backend")]
        public void TestStartTimerCallsStartOnTimer()
        {
            var mockTimer = new Mock<IBackendTimer>();
            var controller = new BackendController(null, null, null) {BackendTimer = mockTimer.Object};

            mockTimer.Verify(t => t.Start(), Times.Never);
            controller.StartTimer();
            mockTimer.Verify(t => t.Start(), Times.Once);
        }

        [TestMethod, TestCategory("Backend")]
        public void TestDisposeCallsStopOnTimer()
        {
            var mockTimer = new Mock<IBackendTimer>();
            var controller = new BackendController(null, null, null) {BackendTimer = mockTimer.Object};

            mockTimer.Verify(t => t.Stop(), Times.Never);
            controller.Dispose();
            mockTimer.Verify(t => t.Stop(), Times.Once);
        }

        [TestMethod, TestCategory("Backend")]
        public void TestUpdateCompanyQuotes_RecentDateDifferentThanTimestamp()
        {
            const string company1 = "NFLX";
            const string company2 = "AAPL";
            const string timestamp1 = "Tues Jun 16";
            const string timestamp2 = "Wed Jun 17";

            const string databaseFileName = "TestStocksDatabase.sqlite";
            const string databaseConnectionString = "Data Source=" + databaseFileName + ";Version=3;";

            var quoteRequest1 = new QuoteLookupRequest {Company = company1, Timestamp = timestamp1};
            var quoteRequest2 = new QuoteLookupRequest {Company = company2, Timestamp = timestamp1};
            var quoteRequests = new List<QuoteLookupRequest> {quoteRequest1, quoteRequest2};

            var quoteResponse1 = new QuoteLookupResponse {Name = company1, Timestamp = timestamp2};
            var quoteResponse2 = new QuoteLookupResponse {Name = company2, Timestamp = timestamp2};

            var mockCommunicator = new Mock<IStockMarketApiCommunicator>();
            var mockCommandRunner = new Mock<IDatabaseCommunicator>();

            mockCommandRunner.Setup(m => m.GetQuoteLookupList(It.Is<SQLiteConnection>(
                c => c.ConnectionString == databaseConnectionString))).Returns(quoteRequests);

            mockCommunicator.Setup(m => m.QuoteLookup(quoteRequest1)).Returns(quoteResponse1);
            mockCommunicator.Setup(m => m.QuoteLookup(quoteRequest2)).Returns(quoteResponse2);

            var controller = new BackendController(mockCommunicator.Object, mockCommandRunner.Object, databaseFileName);
            controller.UpdateCompanyQuotes();

            mockCommandRunner.Verify(m => m.UpdateCompanyTimestamp(It.Is<SQLiteConnection>(
                c => c.ConnectionString == databaseConnectionString), quoteResponse1), Times.Once);
            mockCommandRunner.Verify(m => m.UpdateCompanyTimestamp(It.Is<SQLiteConnection>(
                c => c.ConnectionString == databaseConnectionString), quoteResponse2), Times.Once);

            mockCommandRunner.Verify(m => m.AddQuoteResponseToTable(It.Is<SQLiteConnection>(
                c => c.ConnectionString == databaseConnectionString), quoteResponse1), Times.Once);
            mockCommandRunner.Verify(m => m.AddQuoteResponseToTable(It.Is<SQLiteConnection>(
                c => c.ConnectionString == databaseConnectionString), quoteResponse2), Times.Once);

            mockCommunicator.VerifyAll();
            mockCommandRunner.VerifyAll();
        }

        [TestMethod, TestCategory("Backend")]
        public void TestUpdateCompanyQuotes_RecentDateSameAsTimestamp()
        {
            const string company1 = "NFLX";
            const string company2 = "AAPL";
            const string timestamp = "Tues Jun 16";

            const string databaseFileName = "TestStocksDatabase.sqlite";
            const string databaseConnectionString = "Data Source=" + databaseFileName + ";Version=3;";

            var quoteRequest1 = new QuoteLookupRequest {Company = company1, Timestamp = timestamp};
            var quoteRequest2 = new QuoteLookupRequest {Company = company2, Timestamp = timestamp};
            var quoteRequests = new List<QuoteLookupRequest> {quoteRequest1, quoteRequest2};

            var quoteResponse1 = new QuoteLookupResponse {Name = company1, Timestamp = timestamp};
            var quoteResponse2 = new QuoteLookupResponse {Name = company2, Timestamp = timestamp};

            var mockCommunicator = new Mock<IStockMarketApiCommunicator>();
            var mockCommandRunner = new Mock<IDatabaseCommunicator>();

            mockCommandRunner.Setup(m => m.GetQuoteLookupList(It.Is<SQLiteConnection>(
                c => c.ConnectionString == databaseConnectionString))).Returns(quoteRequests);

            mockCommunicator.Setup(m => m.QuoteLookup(quoteRequest1)).Returns(quoteResponse1);
            mockCommunicator.Setup(m => m.QuoteLookup(quoteRequest2)).Returns(quoteResponse2);

            var controller = new BackendController(mockCommunicator.Object, mockCommandRunner.Object, databaseFileName);
            controller.UpdateCompanyQuotes();

            mockCommandRunner.Verify(m => m.UpdateCompanyTimestamp(It.Is<SQLiteConnection>(
                c => c.ConnectionString == databaseConnectionString), quoteResponse1), Times.Never);
            mockCommandRunner.Verify(m => m.UpdateCompanyTimestamp(It.Is<SQLiteConnection>(
                c => c.ConnectionString == databaseConnectionString), quoteResponse2), Times.Never);

            mockCommandRunner.Verify(m => m.AddQuoteResponseToTable(It.Is<SQLiteConnection>(
                c => c.ConnectionString == databaseConnectionString), quoteResponse1), Times.Never);
            mockCommandRunner.Verify(m => m.AddQuoteResponseToTable(It.Is<SQLiteConnection>(
                c => c.ConnectionString == databaseConnectionString), quoteResponse2), Times.Never);

            mockCommunicator.VerifyAll();
            mockCommandRunner.VerifyAll();
        }
    }
}