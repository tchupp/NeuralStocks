using System.Collections.Generic;
using System.Data.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralStocks.ApiCommunication;
using NeuralStocks.Controller;
using NeuralStocks.SqlDatabase;
using NeuralStocksTests.Testing;

namespace NeuralStocksTests.Controller
{
    [TestClass]
    public class BackendControllerTest
    {
        [TestMethod]
        public void TestImplementsInterface()
        {
            MoreAssert.ImplementsInterface(typeof (IBackendController), typeof (BackendController));
        }

        [TestMethod]
        public void TestGetsStockMarketApiCommunicatorPassedIn()
        {
            var mockCommunicator = new Mock<IStockMarketApiCommunicator>();

            var controller = new BackendController(mockCommunicator.Object, null);

            Assert.AreSame(mockCommunicator.Object, controller.Communicator);
        }

        [TestMethod]
        public void TestGetsSqlDatabaseCommandRunnerPassedIn()
        {
            var mockCommandRunner = new Mock<ISqlDatabaseCommandRunner>();

            var controller = new BackendController(null, mockCommandRunner.Object);

            Assert.AreSame(mockCommandRunner.Object, controller.CommandRunner);
        }

        [TestMethod]
        public void TestGetsCorrectDatabaseFileName()
        {
            var controller = new BackendController(null, null);

            Assert.AreEqual("NeuralStocksDatabase.sqlite", controller.DatabaseFileName);
        }

        [TestMethod]
        public void TestUpdateCompanyQuotes()
        {
            const string company1 = "NFLX";
            const string company2 = "AAPL";

            var quoteRequest1 = new QuoteLookupRequest(company1);
            var quoteRequest2 = new QuoteLookupRequest(company2);
            var quoteRequests = new List<QuoteLookupRequest> {quoteRequest1, quoteRequest2};

            var quoteResponse1 = new QuoteLookupResponse {Name = company1};
            var quoteResponse2 = new QuoteLookupResponse {Name = company2};

            var mockCommunicator = new Mock<IStockMarketApiCommunicator>();
            var mockCommandRunner = new Mock<ISqlDatabaseCommandRunner>();

            mockCommandRunner.Setup(m => m.GetQuoteLookupsFromTable(It.IsAny<SQLiteConnection>()))
                .Returns(quoteRequests);

            mockCommunicator.Setup(m => m.QuoteLookup(quoteRequest1)).Returns(quoteResponse1);
            mockCommunicator.Setup(m => m.QuoteLookup(quoteRequest2)).Returns(quoteResponse2);

            var controller = new BackendController(mockCommunicator.Object, mockCommandRunner.Object);
            controller.UpdateCompanyQuotes();

            mockCommandRunner.Verify(m => m.UpdateCompanyTimestamp(It.IsAny<SQLiteConnection>(), quoteResponse1),
                Times.Once);
            mockCommandRunner.Verify(m => m.UpdateCompanyTimestamp(It.IsAny<SQLiteConnection>(), quoteResponse2),
                Times.Once);

            mockCommandRunner.Verify(m => m.AddQuoteResponseToTable(It.IsAny<SQLiteConnection>(), quoteResponse1),
                Times.Once);
            mockCommandRunner.Verify(m => m.AddQuoteResponseToTable(It.IsAny<SQLiteConnection>(), quoteResponse2),
                Times.Once);

            mockCommunicator.VerifyAll();
            mockCommandRunner.VerifyAll();
        }
    }
}