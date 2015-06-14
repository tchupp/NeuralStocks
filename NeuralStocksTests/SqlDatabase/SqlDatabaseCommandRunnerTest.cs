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

            commandRunner.AddCompanyToTable(connection, companyLookupResponse1);
            commandRunner.AddCompanyToTable(connection, companyLookupResponse2);

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
        public void TestAddCompanyToTable_AlsoAddsNewQuoteHistoryTable()
        {
            const string createCompanyTableCommandString =
                "CREATE TABLE Company (name TEXT, symbol TEXT, firstDate TEXT, recentDate TEXT)";
            const string selectAllTablesCommandString =
                "SELECT * FROM sqlite_master WHERE type='table'";

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

            connection.Open();

            var createCompanyTableCommand = new SQLiteCommand(createCompanyTableCommandString, connection);
            createCompanyTableCommand.ExecuteNonQuery();

            connection.Close();

            var commandRunner = SqlDatabaseCommandRunner.Singleton;

            commandRunner.AddCompanyToTable(connection, companyLookupResponse1);
            commandRunner.AddCompanyToTable(connection, companyLookupResponse2);

            connection.Open();

            var selectAllTablesCommand = new SQLiteCommand(selectAllTablesCommandString, connection);
            var selectAllTablesCommandReader = selectAllTablesCommand.ExecuteReader();

            try
            {
                Assert.IsTrue(selectAllTablesCommandReader.Read());
                Assert.AreEqual("Company", selectAllTablesCommandReader["name"]);

                Assert.IsTrue(selectAllTablesCommandReader.Read());
                Assert.AreEqual(expectedSymbol1, selectAllTablesCommandReader["name"]);

                Assert.IsTrue(selectAllTablesCommandReader.Read());
                Assert.AreEqual(expectedSymbol2, selectAllTablesCommandReader["name"]);

                Assert.IsFalse(selectAllTablesCommandReader.Read());
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
            commandRunner.UpdateCompanyTimestamp(connection, quoteLookupResponse1);
            commandRunner.UpdateCompanyTimestamp(connection, quoteLookupResponse2);

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
            commandRunner.UpdateCompanyTimestamp(connection, quoteLookupResponse1);
            commandRunner.UpdateCompanyTimestamp(connection, quoteLookupResponse2);

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

        [TestMethod]
        public void TestGetCompanyLookupsFromTable_ReturnsCorrectCompanyLookupRequestList()
        {
            const string expectedSymbol1 = "AAPL";
            const string expectedSymbol2 = "NFLX";
            const string timestamp1 = "Tues Jun 16";
            const string timestamp2 = "Tues Jun 16";

            const string createCompanyTableCommandString =
                "CREATE TABLE Company (name TEXT, symbol TEXT, firstDate TEXT, recentDate TEXT)";
            const string addCompanyToTableCommandString1 = "INSERT INTO Company VALUES ('null', '" +
                                                           expectedSymbol1 + "', 'null', '" + timestamp1 + "')";
            const string addCompanyToTableCommandString2 = "INSERT INTO Company VALUES ('null', '" +
                                                           expectedSymbol2 + "', 'null', '" + timestamp2 + "')";

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
            var quoteLookupsFromTable = commandRunner.GetQuoteLookupsFromTable(connection);

            Assert.AreEqual(2, quoteLookupsFromTable.Count);

            Assert.IsInstanceOfType(quoteLookupsFromTable[0], typeof (QuoteLookupRequest));
            Assert.IsInstanceOfType(quoteLookupsFromTable[1], typeof (QuoteLookupRequest));

            Assert.AreEqual(expectedSymbol1, quoteLookupsFromTable[0].Company);
            Assert.AreEqual(timestamp1, quoteLookupsFromTable[0].Timestamp);

            Assert.AreEqual(expectedSymbol2, quoteLookupsFromTable[1].Company);
            Assert.AreEqual(timestamp2, quoteLookupsFromTable[1].Timestamp);
        }

        [TestMethod]
        public void TestAddQuoteResponseToTable()
        {
            const string expectedName1 = "Apple Inc";
            const string expectedSymbol1 = "AAPL";
            const string expectedTimestamp1 = "Thu Jun 4 00:00:00 UTC-04:00 2015";
            const double expectedLastPrice1 = 127.1f;
            const double expectedChange1 = 0.52f;
            const double expectedChangePercent1 = 0.0062f;

            const string expectedName2 = "Netflix";
            const string expectedSymbol2 = "NFLX";
            const string expectedTimestamp2 = "Monday Jun 1 12:51:43 UTC-04:00 2015";
            const double expectedLastPrice2 = 293.4f;
            const double expectedChange2 = 0.3f;
            const double expectedChangePercent2 = 0.0045f;

            const string createCompanyTableCommandString1 =
                "CREATE TABLE " + expectedSymbol1 +
                " (name TEXT, symbol TEXT, timestamp TEXT, lastPrice REAL, change REAL, changePercent REAL)";
            const string createCompanyTableCommandString2 =
                "CREATE TABLE " + expectedSymbol2 +
                " (name TEXT, symbol TEXT, timestamp TEXT, lastPrice REAL, change REAL, changePercent REAL)";

            const string selectAllTablesCommandString1 =
                "SELECT * FROM " + expectedSymbol1;
            const string selectAllTablesCommandString2 =
                "SELECT * FROM " + expectedSymbol2;

            var quoteLookupResponse1 = new QuoteLookupResponse
            {
                Name = expectedName1,
                Symbol = expectedSymbol1,
                Timestamp = expectedTimestamp1,
                LastPrice = expectedLastPrice1,
                Change = expectedChange1,
                ChangePercent = expectedChangePercent1
            };
            var quoteLookupResponse2 = new QuoteLookupResponse
            {
                Name = expectedName2,
                Symbol = expectedSymbol2,
                Timestamp = expectedTimestamp2,
                LastPrice = expectedLastPrice2,
                Change = expectedChange2,
                ChangePercent = expectedChangePercent2
            };

            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

            var connection = new SQLiteConnection(DatabaseConnectionString);

            connection.Open();

            var createCompanyTableCommand1 = new SQLiteCommand(createCompanyTableCommandString1, connection);
            createCompanyTableCommand1.ExecuteNonQuery();

            var createCompanyTableCommand2 = new SQLiteCommand(createCompanyTableCommandString2, connection);
            createCompanyTableCommand2.ExecuteNonQuery();

            connection.Close();

            var commandRunner = SqlDatabaseCommandRunner.Singleton;
            commandRunner.AddQuoteResponseToTable(connection, quoteLookupResponse1);
            commandRunner.AddQuoteResponseToTable(connection, quoteLookupResponse2);

            connection.Open();

            var selectAllTablesCommand1 = new SQLiteCommand(selectAllTablesCommandString1, connection);
            var selectAllTablesCommandReader1 = selectAllTablesCommand1.ExecuteReader();

            var selectAllTablesCommand2 = new SQLiteCommand(selectAllTablesCommandString2, connection);
            var selectAllTablesCommandReader2 = selectAllTablesCommand2.ExecuteReader();

            try
            {
                Assert.IsTrue(selectAllTablesCommandReader1.Read());
                Assert.AreEqual(expectedName1, selectAllTablesCommandReader1["name"]);
                Assert.AreEqual(expectedSymbol1, selectAllTablesCommandReader1["symbol"]);
                Assert.AreEqual(expectedTimestamp1, selectAllTablesCommandReader1["timestamp"]);
                Assert.AreEqual(expectedLastPrice1, (double) selectAllTablesCommandReader1["lastPrice"], 0.001);
                Assert.AreEqual(expectedChange1, (double) selectAllTablesCommandReader1["change"], 0.001);
                Assert.AreEqual(expectedChangePercent1, (double) selectAllTablesCommandReader1["changePercent"], 0.001);
                Assert.IsFalse(selectAllTablesCommandReader1.Read());

                Assert.IsTrue(selectAllTablesCommandReader2.Read());
                Assert.AreEqual(expectedName2, selectAllTablesCommandReader2["name"]);
                Assert.AreEqual(expectedSymbol2, selectAllTablesCommandReader2["symbol"]);
                Assert.AreEqual(expectedTimestamp2, selectAllTablesCommandReader2["timestamp"]);
                Assert.AreEqual(expectedLastPrice2, (double) selectAllTablesCommandReader2["lastPrice"], 0.001);
                Assert.AreEqual(expectedChange2, (double) selectAllTablesCommandReader2["change"], 0.001);
                Assert.AreEqual(expectedChangePercent2, (double) selectAllTablesCommandReader2["changePercent"], 0.001);
                Assert.IsFalse(selectAllTablesCommandReader2.Read());
            }
            finally
            {
                connection.Close();
            }
        }
    }
}