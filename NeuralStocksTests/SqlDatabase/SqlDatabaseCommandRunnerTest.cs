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
        private const string DatabaseFileName = "TestStocksDatabase.sqlite";
        private const string DatabaseConnectionString = "Data Source=" + DatabaseFileName + ";Version=3;";
        private const string SelectAllFromCompanyTableCommandString = "SELECT * FROM Company";

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
        public void TestCreateDatabase()
        {
            const string databaseFileName = "TestStocksDatabase.sqlite";

            if (File.Exists(databaseFileName)) File.Delete(databaseFileName);
            Assert.IsFalse(File.Exists(databaseFileName));

            var commandRunner = SqlDatabaseCommandRunner.Singleton;
            commandRunner.CreateDatabase(databaseFileName);

            Assert.IsTrue(File.Exists(databaseFileName));
        }

        [TestMethod]
        public void TestCreateCompanyTable()
        {
            const string selectAllTablesByNameCompanyCommandString =
                "SELECT name FROM sqlite_master WHERE type='table' AND name='Company'";

            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

            var connection = new SQLiteConnection(DatabaseConnectionString);

            var commandRunner = SqlDatabaseCommandRunner.Singleton;
            commandRunner.CreateCompanyTable(connection);

            connection.Open();

            var selectAllTablesByNameCompanyCommand = new SQLiteCommand(selectAllTablesByNameCompanyCommandString,
                connection);
            var selectAllTablesByNameCompanyCommandReader = selectAllTablesByNameCompanyCommand.ExecuteReader();

            try
            {
                Assert.IsTrue(selectAllTablesByNameCompanyCommandReader.Read());
                Assert.AreEqual(1, selectAllTablesByNameCompanyCommandReader.FieldCount);
                Assert.AreEqual("Company", selectAllTablesByNameCompanyCommandReader["name"]);

                Assert.IsFalse(selectAllTablesByNameCompanyCommandReader.Read());
            }
            finally
            {
                connection.Close();
            }
        }

        [TestMethod]
        public void TestAddCompanyToTable_CorrectlyAddsACompanyWithNameAndSymbol_AndNullDates()
        {
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

            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

            var connection = new SQLiteConnection(DatabaseConnectionString);

            var commandRunner = SqlDatabaseCommandRunner.Singleton;
            commandRunner.CreateCompanyTable(connection);

            commandRunner.AddCompanyToTable(companyLookupResponse1, connection);
            commandRunner.AddCompanyToTable(companyLookupResponse2, connection);

            connection.Open();

            var checkCompanyTableFieldsCommand = new SQLiteCommand(SelectAllFromCompanyTableCommandString, connection);
            var checkCompanyTableFieldsCommandReader = checkCompanyTableFieldsCommand.ExecuteReader();

            try
            {
                Assert.IsTrue(checkCompanyTableFieldsCommandReader.Read());
                Assert.AreEqual(4, checkCompanyTableFieldsCommandReader.FieldCount);
                Assert.AreEqual(expectedName1, checkCompanyTableFieldsCommandReader["name"]);
                Assert.AreEqual(expectedSymbol1, checkCompanyTableFieldsCommandReader["symbol"]);
                Assert.AreEqual("null", checkCompanyTableFieldsCommandReader["firstDate"]);
                Assert.AreEqual("null", checkCompanyTableFieldsCommandReader["recentDate"]);

                Assert.IsTrue(checkCompanyTableFieldsCommandReader.Read());
                Assert.AreEqual(4, checkCompanyTableFieldsCommandReader.FieldCount);
                Assert.AreEqual(expectedName2, checkCompanyTableFieldsCommandReader["name"]);
                Assert.AreEqual(expectedSymbol2, checkCompanyTableFieldsCommandReader["symbol"]);
                Assert.AreEqual("null", checkCompanyTableFieldsCommandReader["firstDate"]);
                Assert.AreEqual("null", checkCompanyTableFieldsCommandReader["recentDate"]);

                Assert.IsFalse(checkCompanyTableFieldsCommandReader.Read());
            }
            finally
            {
                connection.Close();
            }
        }

        [TestMethod]
        public void TestUpdateCompanyTimestamp_SetsRecentDateAsTimestampOfQuote_AndFirstDate_FirstDateNull()
        {
            const string expectedName1 = "Apple Inc";
            const string expectedSymbol1 = "AAPL";
            const string expectedTimestamp1 = "Thu Jun 4 00:00:00 UTC-04:00 2015";
            const string expectedName2 = "Netflix";
            const string expectedSymbol2 = "NFLX";
            const string expectedTimestamp2 = "Monday Jun 1 12:51:43 UTC-04:00 2015";

            var quoteLookupResponse1 = new QuoteLookupResponse
            {
                Name = expectedName1,
                Symbol = expectedSymbol1,
                Timestamp = expectedTimestamp1
            };
            var quoteLookupResponse2 = new QuoteLookupResponse
            {
                Name = expectedName2,
                Symbol = expectedSymbol2,
                Timestamp = expectedTimestamp2
            };

            const string createCompanyTableCommandString =
                "CREATE TABLE Company (name TEXT, symbol TEXT, firstDate TEXT, recentDate TEXT)";
            var addCompanyToTableCommandString1 =
                "INSERT INTO Company VALUES ('" +
                quoteLookupResponse1.Name + "', '" +
                quoteLookupResponse1.Symbol + "', 'null', 'null')";
            var addCompanyToTableCommandString2 =
                "INSERT INTO Company VALUES ('" +
                quoteLookupResponse2.Name + "', '" +
                quoteLookupResponse2.Symbol + "', 'null', 'null')";

            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

            var connection = new SQLiteConnection(DatabaseConnectionString);

            connection.Open();

            var createCompanyTableCommand = new SQLiteCommand(createCompanyTableCommandString, connection);
            createCompanyTableCommand.ExecuteNonQuery();

            var addCompanyToTableCommand1 = new SQLiteCommand(addCompanyToTableCommandString1, connection);
            addCompanyToTableCommand1.ExecuteNonQuery();

            var addCompanyToTableCommand2 = new SQLiteCommand(addCompanyToTableCommandString2, connection);
            addCompanyToTableCommand2.ExecuteNonQuery();

            connection.Close();

            var commandRunner = SqlDatabaseCommandRunner.Singleton;
            commandRunner.UpdateCompanyTimestamp(quoteLookupResponse1, connection);
            commandRunner.UpdateCompanyTimestamp(quoteLookupResponse2, connection);

            connection.Open();

            var selectAllFromCompanyTableCommand = new SQLiteCommand(SelectAllFromCompanyTableCommandString, connection);
            var selectAllFromCompanyTableCommandReader = selectAllFromCompanyTableCommand.ExecuteReader();

            try
            {
                Assert.IsTrue(selectAllFromCompanyTableCommandReader.Read());
                Assert.AreEqual(4, selectAllFromCompanyTableCommandReader.FieldCount);
                Assert.AreEqual(expectedName1, selectAllFromCompanyTableCommandReader["name"]);
                Assert.AreEqual(expectedSymbol1, selectAllFromCompanyTableCommandReader["symbol"]);
                Assert.AreEqual(expectedTimestamp1, selectAllFromCompanyTableCommandReader["firstDate"]);
                Assert.AreEqual(expectedTimestamp1, selectAllFromCompanyTableCommandReader["recentDate"]);

                Assert.IsTrue(selectAllFromCompanyTableCommandReader.Read());
                Assert.AreEqual(4, selectAllFromCompanyTableCommandReader.FieldCount);
                Assert.AreEqual(expectedName2, selectAllFromCompanyTableCommandReader["name"]);
                Assert.AreEqual(expectedSymbol2, selectAllFromCompanyTableCommandReader["symbol"]);
                Assert.AreEqual(expectedTimestamp2, selectAllFromCompanyTableCommandReader["firstDate"]);
                Assert.AreEqual(expectedTimestamp2, selectAllFromCompanyTableCommandReader["recentDate"]);

                Assert.IsFalse(selectAllFromCompanyTableCommandReader.Read());
            }
            finally
            {
                connection.Close();
            }
        }

        [TestMethod]
        public void TestUpdateCompanyTimestamp_SetsRecentDateAsTimestampOfQuote_FirstDateExists()
        {
            const string expectedName1 = "Apple Inc";
            const string expectedSymbol1 = "AAPL";
            const string initialTimestamp1 = "Tues May 26 05:17:42 UTC-04:00 2015";
            const string expectedTimestamp1 = "Thu Jun 4 00:00:00 UTC-04:00 2015";
            const string expectedName2 = "Netflix";
            const string expectedSymbol2 = "NFLX";
            const string initialTimestamp2 = "Wed May 27 16:47:35 UTC-04:00 2015";
            const string expectedTimestamp2 = "Monday Jun 1 12:51:43 UTC-04:00 2015";

            var quoteLookupResponse1 = new QuoteLookupResponse
            {
                Name = expectedName1,
                Symbol = expectedSymbol1,
                Timestamp = expectedTimestamp1
            };
            var quoteLookupResponse2 = new QuoteLookupResponse
            {
                Name = expectedName2,
                Symbol = expectedSymbol2,
                Timestamp = expectedTimestamp2
            };

            const string createCompanyTableCommandString =
                "CREATE TABLE Company (name TEXT, symbol TEXT, firstDate TEXT, recentDate TEXT)";
            var addCompanyToTableCommandString1 =
                "INSERT INTO Company VALUES ('" +
                quoteLookupResponse1.Name + "', '" +
                quoteLookupResponse1.Symbol + "', '" +
                initialTimestamp1 + "', '" +
                initialTimestamp1 + "')";
            var addCompanyToTableCommandString2 =
                "INSERT INTO Company VALUES ('" +
                quoteLookupResponse2.Name + "', '" +
                quoteLookupResponse2.Symbol + "', '" +
                initialTimestamp2 + "', '" +
                initialTimestamp2 + "')";

            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

            var connection = new SQLiteConnection(DatabaseConnectionString);

            connection.Open();

            var createCompanyTableCommand = new SQLiteCommand(createCompanyTableCommandString, connection);
            createCompanyTableCommand.ExecuteNonQuery();

            var addCompanyToTableCommand1 = new SQLiteCommand(addCompanyToTableCommandString1, connection);
            addCompanyToTableCommand1.ExecuteNonQuery();

            var addCompanyToTableCommand2 = new SQLiteCommand(addCompanyToTableCommandString2, connection);
            addCompanyToTableCommand2.ExecuteNonQuery();

            connection.Close();

            var commandRunner = SqlDatabaseCommandRunner.Singleton;
            commandRunner.UpdateCompanyTimestamp(quoteLookupResponse1, connection);
            commandRunner.UpdateCompanyTimestamp(quoteLookupResponse2, connection);

            connection.Open();

            var selectAllFromCompanyTableCommand = new SQLiteCommand(SelectAllFromCompanyTableCommandString, connection);
            var selectAllFromCompanyTableCommandReader = selectAllFromCompanyTableCommand.ExecuteReader();

            try
            {
                Assert.IsTrue(selectAllFromCompanyTableCommandReader.Read());
                Assert.AreEqual(4, selectAllFromCompanyTableCommandReader.FieldCount);
                Assert.AreEqual(expectedName1, selectAllFromCompanyTableCommandReader["name"]);
                Assert.AreEqual(expectedSymbol1, selectAllFromCompanyTableCommandReader["symbol"]);
                Assert.AreEqual(initialTimestamp1, selectAllFromCompanyTableCommandReader["firstDate"]);
                Assert.AreEqual(expectedTimestamp1, selectAllFromCompanyTableCommandReader["recentDate"]);

                Assert.IsTrue(selectAllFromCompanyTableCommandReader.Read());
                Assert.AreEqual(4, selectAllFromCompanyTableCommandReader.FieldCount);
                Assert.AreEqual(expectedName2, selectAllFromCompanyTableCommandReader["name"]);
                Assert.AreEqual(expectedSymbol2, selectAllFromCompanyTableCommandReader["symbol"]);
                Assert.AreEqual(initialTimestamp2, selectAllFromCompanyTableCommandReader["firstDate"]);
                Assert.AreEqual(expectedTimestamp2, selectAllFromCompanyTableCommandReader["recentDate"]);

                Assert.IsFalse(selectAllFromCompanyTableCommandReader.Read());
            }
            finally
            {
                connection.Close();
            }
        }
    }
}