using System.Data;
using System.Web.Mvc;
using Moq;
using NeuralStocks.DatabaseLayer.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NeuralStocks.WebApp.Controllers;
using NUnit.Framework;

namespace NeuralStocks.WebApp.Tests.Controllers
{
    [TestFixture]
    [Category("Web App")]
    public class AnalysisControllerTest : AssertTestClass
    {
        [Test]
        public void TestExtendsMvcController()
        {
            AssertExtendsClass(typeof (Controller), typeof (AnalysisController));
        }

        [Test]
        public void TestGetCompanyLookup_CallsStockMarketApiCommunicator()
        {
            const string companySearch = "i want to find a company";
            var mockCommunicator = new Mock<IStockMarketApiCommunicator>();
            var mockHelper = new Mock<IJsonConversionHelper>();

            var expectedTable = new DataTable();
            const string expectedJson = "[{This is Json, can you here me?}]";

            mockCommunicator.Setup(c => c.CompanyLookup(companySearch)).Returns(expectedTable);
            mockHelper.Setup(c => c.Serialize(expectedTable)).Returns(expectedJson);

            var controller = new AnalysisController(mockCommunicator.Object, mockHelper.Object);

            var actualJson = controller.GetCompanyLookup(companySearch);

            Assert.AreEqual(expectedJson, actualJson);
            mockCommunicator.VerifyAll();
            mockHelper.VerifyAll();
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