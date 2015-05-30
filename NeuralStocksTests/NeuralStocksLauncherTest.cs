using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks;
using NeuralStocksTests.Testing;

namespace NeuralStocksTests
{
    [TestClass]
    public class NeuralStocksLauncherTest
    {
        [TestMethod]
        public void TestImplementsInterface()
        {
            MoreAssert.ImplementsInterface(typeof (INeuralStocksLauncher), typeof (NeuralStocksLauncher));
        }
    }
}