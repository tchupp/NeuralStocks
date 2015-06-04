using System.Data.SQLite;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.ApiCommunication;
using NeuralStocks.SqlDatabase;
using NeuralStocksTests.Testing;

namespace NeuralStocksTests.SqlDatabase
{
    [TestClass]
    public class SqlDatabaseCommandRunnerTest
    {
        [TestMethod]
        public void TestImplementsInterface()
        {
            MoreAssert.ImplementsInterface(
                typeof (ISqlDatabaseCommandRunner), typeof (SqlDatabaseCommandRunner));
        }

        [TestMethod]
        public void TestSqlDatabaseCommandRunnerIsSingleton()
        {
            MoreAssert.PrivateContructor(typeof (SqlDatabaseCommandRunner));
            Assert.AreSame(SqlDatabaseCommandRunner.Singleton, SqlDatabaseCommandRunner.Singleton);
        }

        [TestMethod]
        public void TestCreateCompanyTable()
        {
            const string databaseFileName = "TestStocksDatabase.sqlite";
            const string databaseConnectionString = "Data Source=" + databaseFileName + ";Version=3;";
            const string checkCompanyTableExistsCommandString =
                "SELECT name FROM sqlite_master WHERE type='table' AND name='Company'";

            SQLiteConnection.CreateFile(databaseFileName);
            Assert.IsTrue(File.Exists(databaseFileName));

            var connection = new SQLiteConnection(databaseConnectionString);

            var commandRunner = SqlDatabaseCommandRunner.Singleton;
            commandRunner.CreateCompanyTable(connection);

            connection.Open();

            var checkCompanyTableExistsCommand = new SQLiteCommand(checkCompanyTableExistsCommandString, connection);
            var checkCompanyTableExistsCommandReader = checkCompanyTableExistsCommand.ExecuteReader();

            try
            {
                Assert.IsTrue(checkCompanyTableExistsCommandReader.Read());
                Assert.AreEqual(1, checkCompanyTableExistsCommandReader.FieldCount);
                Assert.AreEqual("Company", checkCompanyTableExistsCommandReader["name"]);

                Assert.IsFalse(checkCompanyTableExistsCommandReader.Read());
            }
            finally
            {
                connection.Close();
            }
        }

        [TestMethod]
        public void TestAddCompanyToTable()
        {
            const string databaseFileName = "TestStocksDatabase.sqlite";
            const string databaseConnectionString = "Data Source=" + databaseFileName + ";Version=3;";
            const string checkCompanyTableFieldsCommandString = "SELECT * FROM Company";

            const string expectedName1 = "Apple Inc";
            const string expectedSymbol1 = "AAPL";
            const string expectedName2 = "Netflix";
            const string expectedSymbol2 = "NFLX";

            var companyLookupResponse1 = new CompanyLookupResponse
            {
                Name = expectedName1,
                Symbol = expectedSymbol1
            };
            var companyLookupResponse2 = new CompanyLookupResponse
            {
                Name = expectedName2,
                Symbol = expectedSymbol2
            };

            SQLiteConnection.CreateFile(databaseFileName);
            Assert.IsTrue(File.Exists(databaseFileName));

            var connection = new SQLiteConnection(databaseConnectionString);

            var commandRunner = SqlDatabaseCommandRunner.Singleton;
            commandRunner.CreateCompanyTable(connection);

            commandRunner.AddCompanyToTable(companyLookupResponse1, connection);
            commandRunner.AddCompanyToTable(companyLookupResponse2, connection);

            connection.Open();

            var checkCompanyTableFieldsCommand = new SQLiteCommand(checkCompanyTableFieldsCommandString, connection);
            var checkCompanyTableFieldsCommandReader = checkCompanyTableFieldsCommand.ExecuteReader();

            try
            {
                Assert.IsTrue(checkCompanyTableFieldsCommandReader.Read());
                Assert.AreEqual(4, checkCompanyTableFieldsCommandReader.FieldCount);
                Assert.AreEqual(expectedName1, checkCompanyTableFieldsCommandReader["name"]);
                Assert.AreEqual(expectedSymbol1, checkCompanyTableFieldsCommandReader["Symbol"]);
                Assert.AreEqual("null", checkCompanyTableFieldsCommandReader["firstDate"]);
                Assert.AreEqual("null", checkCompanyTableFieldsCommandReader["recentDate"]);

                Assert.IsTrue(checkCompanyTableFieldsCommandReader.Read());
                Assert.AreEqual(4, checkCompanyTableFieldsCommandReader.FieldCount);
                Assert.AreEqual(expectedName2, checkCompanyTableFieldsCommandReader["name"]);
                Assert.AreEqual(expectedSymbol2, checkCompanyTableFieldsCommandReader["Symbol"]);
                Assert.AreEqual("null", checkCompanyTableFieldsCommandReader["firstDate"]);
                Assert.AreEqual("null", checkCompanyTableFieldsCommandReader["recentDate"]);

                Assert.IsFalse(checkCompanyTableFieldsCommandReader.Read());
            }
            finally
            {
                connection.Close();
            }
        }
    }
}