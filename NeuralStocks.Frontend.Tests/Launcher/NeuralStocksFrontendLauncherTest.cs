using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.DatabaseLayer.Communicator.Database;
using NeuralStocks.DatabaseLayer.Communicator.StockApi;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NeuralStocks.Frontend.Controller;
using NeuralStocks.Frontend.Launcher;
using NeuralStocks.Frontend.UI;

namespace NeuralStocks.Frontend.Tests.Launcher
{
    [TestClass]
    public class NeuralStocksFrontendLauncherTest : AssertTestClass
    {
        [TestMethod, TestCategory("Frontend")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (INeuralStocksFrontendLauncher), typeof (NeuralStocksFrontendLauncher));
        }

        [TestMethod, TestCategory("Frontend")]
        public void TestFrontEndControllerIsConstructedWithCorrectArguments()
        {
            var launcher = new NeuralStocksFrontendLauncher();
            var frontendController = AssertIsOfTypeAndGet<FrontendController>(launcher.FrontendController);

            var stockCommunicator =
                AssertIsOfTypeAndGet<StockMarketApiCommunicator>(frontendController.StockCommunicator);
            Assert.AreSame(StockMarketApi.Singleton, stockCommunicator.StockMarketApi);
            Assert.AreSame(TimestampParser.Singleton, stockCommunicator.TimestampParser);

            Assert.AreSame(DataTableFactory.Factory, frontendController.TableFactory);

            var communicator = AssertIsOfTypeAndGet<DatabaseCommunicator>(frontendController.DatabaseCommunicator);
            Assert.AreSame(DatabaseCommandStringFactory.Singleton, communicator.Factory);

            var connection = AssertIsOfTypeAndGet<DatabaseConnection>(communicator.Connection);
            var databaseName = AssertIsOfTypeAndGet<DatabaseName>(connection.DatabaseName);
            Assert.AreEqual("NeuralStocksDatabase.sqlite", databaseName.Name);
        }

        [TestMethod, TestCategory("Frontend")]
        public void TestMainWindowIsConstructedWithFrontendController()
        {
            var launcher = new NeuralStocksFrontendLauncher();
            var mainWindow = AssertIsOfTypeAndGet<MainWindow>(launcher.MainWindow);

            Assert.AreSame(launcher.FrontendController, mainWindow.FrontendController);
        }
    }
}