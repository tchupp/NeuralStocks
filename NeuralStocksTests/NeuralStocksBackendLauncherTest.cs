using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks;
using NeuralStocks.ApiCommunication;
using NeuralStocks.Controller;
using NeuralStocks.SqlDatabase;
using NeuralStocksTests.Testing;

namespace NeuralStocksTests
{
    [TestClass]
    public class NeuralStocksBackendLauncherTest
    {
        [TestMethod]
        public void TestImplementsInterface()
        {
            MoreAssert.ImplementsInterface(typeof (INeuralStocksBackendLauncher), typeof (NeuralStocksBackendLauncher));
        }

        [TestMethod]
        public void TestConstructorSetsUpSetupManagerCorrectly()
        {
            var launcher = new NeuralStocksBackendLauncher();

            var setupManager = MoreAssert.AssertIsOfTypeAndGet<SqlDatabaseSetupManager>(launcher.SetupManager);
            Assert.AreSame(SqlDatabaseCommandRunner.Singleton, setupManager.CommandRunner);
        }

        [TestMethod]
        public void TestConstructorSetsUpBackendControllerCorrectly()
        {
            var launcher = new NeuralStocksBackendLauncher();

            var backendController = MoreAssert.AssertIsOfTypeAndGet<BackendController>(launcher.BackendController);

            var stockApiCommunicator =
                MoreAssert.AssertIsOfTypeAndGet<StockMarketApiCommunicator>(backendController.Communicator);
            Assert.AreSame(StockMarketApi.Singleton, stockApiCommunicator.StockMarketApi);

            Assert.AreSame(SqlDatabaseCommandRunner.Singleton, backendController.CommandRunner);

            Assert.AreEqual("NeuralStocksDatabase.sqlite", backendController.DatabaseFileName);
        }
    }
}