using System.Data.SQLite;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.SqlDatabase;
using NeuralStocksTests.Testing;

namespace NeuralStocksTests.SqlDatabase
{
    [TestClass]
    public class SqlDatabaseManagerTest
    {
        [TestMethod]
        public void TestImplementsInterface()
        {
            MoreAssert.ImplementsInterface(typeof (ISqlDatabaseManager), typeof (SqlDatabaseManager));
        }

        [TestMethod]
        public void TestInitializeDatabaseCreatesNewDatabaseWhenFileDoesNotExist()
        {
            const string databaseFileName = "NeuralStocksDatabase.sqlite";

            if (File.Exists(databaseFileName)) File.Delete(databaseFileName);
            Assert.IsFalse(File.Exists(databaseFileName));

            var sqlDatabaseManager = new SqlDatabaseManager();
            sqlDatabaseManager.InitializeDatabase();

            Assert.IsTrue(File.Exists(databaseFileName));
        }

        [TestMethod]
        public void TestInitializeDatabaseCreatesInitialTable()
        {
            const string databaseFileName = "NeuralStocksDatabase.sqlite";
            const string databaseConnectionString = "Data Source=" + databaseFileName + ";Version=3;";

            Assert.IsTrue(File.Exists(databaseFileName));

            var sqlDatabaseManager = new SqlDatabaseManager();
            sqlDatabaseManager.InitializeDatabase();

            Assert.IsTrue(File.Exists(databaseFileName));

            const string checkInitialTableCommandString =
                "SELECT name FROM sqlite_master WHERE type='table' AND name='Company'";

            var connection = new SQLiteConnection(databaseConnectionString);

            connection.Open();

            var checkInitialTableCommand = new SQLiteCommand(checkInitialTableCommandString, connection);
            var checkInitialTableCommandReader = checkInitialTableCommand.ExecuteReader();

            try
            {
                Assert.IsTrue(checkInitialTableCommandReader.Read());
                Assert.AreEqual(1, checkInitialTableCommandReader.FieldCount);
                Assert.AreEqual("Company", checkInitialTableCommandReader["name"]);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}