using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.DatabaseLayer.Tests.Sqlite
{
    [TestClass]
    public class DatabaseNameTest : AssertTestClass
    {
        [TestMethod, TestCategory("Database")]
        public void TestGetsNameAndConnectionString()
        {
            const string databaseName1 = "TestDatabase.sqlite";
            const string databaseConnectionString1 = "Data Source=" + databaseName1 + ";Version=3;";

            var connectionName = new DatabaseName
            {
                Name = databaseName1
            };

            Assert.AreEqual(databaseName1, connectionName.Name);
            Assert.AreEqual(databaseConnectionString1, connectionName.DatabaseConnectionString);


            const string databaseName2 = "AnotherDatabase.sqlite";
            const string databaseConnectionString2 = "Data Source=" + databaseName2 + ";Version=3;";

            connectionName = new DatabaseName
            {
                Name = databaseName2
            };

            Assert.AreEqual(databaseName2, connectionName.Name);
            Assert.AreEqual(databaseConnectionString2, connectionName.DatabaseConnectionString);
        }
    }
}