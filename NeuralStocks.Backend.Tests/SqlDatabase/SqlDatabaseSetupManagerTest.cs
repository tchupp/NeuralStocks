using System.Data.SQLite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralStocks.Backend.SqlDatabase;
using NeuralStocks.Backend.Tests.Testing;

namespace NeuralStocks.Backend.Tests.SqlDatabase
{
    [TestClass]
    public class SqlDatabaseSetupManagerTest : AssertTestClass
    {
        [TestMethod]
        public void TestImplementsInterface()
        {
            ImplementsInterface(typeof (ISqlDatabaseSetupManager), typeof (SqlDatabaseSetupManager));
        }

        [TestMethod]
        public void TestInitializeDatabaseCreatesInitialDatabaseWithEmptyTable()
        {
            const string databaseFileName = "TestStocksDatabase.sqlite";
            const string databaseConnectionString = "Data Source=" + databaseFileName + ";Version=3;";

            var mockCommandRunner = new Mock<ISqlDatabaseCommandRunner>();
            var setupManager = new SqlDatabaseSetupManager(mockCommandRunner.Object);

            mockCommandRunner.Verify(m => m.CreateDatabase(databaseFileName), Times.Never);
            mockCommandRunner.Verify(m => m.CreateCompanyTable(It.IsAny<SQLiteConnection>()), Times.Never);

            setupManager.InitializeDatabase(databaseFileName);

            mockCommandRunner.Verify(m => m.CreateDatabase(databaseFileName), Times.Once);
            mockCommandRunner.Verify(m => m.CreateCompanyTable(It.Is<SQLiteConnection>
                (n => n.ConnectionString == databaseConnectionString)), Times.Once);
        }
    }
}