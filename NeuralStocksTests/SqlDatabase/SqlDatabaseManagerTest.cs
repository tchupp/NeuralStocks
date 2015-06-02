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
        public void TestInitializeDatabase()
        {
            var sqlDatabaseManager = new SqlDatabaseManager();
            sqlDatabaseManager.InitializeDatabase();

            Assert.IsTrue(File.Exists("TestStocksDatabase.sqlite"));
        }
    }
}