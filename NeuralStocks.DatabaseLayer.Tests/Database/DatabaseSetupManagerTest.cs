using System.Data.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.DatabaseLayer.Tests.Database
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
            const string databaseConnectionString = "Data Source=" + databaseFileName + ";Version=3;";

            var mockCommandRunner = new Mock<IDatabaseCommunicator>();
            var setupManager = new DatabaseSetupManager(mockCommandRunner.Object);

            mockCommandRunner.Verify(m => m.CreateDatabase(databaseFileName), Times.Never);
            mockCommandRunner.Verify(m => m.CreateCompanyTable(It.IsAny<SQLiteConnection>()), Times.Never);

            setupManager.InitializeDatabase(databaseFileName);

            mockCommandRunner.Verify(m => m.CreateDatabase(databaseFileName), Times.Once);
            mockCommandRunner.Verify(m => m.CreateCompanyTable(It.Is<SQLiteConnection>
                (n => n.ConnectionString == databaseConnectionString)), Times.Once);
        }
    }
}