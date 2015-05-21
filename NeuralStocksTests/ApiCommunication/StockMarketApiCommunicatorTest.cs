using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.ApiCommunication;

namespace NeuralStocksTests.ApiCommunication
{
    [TestClass]
    public class StockMarketApiCommunicatorTest
    {
        [TestMethod]
        public void TestImplementsInterface()
        {
            var interfaces = typeof (StockMarketApiCommunicator).GetInterfaces();
            Assert.AreEqual(1, interfaces.Count());
            Assert.IsTrue(interfaces.Contains(typeof (IStockMarketApiCommunicator)));
        }

        [TestMethod]
        public void TestGetResponce()
        {
            var communicator = new StockMarketApiCommunicator();
            communicator.GetResponse();
        }
    }
}