using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NeuralStocks.Frontend.Controller;
using NeuralStocks.Frontend.Launcher;
using NeuralStocks.Frontend.UI;
using NUnit.Framework;

namespace NeuralStocks.Frontend.Tests.Launcher
{
    [TestFixture]
    public class NeuralStocksFrontendLauncherTest : AssertTestClass
    {
        [Test]
        [Category("Frontend")]
        public void TestFrontEndControllerIsConstructedWithCorrectArguments()
        {
            var launcher = new NeuralStocksFrontendLauncher();
            var frontendController = AssertIsOfTypeAndGet<FrontendController>(launcher.FrontendController);

            Assert.AreSame(StockMarketApiCommunicator.Singleton, frontendController.StockCommunicator);

            Assert.AreSame(DataTableFactory.Factory, frontendController.TableFactory);

            var communicator = AssertIsOfTypeAndGet<DatabaseCommunicator>(frontendController.DatabaseCommunicator);
            Assert.AreSame(DatabaseCommandStringFactory.Singleton, communicator.Factory);

            var connection = AssertIsOfTypeAndGet<DatabaseConnection>(communicator.Connection);
            var databaseName = AssertIsOfTypeAndGet<DatabaseName>(connection.DatabaseName);
            Assert.AreEqual(DatabaseConfiguration.FullDatabaseFileName, databaseName.Name);
        }

        [Test]
        [Category("Frontend")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (INeuralStocksFrontendLauncher), typeof (NeuralStocksFrontendLauncher));
        }

        [Test]
        [Category("Frontend")]
        public void TestMainWindowIsConstructedWithFrontendController()
        {
            var launcher = new NeuralStocksFrontendLauncher();
            var mainWindow = AssertIsOfTypeAndGet<MainWindow>(launcher.MainWindow);

            Assert.AreSame(launcher.FrontendController, mainWindow.FrontendController);
        }
    }
}