using System;
using System.Data.SQLite;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.DatabaseLayer.Tests.Sqlite
{
    [TestClass]
    public class DatabaseCommandTest : AssertTestClass
    {
        private const string DatabaseFileName = "TestStocksDatabase.sqlite";
        private const string DatabaseConnectionString = "Data Source=" + DatabaseFileName + ";Version=3;";
        private const string SelectAllFromCompanyTableCommandString = "SELECT * FROM Company";

        [TestCleanup, TestCategory("Database")]
        public void TearDown()
        {
            GC.Collect();
            GC.WaitForFullGCComplete();
            GC.WaitForPendingFinalizers();
        }

        [TestMethod, TestCategory("Database")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IDatabaseCommand), typeof (DatabaseCommand));
        }

        [TestMethod, TestCategory("Database")]
        public void TestGetsWrappedCommand()
        {
            using (var wrappedCommand = new SQLiteCommand())
            {
                var command = new DatabaseCommand(wrappedCommand);

                Assert.AreSame(wrappedCommand, command.WrappedCommand);
            }
        }

        [TestMethod, TestCategory("Database Funcational Test")]
        public void TestCreateCompanyLookupTable()
        {
            SetupCleanTestingDatabase();

            using (var connection = new SQLiteConnection(DatabaseConnectionString))
            {
                var command = connection.CreateCommand();

                var factory = DatabaseCommandStringFactory.Singleton;
                command.CommandText = factory.BuildCreateCompanyLookupTableCommandString();

                var databaseCommand = new DatabaseCommand(command);

                connection.Open();
                databaseCommand.ExecuteNonQuery();
                connection.Close();

                const string selectAllTablesByNameCompanyCommandString =
                    "SELECT name FROM sqlite_master WHERE type='table' AND name='Company'";
                using (var allByNameCommand = new SQLiteCommand(selectAllTablesByNameCompanyCommandString, connection))
                {
                    connection.Open();
                    var selectAllTablesByNameCompanyCommandReader = allByNameCommand.ExecuteReader();

                    Assert.IsTrue(selectAllTablesByNameCompanyCommandReader.Read());
                    Assert.AreEqual(1, selectAllTablesByNameCompanyCommandReader.FieldCount);
                    Assert.AreEqual("Company", selectAllTablesByNameCompanyCommandReader["name"]);
                    Assert.IsFalse(selectAllTablesByNameCompanyCommandReader.Read());
                    connection.Close();
                }


                using (var allFromCompanyCommand = new SQLiteCommand(SelectAllFromCompanyTableCommandString, connection)
                    )
                {
                    connection.Open();
                    var selectAllFromCompanyTableCommandReader = allFromCompanyCommand.ExecuteReader();

                    Assert.IsFalse(selectAllFromCompanyTableCommandReader.Read());
                    Assert.AreEqual(5, selectAllFromCompanyTableCommandReader.FieldCount);
                    Assert.AreEqual("name", selectAllFromCompanyTableCommandReader.GetName(0));
                    Assert.AreEqual("TEXT", selectAllFromCompanyTableCommandReader.GetDataTypeName(0));
                    Assert.AreEqual("symbol", selectAllFromCompanyTableCommandReader.GetName(1));
                    Assert.AreEqual("TEXT", selectAllFromCompanyTableCommandReader.GetDataTypeName(1));
                    Assert.AreEqual("firstDate", selectAllFromCompanyTableCommandReader.GetName(2));
                    Assert.AreEqual("TEXT", selectAllFromCompanyTableCommandReader.GetDataTypeName(2));
                    Assert.AreEqual("recentDate", selectAllFromCompanyTableCommandReader.GetName(3));
                    Assert.AreEqual("TEXT", selectAllFromCompanyTableCommandReader.GetDataTypeName(3));
                    Assert.AreEqual("collect", selectAllFromCompanyTableCommandReader.GetName(4));
                    Assert.AreEqual("INTEGER", selectAllFromCompanyTableCommandReader.GetDataTypeName(4));
                    connection.Close();
                }
            }
        }

        [TestMethod, TestCategory("Database Funcational Test")]
        public void TestCreateQuoteHistoryTable()
        {
            SetupCleanTestingDatabase();

            var company1 = new CompanyLookupResponse {Symbol = "NFLX"};
            var company2 = new CompanyLookupResponse {Symbol = "AAPL"};

            using (var connection = new SQLiteConnection(DatabaseConnectionString))
            {
                var command1 = connection.CreateCommand();
                var command2 = connection.CreateCommand();

                var factory = DatabaseCommandStringFactory.Singleton;
                command1.CommandText = factory.BuildCreateQuoteHistoryTableCommandString(company1);
                command2.CommandText = factory.BuildCreateQuoteHistoryTableCommandString(company2);

                var databaseCommand1 = new DatabaseCommand(command1);
                var databaseCommand2 = new DatabaseCommand(command2);

                connection.Open();
                databaseCommand1.ExecuteNonQuery();
                databaseCommand2.ExecuteNonQuery();
                connection.Close();

                const string selectAllTablesCommandString = "SELECT * FROM sqlite_master WHERE type='table'";
                using (var selectAllTablesCommand = new SQLiteCommand(selectAllTablesCommandString, connection))
                {
                    connection.Open();
                    var selectAllTablesCommandReader = selectAllTablesCommand.ExecuteReader();

                    Assert.IsTrue(selectAllTablesCommandReader.Read());
                    Assert.AreEqual(company1.Symbol, selectAllTablesCommandReader["name"]);
                    Assert.IsTrue(selectAllTablesCommandReader.Read());
                    Assert.AreEqual(company2.Symbol, selectAllTablesCommandReader["name"]);
                    Assert.IsFalse(selectAllTablesCommandReader.Read());

                    connection.Close();
                }

                const string selectAllTablesCommandString1 = "SELECT * FROM AAPL";
                using (var selectAllTablesCommand1 = new SQLiteCommand(selectAllTablesCommandString1, connection))
                {
                    connection.Open();
                    var selectAllTablesCommandReader1 = selectAllTablesCommand1.ExecuteReader();

                    Assert.IsFalse(selectAllTablesCommandReader1.Read());
                    Assert.AreEqual(6, selectAllTablesCommandReader1.FieldCount);
                    Assert.AreEqual("name", selectAllTablesCommandReader1.GetName(0));
                    Assert.AreEqual("TEXT", selectAllTablesCommandReader1.GetDataTypeName(0));
                    Assert.AreEqual("symbol", selectAllTablesCommandReader1.GetName(1));
                    Assert.AreEqual("TEXT", selectAllTablesCommandReader1.GetDataTypeName(1));
                    Assert.AreEqual("timestamp", selectAllTablesCommandReader1.GetName(2));
                    Assert.AreEqual("TEXT", selectAllTablesCommandReader1.GetDataTypeName(2));
                    Assert.AreEqual("lastPrice", selectAllTablesCommandReader1.GetName(3));
                    Assert.AreEqual("REAL", selectAllTablesCommandReader1.GetDataTypeName(3));
                    Assert.AreEqual("change", selectAllTablesCommandReader1.GetName(4));
                    Assert.AreEqual("REAL", selectAllTablesCommandReader1.GetDataTypeName(4));
                    Assert.AreEqual("changePercent", selectAllTablesCommandReader1.GetName(5));
                    Assert.AreEqual("REAL", selectAllTablesCommandReader1.GetDataTypeName(5));

                    connection.Close();
                }

                const string selectAllTablesCommandString2 = "SELECT * FROM NFLX";

                using (var selectAllTablesCommand2 = new SQLiteCommand(selectAllTablesCommandString2, connection))
                {
                    connection.Open();
                    var selectAllTablesCommandReader2 = selectAllTablesCommand2.ExecuteReader();

                    Assert.IsFalse(selectAllTablesCommandReader2.Read());
                    Assert.AreEqual(6, selectAllTablesCommandReader2.FieldCount);
                    Assert.AreEqual("name", selectAllTablesCommandReader2.GetName(0));
                    Assert.AreEqual("TEXT", selectAllTablesCommandReader2.GetDataTypeName(0));
                    Assert.AreEqual("symbol", selectAllTablesCommandReader2.GetName(1));
                    Assert.AreEqual("TEXT", selectAllTablesCommandReader2.GetDataTypeName(1));
                    Assert.AreEqual("timestamp", selectAllTablesCommandReader2.GetName(2));
                    Assert.AreEqual("TEXT", selectAllTablesCommandReader2.GetDataTypeName(2));
                    Assert.AreEqual("lastPrice", selectAllTablesCommandReader2.GetName(3));
                    Assert.AreEqual("REAL", selectAllTablesCommandReader2.GetDataTypeName(3));
                    Assert.AreEqual("change", selectAllTablesCommandReader2.GetName(4));
                    Assert.AreEqual("REAL", selectAllTablesCommandReader2.GetDataTypeName(4));
                    Assert.AreEqual("changePercent", selectAllTablesCommandReader2.GetName(5));
                    Assert.AreEqual("REAL", selectAllTablesCommandReader2.GetDataTypeName(5));

                    connection.Close();
                }
            }
        }

        [TestMethod, TestCategory("Database Funcational Test")]
        public void TestInsertCompanyToLookupTable()
        {
            SetupCleanTestingDatabase();

            var company1 = new CompanyLookupResponse {Symbol = "NFLX", Name = "Apple Inc"};
            var company2 = new CompanyLookupResponse {Symbol = "AAPL", Name = "Netflix"};

            using (var connection = new SQLiteConnection(DatabaseConnectionString))
            {
                SetupCleanCompanyLookupTable(connection);

                var command1 = connection.CreateCommand();
                var command2 = connection.CreateCommand();

                var factory = DatabaseCommandStringFactory.Singleton;
                command1.CommandText = factory.BuildInsertCompanyToLookupTableCommandString(company1);
                command2.CommandText = factory.BuildInsertCompanyToLookupTableCommandString(company2);

                var databaseCommand1 = new DatabaseCommand(command1);
                var databaseCommand2 = new DatabaseCommand(command2);

                connection.Open();
                databaseCommand1.ExecuteNonQuery();
                databaseCommand2.ExecuteNonQuery();
                connection.Close();

                using (var command = new SQLiteCommand(SelectAllFromCompanyTableCommandString, connection))
                {
                    connection.Open();
                    var checkCompanyTableFieldsCommandReader = command.ExecuteReader();

                    Assert.IsTrue(checkCompanyTableFieldsCommandReader.Read());
                    Assert.AreEqual(5, checkCompanyTableFieldsCommandReader.FieldCount);
                    Assert.AreEqual(company1.Name, checkCompanyTableFieldsCommandReader["name"]);
                    Assert.AreEqual(company1.Symbol, checkCompanyTableFieldsCommandReader["symbol"]);
                    Assert.AreEqual("null", checkCompanyTableFieldsCommandReader["firstDate"]);
                    Assert.AreEqual("null", checkCompanyTableFieldsCommandReader["recentDate"]);
                    Assert.AreEqual(1, checkCompanyTableFieldsCommandReader["collect"]);

                    Assert.IsTrue(checkCompanyTableFieldsCommandReader.Read());
                    Assert.AreEqual(5, checkCompanyTableFieldsCommandReader.FieldCount);
                    Assert.AreEqual(company2.Name, checkCompanyTableFieldsCommandReader["name"]);
                    Assert.AreEqual(company2.Symbol, checkCompanyTableFieldsCommandReader["symbol"]);
                    Assert.AreEqual("null", checkCompanyTableFieldsCommandReader["firstDate"]);
                    Assert.AreEqual("null", checkCompanyTableFieldsCommandReader["recentDate"]);
                    Assert.AreEqual(1, checkCompanyTableFieldsCommandReader["collect"]);

                    Assert.IsFalse(checkCompanyTableFieldsCommandReader.Read());

                    connection.Close();
                }
            }
        }

        [TestMethod, TestCategory("Database Funcational Test")]
        public void TestInsertQuoteToHistoryTable()
        {
            SetupCleanTestingDatabase();

            var response1 = new QuoteLookupResponse
            {
                Name = "Apple Inc",
                Symbol = "AAPL",
                Timestamp = "Jun 4 00:00:00",
                LastPrice = 127.1f,
                Change = 0.52f,
                ChangePercent = 0.0062f
            };
            var response2 = new QuoteLookupResponse
            {
                Name = "Netflix",
                Symbol = "NFLX",
                Timestamp = "Jun 1 12:51:43",
                LastPrice = 293.4f,
                Change = 0.3f,
                ChangePercent = 0.0045f
            };

            using (var connection = new SQLiteConnection(DatabaseConnectionString))
            {
                SetupCleanQuoteHistoryTable(connection, "AAPL");
                SetupCleanQuoteHistoryTable(connection, "NFLX");

                var command1 = connection.CreateCommand();
                var command2 = connection.CreateCommand();

                var factory = DatabaseCommandStringFactory.Singleton;
                command1.CommandText = factory.BuildInsertQuoteToHistoryTableCommandString(response1);
                command2.CommandText = factory.BuildInsertQuoteToHistoryTableCommandString(response2);

                var databaseCommand1 = new DatabaseCommand(command1);
                var databaseCommand2 = new DatabaseCommand(command2);

                connection.Open();
                databaseCommand1.ExecuteNonQuery();
                databaseCommand2.ExecuteNonQuery();
                connection.Close();

                const string selectAllTablesCommandString1 = "SELECT * FROM AAPL";
                using (var selectAllTablesCommand1 = new SQLiteCommand(selectAllTablesCommandString1, connection))
                {
                    connection.Open();
                    var selectAllTablesCommandReader1 = selectAllTablesCommand1.ExecuteReader();

                    Assert.IsTrue(selectAllTablesCommandReader1.Read());
                    Assert.AreEqual(response1.Name, selectAllTablesCommandReader1["name"]);
                    Assert.AreEqual(response1.Symbol, selectAllTablesCommandReader1["symbol"]);
                    Assert.AreEqual(response1.Timestamp, selectAllTablesCommandReader1["timestamp"]);
                    Assert.AreEqual(response1.LastPrice, (double) selectAllTablesCommandReader1["lastPrice"], 0.001);
                    Assert.AreEqual(response1.Change, (double) selectAllTablesCommandReader1["change"], 0.001);
                    Assert.AreEqual(response1.ChangePercent, (double) selectAllTablesCommandReader1["changePercent"],
                        0.001);
                    Assert.IsFalse(selectAllTablesCommandReader1.Read());

                    connection.Close();
                }

                const string selectAllTablesCommandString2 = "SELECT * FROM NFLX";
                using (var selectAllTablesCommand2 = new SQLiteCommand(selectAllTablesCommandString2, connection))
                {
                    connection.Open();
                    var selectAllTablesCommandReader2 = selectAllTablesCommand2.ExecuteReader();

                    Assert.IsTrue(selectAllTablesCommandReader2.Read());
                    Assert.AreEqual(response2.Name, selectAllTablesCommandReader2["name"]);
                    Assert.AreEqual(response2.Symbol, selectAllTablesCommandReader2["symbol"]);
                    Assert.AreEqual(response2.Timestamp, selectAllTablesCommandReader2["timestamp"]);
                    Assert.AreEqual(response2.LastPrice, (double) selectAllTablesCommandReader2["lastPrice"], 0.001);
                    Assert.AreEqual(response2.Change, (double) selectAllTablesCommandReader2["change"], 0.001);
                    Assert.AreEqual(response2.ChangePercent, (double) selectAllTablesCommandReader2["changePercent"],
                        0.001);
                    Assert.IsFalse(selectAllTablesCommandReader2.Read());

                    connection.Close();
                }
            }
        }

        [TestMethod, TestCategory("Database Funcational Test")]
        public void TestUpdateCompanyFirstTimestamp()
        {
            SetupCleanTestingDatabase();
            using (var connection = new SQLiteConnection(DatabaseConnectionString))
            {
                SetupCleanCompanyLookupTable(connection);

                var response1 = new QuoteLookupResponse {Name = "Apple", Symbol = "AAPL", Timestamp = "Jun 4 00:00:00"};
                var response2 = new QuoteLookupResponse {Name = "Netflix", Symbol = "NFLX", Timestamp = "Jun 1 2:51:43"};

                InsertCompanyToLookupTable(connection, response1);
                InsertCompanyToLookupTable(connection, response2);

                var command1 = connection.CreateCommand();
                var command2 = connection.CreateCommand();

                var factory = DatabaseCommandStringFactory.Singleton;
                command1.CommandText = factory.BuildUpdateCompanyFirstDateCommandString(response1);
                command2.CommandText = factory.BuildUpdateCompanyFirstDateCommandString(response2);

                var databaseCommand1 = new DatabaseCommand(command1);
                var databaseCommand2 = new DatabaseCommand(command2);

                connection.Open();
                databaseCommand1.ExecuteNonQuery();
                databaseCommand2.ExecuteNonQuery();
                connection.Close();

                using (var command = new SQLiteCommand(SelectAllFromCompanyTableCommandString, connection))
                {
                    connection.Open();
                    var selectAllFromCompanyTableCommandReader = command.ExecuteReader();

                    try
                    {
                        Assert.IsTrue(selectAllFromCompanyTableCommandReader.Read());
                        Assert.AreEqual(5, selectAllFromCompanyTableCommandReader.FieldCount);
                        Assert.AreEqual(response1.Name, selectAllFromCompanyTableCommandReader["name"]);
                        Assert.AreEqual(response1.Symbol, selectAllFromCompanyTableCommandReader["symbol"]);
                        Assert.AreEqual(response1.Timestamp, selectAllFromCompanyTableCommandReader["firstDate"]);
                        Assert.AreEqual("null", selectAllFromCompanyTableCommandReader["recentDate"]);
                        Assert.AreEqual(1, selectAllFromCompanyTableCommandReader["collect"]);

                        Assert.IsTrue(selectAllFromCompanyTableCommandReader.Read());
                        Assert.AreEqual(5, selectAllFromCompanyTableCommandReader.FieldCount);
                        Assert.AreEqual(response2.Name, selectAllFromCompanyTableCommandReader["name"]);
                        Assert.AreEqual(response2.Symbol, selectAllFromCompanyTableCommandReader["symbol"]);
                        Assert.AreEqual(response2.Timestamp, selectAllFromCompanyTableCommandReader["firstDate"]);
                        Assert.AreEqual("null", selectAllFromCompanyTableCommandReader["recentDate"]);
                        Assert.AreEqual(1, selectAllFromCompanyTableCommandReader["collect"]);

                        Assert.IsFalse(selectAllFromCompanyTableCommandReader.Read());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        [TestMethod, TestCategory("Database Funcational Test")]
        public void TestUpdateCompanyRecentTimestamp()
        {
            SetupCleanTestingDatabase();

            using (var connection = new SQLiteConnection(DatabaseConnectionString))
            {
                SetupCleanCompanyLookupTable(connection);

                var response1 = new QuoteLookupResponse {Name = "Apple", Symbol = "AAPL", Timestamp = "Jun 4 00:00:00"};
                var response2 = new QuoteLookupResponse {Name = "Netflix", Symbol = "NFLX", Timestamp = "Jun 1 2:51:43"};
                InsertCompanyToLookupTable(connection, response1);
                InsertCompanyToLookupTable(connection, response2);

                var command1 = connection.CreateCommand();
                var command2 = connection.CreateCommand();

                var factory = DatabaseCommandStringFactory.Singleton;
                command1.CommandText = factory.BuildUpdateCompanyRecentTimestampCommandString(response1);
                command2.CommandText = factory.BuildUpdateCompanyRecentTimestampCommandString(response2);

                var databaseCommand1 = new DatabaseCommand(command1);
                var databaseCommand2 = new DatabaseCommand(command2);

                connection.Open();
                databaseCommand1.ExecuteNonQuery();
                databaseCommand2.ExecuteNonQuery();
                connection.Close();

                using (var command = new SQLiteCommand(SelectAllFromCompanyTableCommandString, connection))
                {
                    connection.Open();
                    var selectAllFromCompanyTableCommandReader = command.ExecuteReader();

                    try
                    {
                        Assert.IsTrue(selectAllFromCompanyTableCommandReader.Read());
                        Assert.AreEqual(5, selectAllFromCompanyTableCommandReader.FieldCount);
                        Assert.AreEqual(response1.Name, selectAllFromCompanyTableCommandReader["name"]);
                        Assert.AreEqual(response1.Symbol, selectAllFromCompanyTableCommandReader["symbol"]);
                        Assert.AreEqual("null", selectAllFromCompanyTableCommandReader["firstDate"]);
                        Assert.AreEqual(response1.Timestamp, selectAllFromCompanyTableCommandReader["recentDate"]);
                        Assert.AreEqual(1, selectAllFromCompanyTableCommandReader["collect"]);

                        Assert.IsTrue(selectAllFromCompanyTableCommandReader.Read());
                        Assert.AreEqual(5, selectAllFromCompanyTableCommandReader.FieldCount);
                        Assert.AreEqual(response2.Name, selectAllFromCompanyTableCommandReader["name"]);
                        Assert.AreEqual(response2.Symbol, selectAllFromCompanyTableCommandReader["symbol"]);
                        Assert.AreEqual("null", selectAllFromCompanyTableCommandReader["firstDate"]);
                        Assert.AreEqual(response2.Timestamp, selectAllFromCompanyTableCommandReader["recentDate"]);
                        Assert.AreEqual(1, selectAllFromCompanyTableCommandReader["collect"]);

                        Assert.IsFalse(selectAllFromCompanyTableCommandReader.Read());
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        [TestMethod, TestCategory("Database Funcational Test")]
        public void TestSelectAllCompaniesFromLookupTable()
        {
            SetupCleanTestingDatabase();

            using (var connection = new SQLiteConnection(DatabaseConnectionString))
            {
                SetupCleanCompanyLookupTable(connection);

                var response1 = new QuoteLookupResponse {Name = "Apple Inc", Symbol = "AAPL"};
                var response2 = new QuoteLookupResponse {Name = "Netflix", Symbol = "NFLX"};

                InsertCompanyToLookupTable(connection, response1);
                InsertCompanyToLookupTable(connection, response2);

                var factory = DatabaseCommandStringFactory.Singleton;

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = factory.BuildSelectAllCompaniesFromLookupTableCommandString();

                    connection.Open();
                    var databaseCommand = new DatabaseCommand(command);
                    var databaseReader = AssertIsOfTypeAndGet<DatabaseReader>(databaseCommand.ExecuteReader());

                    Assert.IsTrue(databaseReader.Read());
                    Assert.AreEqual(5, databaseReader.FieldCount);
                    Assert.AreEqual(response1.Name, databaseReader.Field<string>("name"));
                    Assert.AreEqual(response1.Symbol, databaseReader.Field<string>("symbol"));
                    Assert.AreEqual("null", databaseReader.Field<string>("firstDate"));
                    Assert.AreEqual("null", databaseReader.Field<string>("recentDate"));
                    Assert.AreEqual(1, databaseReader.Field<int>("collect"));

                    Assert.IsTrue(databaseReader.Read());
                    Assert.AreEqual(5, databaseReader.FieldCount);
                    Assert.AreEqual(response2.Name, databaseReader.Field<string>("name"));
                    Assert.AreEqual(response2.Symbol, databaseReader.Field<string>("symbol"));
                    Assert.AreEqual("null", databaseReader.Field<string>("firstDate"));
                    Assert.AreEqual("null", databaseReader.Field<string>("recentDate"));
                    Assert.AreEqual(1, databaseReader.Field<int>("collect"));
                    Assert.IsFalse(databaseReader.Read());

                    connection.Close();
                }
            }
        }

        [TestMethod, TestCategory("Database Funcational Test")]
        public void TestSelectAllQuotesFromHistoryTable()
        {
            SetupCleanTestingDatabase();
            var response1 = new QuoteLookupResponse
            {
                Name = "Apple Inc",
                Symbol = "AAPL",
                Timestamp = "Jun 4 00:00:00",
                LastPrice = 127.1f,
                Change = 0.52f,
                ChangePercent = 0.0062f
            };
            var response2 = new QuoteLookupResponse
            {
                Name = "Apple Inc",
                Symbol = "AAPL",
                Timestamp = "Jun 1 12:51:43",
                LastPrice = 293.4f,
                Change = 0.3f,
                ChangePercent = 0.0045f
            };

            using (var connection = new SQLiteConnection(DatabaseConnectionString))
            {
                SetupCleanQuoteHistoryTable(connection, response1.Symbol);
                InsertQuoteToHistoryTable(connection, response1);
                InsertQuoteToHistoryTable(connection, response2);


                using (var command = connection.CreateCommand())
                {
                    var lookupEntry = new CompanyLookupEntry {Symbol = "AAPL"};
                    var factory = DatabaseCommandStringFactory.Singleton;
                    command.CommandText = factory.BuildSelectAllQuotesFromHistoryTableCommandString(lookupEntry);

                    connection.Open();
                    var databaseCommand = new DatabaseCommand(command);
                    var databaseReader = AssertIsOfTypeAndGet<DatabaseReader>(databaseCommand.ExecuteReader());

                    Assert.IsTrue(databaseReader.Read());
                    Assert.AreEqual(6, databaseReader.FieldCount);
                    Assert.AreEqual(response1.Name, databaseReader.Field<string>("name"));
                    Assert.AreEqual(response1.Symbol, databaseReader.Field<string>("symbol"));
                    Assert.AreEqual(response1.Timestamp, databaseReader.Field<string>("timestamp"));
                    Assert.AreEqual(response1.LastPrice, databaseReader.Field<double>("lastPrice"), 0.001);
                    Assert.AreEqual(response1.Change, databaseReader.Field<double>("change"), 0.001);
                    Assert.AreEqual(response1.ChangePercent, databaseReader.Field<double>("changePercent"), 0.001);

                    Assert.IsTrue(databaseReader.Read());
                    Assert.AreEqual(6, databaseReader.FieldCount);
                    Assert.AreEqual(response2.Name, databaseReader.Field<string>("name"));
                    Assert.AreEqual(response2.Symbol, databaseReader.Field<string>("symbol"));
                    Assert.AreEqual(response2.Timestamp, databaseReader.Field<string>("timestamp"));
                    Assert.AreEqual(response2.LastPrice, databaseReader.Field<double>("lastPrice"), 0.001);
                    Assert.AreEqual(response2.Change, databaseReader.Field<double>("change"), 0.001);
                    Assert.AreEqual(response2.ChangePercent, databaseReader.Field<double>("changePercent"), 0.001);
                    Assert.IsFalse(databaseReader.Read());

                    connection.Close();
                }
            }
        }

        private static void SetupCleanTestingDatabase()
        {
            GC.Collect();
            GC.WaitForFullGCComplete();

            if (File.Exists(DatabaseFileName))File.Delete(DatabaseFileName);
            Assert.IsFalse(File.Exists(DatabaseFileName));
            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));
        }

        private static void SetupCleanCompanyLookupTable(SQLiteConnection connection)
        {
            const string commandString =
                "CREATE TABLE Company (name TEXT, symbol TEXT, firstDate TEXT, recentDate TEXT, collect INT)";
            using (var command = new SQLiteCommand(commandString, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        private static void SetupCleanQuoteHistoryTable(SQLiteConnection connection, string companySymbol)
        {
            var commandString = string.Format(
                "CREATE TABLE {0} (name TEXT, symbol TEXT, timestamp TEXT, lastPrice REAL, change REAL, changePercent REAL)",
                companySymbol);

            using (var command = new SQLiteCommand(commandString, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        private static void InsertCompanyToLookupTable(SQLiteConnection connection, QuoteLookupResponse response)
        {
            var commandString = string.Format(
                "INSERT INTO Company VALUES ('{0}', '{1}', 'null', 'null', 1)", response.Name, response.Symbol);


            using (var command = new SQLiteCommand(commandString, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        private static void InsertQuoteToHistoryTable(SQLiteConnection connection, QuoteLookupResponse response)
        {
            var commandString =
                string.Format(
                    "INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', {4}, {5}, {6})",
                    response.Symbol, response.Name, response.Symbol, response.Timestamp,
                    response.LastPrice, response.Change, response.ChangePercent);

            using (var command = new SQLiteCommand(commandString, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}