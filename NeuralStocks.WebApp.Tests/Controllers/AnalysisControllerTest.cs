using System.Web.Mvc;
using Moq;
using NeuralStocks.DatabaseLayer.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NeuralStocks.WebApp.Controllers;
using NUnit.Framework;

namespace NeuralStocks.WebApp.Tests.Controllers
{
    [TestFixture]
    public class AnalysisControllerTest : AssertTestClass
    {
        [Test]
        [Category("Web App")]
        public void TestExtendsMvcController()
        {
            AssertExtendsClass(typeof (Controller), typeof (AnalysisController));
        }

        [Test]
        public void TestGetCompanyLookup_CallsStockMarketApiCommunicator()
        {
            const string companySearch = "";
            var mockCommunicator = new Mock<IStockMarketApiCommunicator>();

            mockCommunicator.Setup(c => c.CompanyLookup(It.Is<CompanyLookupRequest>(
                r => r.Company == companySearch)));

            var controller = new AnalysisController(mockCommunicator.Object);

            controller.GetCompanyLookup(companySearch);
        }

        [Test]
        public void TestGetCompanyLookup_HasHttpGetAttribute()
        {
            var method = typeof (AnalysisController).GetMethod("GetCompanyLookup");

            Assert.NotNull(method);

            AssertMethodHasAttribute(method, typeof (HttpGetAttribute));
        }
    }
}