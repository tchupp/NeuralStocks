using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralStocks.Backend.ApiCommunication;
using NeuralStocks.Backend.Controller;
using NeuralStocks.Backend.Launcher;
using NeuralStocks.Backend.SqlDatabase;
using NeuralStocks.Backend.Tests.Testing;

namespace NeuralStocks.Backend.Tests.Launcher
{
    [TestClass]
    public class NeuralStocksBackendLauncherTest : AssertTestClass
    {
        [TestMethod]
        public void TestImplementsInterface()
        {
            ImplementsInterface(typeof (INeuralStocksBackendLauncher), typeof (NeuralStocksBackendLauncher));
        }

        [TestMethod]
        public void TestConstructorSetsUpSetupManagerCorrectly()
        {
            var launcher = new NeuralStocksBackendLauncher();

            var setupManager = AssertIsOfTypeAndGet<SqlDatabaseSetupManager>(launcher.SetupManager);
            Assert.AreSame(SqlDatabaseCommandRunner.Singleton, setupManager.CommandRunner);
        }

        [TestMethod]
        public void TestConstructorSetsUpBackendControllerCorrectly()
        {
            var launcher = new NeuralStocksBackendLauncher();

            var backendController = AssertIsOfTypeAndGet<BackendController>(launcher.BackendController);

            var stockApiCommunicator =
                AssertIsOfTypeAndGet<StockMarketApiCommunicator>(backendController.Communicator);
            Assert.AreSame(StockMarketApi.Singleton, stockApiCommunicator.StockMarketApi);

            Assert.AreSame(SqlDatabaseCommandRunner.Singleton, backendController.CommandRunner);

            Assert.AreEqual("NeuralStocksDatabase.sqlite", backendController.DatabaseFileName);
        }

        [TestMethod]
        public void TestStartBackendCallsInitializeDatabaseOnSetupManager_DatabaseDoesNotExist()
        {
            const string databaseFileName = "NeuralStocksDatabase.sqlite";
            File.Delete(databaseFileName);
            Assert.IsFalse(File.Exists(databaseFileName));

            var mockSetupManager = new Mock<ISqlDatabaseSetupManager>();
            var mockController = new Mock<IBackendController>();

            var launcher = new NeuralStocksBackendLauncher
            {
                SetupManager = mockSetupManager.Object,
                BackendController = mockController.Object
            };

            mockSetupManager.Verify(m => m.InitializeDatabase(It.IsAny<string>()), Times.Never);

            launcher.StartBackend();

            mockSetupManager.Verify(m => m.InitializeDatabase(databaseFileName), Times.Once);
        }

        [TestMethod]
        public void TestStartBackendDoesNotCallsInitializeDatabaseOnSetupManager_DatabaseExists()
        {
            const string databaseFileName = "NeuralStocksDatabase.sqlite";
            File.Create(databaseFileName);
            Assert.IsTrue(File.Exists(databaseFileName));

            var mockSetupManager = new Mock<ISqlDatabaseSetupManager>();
            var mockController = new Mock<IBackendController>();

            var launcher = new NeuralStocksBackendLauncher
            {
                SetupManager = mockSetupManager.Object,
                BackendController = mockController.Object
            };

            mockSetupManager.Verify(m => m.InitializeDatabase(It.IsAny<string>()), Times.Never);

            launcher.StartBackend();

            mockSetupManager.Verify(m => m.InitializeDatabase(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void TestStartBackendCallsStartTimerOnBackendController()
        {
            var mockSetupManager = new Mock<ISqlDatabaseSetupManager>();
            var mockController = new Mock<IBackendController>();

            var launcher = new NeuralStocksBackendLauncher
            {
                SetupManager = mockSetupManager.Object,
                BackendController = mockController.Object
            };

            mockController.Verify(m => m.StartTimer(), Times.Never);

            launcher.StartBackend();

            mockController.Verify(m => m.StartTimer(), Times.Once);
        }

        [TestMethod]
        public void TestStartBackendWritesCorrectlyToConsole()
        {
            var mockWriter = new Mock<TextWriter>();
            Console.SetOut(mockWriter.Object);

            var mockSetupManager = new Mock<ISqlDatabaseSetupManager>();
            var mockController = new Mock<IBackendController>();
            var launcher = new NeuralStocksBackendLauncher
            {
                SetupManager = mockSetupManager.Object,
                BackendController = mockController.Object
            };

            mockWriter.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);

            launcher.StartBackend();

            mockWriter.Verify(m => m.WriteLine("Backend Started"), Times.Once);
        }
    }
}