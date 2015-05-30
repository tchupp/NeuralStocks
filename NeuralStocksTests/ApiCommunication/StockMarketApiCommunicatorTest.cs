using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.ApiCommunication;
using NeuralStocksTests.Testing;

namespace NeuralStocksTests.ApiCommunication
{
    [TestClass]
    public class StockMarketApiCommunicatorTest
    {
        [TestMethod]
        public void TestImplementsInterface()
        {
            MoreAssert.ImplementsInterface(typeof (IStockMarketApiCommunicator), typeof (StockMarketApiCommunicator));
        }

        [TestMethod]
        public void TestGetResponce()
        {
            var communicator = new StockMarketApiCommunicator();
            communicator.GetResponse();
        }
    }
}