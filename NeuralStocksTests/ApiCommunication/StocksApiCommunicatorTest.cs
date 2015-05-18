using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.ApiCommunication;

namespace NeuralStocksTests.ApiCommunication
{
    [TestClass]
    public class StocksApiCommunicatorTest
    {
        [TestMethod]
        public void TestImplementsInterface()
        {
            var interfaces = typeof (StocksApiCommunicator).GetInterfaces();
            Assert.AreEqual(1, interfaces.Count());
            Assert.IsTrue(interfaces.Contains(typeof (IStocksApiCommunicator)));
        }

        [TestMethod]
        public void TestGetResponce()
        {
            var communicator = new StocksApiCommunicator();
            communicator.GetResponse();
        }
    }
}