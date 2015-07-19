using System.Collections.Generic;
using System.Data;
using Moq;
using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NeuralStocks.Frontend.Controller;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace NeuralStocks.Frontend.Tests.Controller
{
    [TestFixture]
    public class FrontendControllerTest : AssertTestClass
    {
        [Test]
        [Category("Frontend")]
        public void TestConstructorSetsStockCommunicatorAndTableFactory_AsSingletons()
        {
            var controller = new FrontendController(null);

            Assert.AreSame(StockMarketApiCommunicator.Singleton, controller.StockCommunicator);
            Assert.AreSame(DataTableFactory.Factory, controller.TableFactory);
        }

        [Test]
        [Category("Frontend")]
        public void TestGetsDatabaseCommunicator()
        {
            var mockDatabaseCommunicator = new Mock<IDatabaseCommunicator>();

            var expected = mockDatabaseCommunicator.Object;
            var controller = new FrontendController(expected);
            Assert.AreSame(expected, controller.DatabaseCommunicator);
        }

        [Test]
        [Category("Frontend")]
        public void TestGetSummaryForCurrentCompany()
        {
            var mockDatabaseCommunicator = new Mock<IDatabaseCommunicator>();

            const string expectedSearch = "Apple";
            var expectedDataTable = new DataTable();

            var lookupEntry = new CompanyLookupEntry
            {
                Symbol = expectedSearch
            };

            mockDatabaseCommunicator.Setup(
                c => c.SelectCompanyQuoteHistoryTable(It.Is<CompanyLookupEntry>(
                    e => e.Symbol == expectedSearch))).Returns(expectedDataTable);

            var controller = new FrontendController(mockDatabaseCommunicator.Object);

            var dataTable = controller.GetSummaryForCurrentCompany(lookupEntry);
            Assert.AreSame(expectedDataTable, dataTable);

            mockDatabaseCommunicator.VerifyAll();
        }

        [Test]
        [Category("Frontend")]
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

        [Test]
        [Category("Frontend")]
        public void TestGetCompanyLookupTable()
        {
            var mockDatabaseCommunicator = new Mock<IDatabaseCommunicator>();

            var expectedDataTable = new DataTable();

            mockDatabaseCommunicator.Setup(
                c => c.SelectCompanyLookupTable()).Returns(expectedDataTable);

            var controller = new FrontendController(mockDatabaseCommunicator.Object);

            var dataTable = controller.GetCompanyLookupTable();
            Assert.AreSame(expectedDataTable, dataTable);

            mockDatabaseCommunicator.VerifyAll();
        }

        [Test]
        [Category("Frontend")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IFrontendController), typeof (FrontendController));
        }
    }
}