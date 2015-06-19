

using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            AssertImplementsInterface(typeof(IFrontendController), typeof(FrontendController));
        }
    }
}

