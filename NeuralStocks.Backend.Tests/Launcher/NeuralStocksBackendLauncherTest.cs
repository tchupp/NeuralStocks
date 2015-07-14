using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralStocks.Backend.Controller;
using NeuralStocks.Backend.Launcher;
using NeuralStocks.DatabaseLayer.Communicator.Database;
using NeuralStocks.DatabaseLayer.Communicator.StockApi;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.Backend.Tests.Launcher
{
    [TestClass]
    public class NeuralStocksBackendLauncherTest : AssertTestClass
    {
        [TestMethod, TestCategory("Backend")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (INeuralStocksBackendLauncher), typeof (NeuralStocksBackendLauncher));
        }

        [TestMethod, TestCategory("Backend")]
        public void TestConstructorSetsUpSetupManagerCorrectly()
        {
            var launcher = new NeuralStocksBackendLauncher();

            var setupManager = AssertIsOfTypeAndGet<DatabaseSetupManager>(launcher.SetupManager);
            var communicator = AssertIsOfTypeAndGet<DatabaseCommunicator>(setupManager.DatabaseCommunicator);
            Assert.AreSame(DatabaseCommandStringFactory.Singleton, communicator.Factory);

            var connection = AssertIsOfTypeAndGet<DatabaseConnection>(communicator.Connection);
            var databaseName = AssertIsOfTypeAndGet<DatabaseName>(connection.DatabaseName);
            Assert.AreEqual("NeuralStocksDatabase.sqlite", databaseName.Name);
        }

        [TestMethod, TestCategory("Backend")]
        public void TestConstructorSetsUpBackendControllerCorrectly()
        {
            var launcher = new NeuralStocksBackendLauncher();

            var backendController = AssertIsOfTypeAndGet<BackendController>(launcher.BackendController);

            var stockApiCommunicator =
                AssertIsOfTypeAndGet<StockMarketApiCommunicator>(backendController.StockCommunicator);
            Assert.AreSame(StockMarketApi.Singleton, stockApiCommunicator.StockMarketApi);
            Assert.AreSame(TimestampParser.Singleton, stockApiCommunicator.TimestampParser);

            var communicator = AssertIsOfTypeAndGet<DatabaseCommunicator>(backendController.DatabaseCommunicator);
            Assert.AreSame(DatabaseCommandStringFactory.Singleton, communicator.Factory);

            var connection = AssertIsOfTypeAndGet<DatabaseConnection>(communicator.Connection);
            var databaseName = AssertIsOfTypeAndGet<DatabaseName>(connection.DatabaseName);
            Assert.AreEqual("NeuralStocksDatabase.sqlite", databaseName.Name);

            Assert.AreEqual("NeuralStocksDatabase.sqlite", backendController.DatabaseFileName);
        }

        [TestMethod, TestCategory("Backend")]
        public void TestConstructorSetsUpBackendLockCorrectly()
        {
            var launcher = new NeuralStocksBackendLauncher();

            var backendLock = AssertIsOfTypeAndGet<BackendLock>(launcher.BackendLock);

            Assert.AreEqual(58525, backendLock.Port);
        }

        [TestMethod, TestCategory("Backend")]
        public void TestStartBackendCallsInitializeDatabaseOnSetupManager_DatabaseDoesNotExist()
        {
            const string databaseFileName = "NeuralStocksDatabase.sqlite";
            File.Delete(databaseFileName);
            Assert.IsFalse(File.Exists(databaseFileName));

            var mockSetupManager = new Mock<IDatabaseSetupManager>();
            var mockController = new Mock<IBackendController>();
            var mockBackendLock = new Mock<IBackendLock>();
            mockBackendLock.Setup(m => m.Lock()).Returns(true);

            var launcher = new NeuralStocksBackendLauncher
            {
                SetupManager = mockSetupManager.Object,
                BackendController = mockController.Object,
                BackendLock = mockBackendLock.Object
            };

            mockSetupManager.Verify(m => m.InitializeDatabase(It.IsAny<string>()), Times.Never);

            launcher.StartBackend();

            mockSetupManager.Verify(m => m.InitializeDatabase(databaseFileName), Times.Once);
            mockBackendLock.VerifyAll();
        }

        [TestMethod, TestCategory("Backend")]
        public void TestStartBackendDoesNotCallsInitializeDatabaseOnSetupManager_DatabaseExists()
        {
            const string databaseFileName = "NeuralStocksDatabase.sqlite";
            File.Create(databaseFileName);
            Assert.IsTrue(File.Exists(databaseFileName));

            var mockSetupManager = new Mock<IDatabaseSetupManager>();
            var mockController = new Mock<IBackendController>();
            var mockBackendLock = new Mock<IBackendLock>();
            mockBackendLock.Setup(m => m.Lock()).Returns(true);

            var launcher = new NeuralStocksBackendLauncher
            {
                SetupManager = mockSetupManager.Object,
                BackendController = mockController.Object,
                BackendLock = mockBackendLock.Object
            };

            mockSetupManager.Verify(m => m.InitializeDatabase(It.IsAny<string>()), Times.Never);

            launcher.StartBackend();

            mockSetupManager.Verify(m => m.InitializeDatabase(It.IsAny<string>()), Times.Never);
            mockBackendLock.VerifyAll();
        }

        [TestMethod, TestCategory("Backend")]
        public void TestStartBackendCallsStartTimerOnBackendController()
        {
            var mockSetupManager = new Mock<IDatabaseSetupManager>();
            var mockController = new Mock<IBackendController>();
            var mockBackendLock = new Mock<IBackendLock>();
            mockBackendLock.Setup(m => m.Lock()).Returns(true);

            var launcher = new NeuralStocksBackendLauncher
            {
                SetupManager = mockSetupManager.Object,
                BackendController = mockController.Object,
                BackendLock = mockBackendLock.Object
            };

            mockController.Verify(m => m.StartTimer(), Times.Never);

            launcher.StartBackend();

            mockController.Verify(m => m.StartTimer(), Times.Once);
            mockBackendLock.VerifyAll();
        }

        [TestMethod, TestCategory("Backend")]
        public void TestStartBackendWritesCorrectlyToConsole()
        {
            var mockWriter = new Mock<TextWriter>();
            Console.SetOut(mockWriter.Object);

            var mockSetupManager = new Mock<IDatabaseSetupManager>();
            var mockController = new Mock<IBackendController>();
            var mockBackendLock = new Mock<IBackendLock>();
            mockBackendLock.Setup(m => m.Lock()).Returns(true);

            var launcher = new NeuralStocksBackendLauncher
            {
                SetupManager = mockSetupManager.Object,
                BackendController = mockController.Object,
                BackendLock = mockBackendLock.Object
            };

            mockWriter.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);

            launcher.StartBackend();

            mockWriter.Verify(m => m.WriteLine("Backend Started"), Times.Once);
            mockBackendLock.VerifyAll();
        }

        [TestMethod, TestCategory("Backend")]
        public void TestStartBackendDoesNotCallInitializeDatabase_StartTimer_OrWriteToConsole_BackendAlreadyLocked()
        {
            var mockWriter = new Mock<TextWriter>();
            Console.SetOut(mockWriter.Object);

            var mockSetupManager = new Mock<IDatabaseSetupManager>();
            var mockController = new Mock<IBackendController>();
            var mockBackendLock = new Mock<IBackendLock>();

            mockBackendLock.Setup(m => m.Lock()).Returns(false);

            var launcher = new NeuralStocksBackendLauncher
            {
                SetupManager = mockSetupManager.Object,
                BackendController = mockController.Object,
                BackendLock = mockBackendLock.Object
            };

            mockSetupManager.Verify(m => m.InitializeDatabase(It.IsAny<string>()), Times.Never);
            mockController.Verify(m => m.StartTimer(), Times.Never);
            mockWriter.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);

            launcher.StartBackend();

            mockSetupManager.Verify(m => m.InitializeDatabase(It.IsAny<string>()), Times.Never);
            mockController.Verify(m => m.StartTimer(), Times.Never);
            mockWriter.Verify(m => m.WriteLine("Backend Started"), Times.Never);
            mockBackendLock.VerifyAll();
        }

        [TestMethod, TestCategory("Backend")]
        public void TestStartBackendWritesToConsole_BackendAlreadyLocked()
        {
            var mockWriter = new Mock<TextWriter>();
            Console.SetOut(mockWriter.Object);

            var mockSetupManager = new Mock<IDatabaseSetupManager>();
            var mockController = new Mock<IBackendController>();
            var mockBackendLock = new Mock<IBackendLock>();

            mockBackendLock.Setup(m => m.Lock()).Returns(false);

            var launcher = new NeuralStocksBackendLauncher
            {
                SetupManager = mockSetupManager.Object,
                BackendController = mockController.Object,
                BackendLock = mockBackendLock.Object
            };

            mockWriter.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);

            launcher.StartBackend();

            mockWriter.Verify(m => m.WriteLine("Backend already started. Application is locked"), Times.Once);
            mockBackendLock.VerifyAll();
        }
    }
}