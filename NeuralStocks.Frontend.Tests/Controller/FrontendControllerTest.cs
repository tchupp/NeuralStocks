using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralStocks.DatabaseLayer.Communicator.Database;
using NeuralStocks.DatabaseLayer.Communicator.StockApi;
using NeuralStocks.DatabaseLayer.Model.Database;
using NeuralStocks.DatabaseLayer.Model.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NeuralStocks.Frontend.Controller;

namespace NeuralStocks.Frontend.Tests.Controller
{
    [TestClass]
    public class FrontendControllerTest : AssertTestClass
    {
        [TestMethod, TestCategory("Frontend")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IFrontendController), typeof (FrontendController));
        }

        [TestMethod, TestCategory("Frontend")]
        public void TestGetsStockMarketApiCommunicatorPassedIn()
        {
            var mockApiCommunicator = new Mock<IStockMarketApiCommunicator>();

            var expected = mockApiCommunicator.Object;
            var controller = new FrontendController(expected, null, null);
            Assert.AreSame(expected, controller.StockCommunicator);
        }

        [TestMethod, TestCategory("Frontend")]
        public void TestGetsTableFactoryPassedIn()
        {
            var mockTableFactory = new Mock<IDataTableFactory>();

            var expectedFactory = mockTableFactory.Object;
            var controller = new FrontendController(null, expectedFactory, null);
            Assert.AreSame(expectedFactory, controller.TableFactory);
        }

        [TestMethod, TestCategory("Frontend")]
        public void TestGetsDatabaseCommunicator()
        {
            var mockDatabaseCommunicator = new Mock<IDatabaseCommunicator>();

            var expected = mockDatabaseCommunicator.Object;
            var controller = new FrontendController(null, null, expected);
            Assert.AreSame(expected, controller.DatabaseCommunicator);
        }

        [TestMethod, TestCategory("Frontend")]
        public void TestGetSearchResultsForNewCompany()
        {
            var mockApiCommunicator = new Mock<IStockMarketApiCommunicator>();
            var mockTableFactory = new Mock<IDataTableFactory>();

            const string expectedSearch = "Apple";
            var lookupResponseList = new List<CompanyLookupResponse>();
            var expectedDataTable = new DataTable();

            mockApiCommunicator.Setup(
                c => c.CompanyLookup(It.Is<CompanyLookupRequest>(
                    r => r.Company == expectedSearch))).Returns(lookupResponseList);
            mockTableFactory.Setup(f => f.BuildNewCompanySearchTable(lookupResponseList)).Returns(expectedDataTable);

            var controller = new FrontendController(mockApiCommunicator.Object, mockTableFactory.Object, null);

            var dataTable = controller.GetSearchResultsForNewCompany(expectedSearch);
            Assert.AreSame(expectedDataTable, dataTable);

            mockApiCommunicator.VerifyAll();
            mockTableFactory.VerifyAll();
        }

        [TestMethod, TestCategory("Frontend")]
        public void TestGetSearchResultsForCurrentCompany()
        {
            var mockTableFactory = new Mock<IDataTableFactory>();
            var mockDatabaseCommunicator = new Mock<IDatabaseCommunicator>();

            const string expectedSearch = "Apple";
            var quoteHistoryList = new List<QuoteHistoryEntry>();
            var expectedDataTable = new DataTable();

            var lookupEntry = new CompanyLookupEntry
            {
                Symbol = expectedSearch
            };

            mockDatabaseCommunicator.Setup(
                c => c.GetQuoteHistoryEntryList(It.Is<CompanyLookupEntry>(
                    e => e.Symbol == expectedSearch))).Returns(quoteHistoryList);
            mockTableFactory.Setup(
                f => f.BuildCurrentCompanySearchTable(quoteHistoryList)).Returns(expectedDataTable);

            var controller = new FrontendController(null, mockTableFactory.Object, mockDatabaseCommunicator.Object);

            var dataTable = controller.GetSearchResultsForCurrentCompany(lookupEntry);
            Assert.AreSame(expectedDataTable, dataTable);

            mockTableFactory.VerifyAll();
            mockDatabaseCommunicator.VerifyAll();
        }
    }
}