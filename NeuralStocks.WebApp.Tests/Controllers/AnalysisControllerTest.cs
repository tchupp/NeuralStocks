using System.Web.Mvc;
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
    }
}