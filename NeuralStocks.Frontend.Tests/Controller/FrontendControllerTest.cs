using System.Collections.Generic;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralStocks.Backend.ApiCommunication;
using NeuralStocks.Backend.Tests.Testing;
using NeuralStocks.Frontend.Controller;

namespace NeuralStocks.Frontend.Tests.Controller
{
    [TestClass]
    public class FrontendControllerTest : AssertTestClass
    {
        [TestMethod]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IFrontendController), typeof (FrontendController));
        }

        [TestMethod]
        public void TestGetsStockMarketApiCommunicatorPassedIn()
        {
            var mockApiCommunicator = new Mock<IStockMarketApiCommunicator>();

            var expected = mockApiCommunicator.Object;
            var controller = new FrontendController(expected, null);
            Assert.AreSame(expected, controller.StockCommunicator);
        }

        [TestMethod]
        public void TestGetsTableFactoryPassedIn()
        {
            var mockTableFactory = new Mock<IDataTableFactory>();

            var expectedFactory = mockTableFactory.Object;
            var controller = new FrontendController(null, expectedFactory);
            Assert.AreSame(expectedFactory, controller.TableFactory);
        }

        [TestMethod]
        public void TestGetSearchResultsForCompany()
        {
            var mockApiCommunicator = new Mock<IStockMarketApiCommunicator>();
            var mockTableFactory = new Mock<IDataTableFactory>();

            const string expectedSearch = "Apple";
            var lookupResponseList = new List<CompanyLookupResponse>();
            var expectedDataTable = new DataTable();

            mockApiCommunicator.Setup(
                c => c.CompanyLookup(It.Is<CompanyLookupRequest>(
                    r => r.Company == expectedSearch))).Returns(lookupResponseList);
            mockTableFactory.Setup(f => f.BuildCompanySearchTable(lookupResponseList)).Returns(expectedDataTable);

            var controller = new FrontendController(mockApiCommunicator.Object, mockTableFactory.Object);

            var dataTable = controller.GetSearchResultsForCompany(expectedSearch);
            Assert.AreSame(expectedDataTable, dataTable);

            mockApiCommunicator.VerifyAll();
            mockTableFactory.VerifyAll();
        }
    }
}