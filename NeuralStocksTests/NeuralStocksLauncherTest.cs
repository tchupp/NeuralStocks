using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks;

namespace NeuralStocksTests
{
    [TestClass]
    public class NeuralStocksLauncherTest
    {
        [TestMethod]
        public void TestInterface_INeuralStocksLauncher()
        {
            var interfaces = typeof (NeuralStocksLauncher).GetInterfaces();
            Assert.AreEqual(1, interfaces.Count());
            Assert.IsTrue(interfaces.Contains(typeof (INeuralStocksLauncher)));
        }
    }
}