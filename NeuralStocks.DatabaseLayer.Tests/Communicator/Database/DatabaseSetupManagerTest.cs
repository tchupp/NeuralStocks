using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralStocks.DatabaseLayer.Communicator.Database;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.DatabaseLayer.Tests.Communicator.Database
{
    [TestClass]
    public class DatabaseSetupManagerTest : AssertTestClass
    {
        [TestMethod, TestCategory("Database")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IDatabaseSetupManager), typeof (DatabaseSetupManager));
        }

        [TestMethod, TestCategory("Database")]
        public void TestInitializeDatabaseCreatesInitialDatabaseWithEmptyTable()
        {
            const string databaseFileName = "TestStocksDatabase.sqlite";

            var mockCommandRunner = new Mock<IDatabaseCommunicator>();
            var setupManager = new DatabaseSetupManager(mockCommandRunner.Object);

            mockCommandRunner.Verify(m => m.CreateDatabase(databaseFileName), Times.Never);
            mockCommandRunner.Verify(m => m.CreateCompanyTable(), Times.Never);

            setupManager.InitializeDatabase(databaseFileName);

            mockCommandRunner.Verify(m => m.CreateDatabase(databaseFileName), Times.Once);
            mockCommandRunner.Verify(m => m.CreateCompanyTable(), Times.Once);
        }
    }
}