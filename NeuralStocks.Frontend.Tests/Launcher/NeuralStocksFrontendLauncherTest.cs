

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.Backend.Tests.Testing;
using NeuralStocks.Frontend.Launcher;

namespace NeuralStocks.Frontend.Tests.Launcher
{
    [TestClass]
    public class NeuralStocksFrontendLauncherTest : AssertTestClass
    {
        [TestMethod]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof(INeuralStocksFrontendLauncher), typeof(NeuralStocksFrontendLauncher));
        }
    }
}

