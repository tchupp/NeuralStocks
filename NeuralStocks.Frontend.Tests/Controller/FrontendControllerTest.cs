using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.StockApi;
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
        public void TestGetsDatabaseCommunicator()
        {
            var mockDatabaseCommunicator = new Mock<IDatabaseCommunicator>();

            var expected = mockDatabaseCommunicator.Object;
            var controller = new FrontendController(expected);
            Assert.AreSame(expected, controller.DatabaseCommunicator);
        }

        [TestMethod]
        public void TestConstructorSetsStockCommunicatorAndTableFactory_AsSingletons()
        {
            var controller = new FrontendController(null);

            Assert.AreSame(StockMarketApiCommunicator.Singleton, controller.StockCommunicator);
            Assert.AreSame(DataTableFactory.Factory, controller.TableFactory);
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

            var controller = new FrontendController(null)
            {
                StockCommunicator = mockApiCommunicator.Object,
                TableFactory = mockTableFactory.Object
            };

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
                c => c.SelectQuoteHistoryEntryList(It.Is<CompanyLookupEntry>(
                    e => e.Symbol == expectedSearch))).Returns(quoteHistoryList);
            mockTableFactory.Setup(
                f => f.BuildCurrentCompanySearchTable(quoteHistoryList)).Returns(expectedDataTable);

            var controller = new FrontendController(mockDatabaseCommunicator.Object)
            {
                TableFactory = mockTableFactory.Object
            };

            var dataTable = controller.GetSearchResultsForCurrentCompany(lookupEntry);
            Assert.AreSame(expectedDataTable, dataTable);

            mockTableFactory.VerifyAll();
            mockDatabaseCommunicator.VerifyAll();
        }
    }
}