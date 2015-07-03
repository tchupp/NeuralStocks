using System;
using System.Data.SQLite;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.Model.Database;
using NeuralStocks.DatabaseLayer.Model.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.DatabaseLayer.Tests.Database
{
    [TestClass]
    public class DatabaseCommunicatorTest : AssertTestClass
    {
        private const string DatabaseFileName = "TestStocksDatabase.sqlite";
        private const string DatabaseConnectionString = "Data Source=" + DatabaseFileName + ";Version=3;";
        private const string SelectAllFromCompanyTableCommandString = "SELECT * FROM Company";

        private const string CreateCompanyTableCommandString =
            "CREATE TABLE Company (name TEXT, symbol TEXT, firstDate TEXT, recentDate TEXT, collect INT)";

        [TestCleanup]
        public void TearDown()
        {
            GC.Collect();
            GC.WaitForFullGCComplete();
        }

        [TestMethod, TestCategory("Database")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(
                typeof (IDatabaseCommunicator), typeof (DatabaseCommunicator));
        }

        [TestMethod, TestCategory("Database")]
        public void TestSqlDatabaseCommandRunnerIsSingleton()
        {
            AssertPrivateContructor(typeof (DatabaseCommunicator));
            Assert.AreSame(DatabaseCommunicator.Singleton, DatabaseCommunicator.Singleton);
        }

        [TestMethod, TestCategory("Database")]
        public void TestCreateDatabase()
        {
            if (File.Exists(DatabaseFileName)) File.Delete(DatabaseFileName);
            Assert.IsFalse(File.Exists(DatabaseFileName));

            var commandRunner = DatabaseCommunicator.Singleton;
            commandRunner.CreateDatabase(DatabaseFileName);

            Assert.IsTrue(File.Exists(DatabaseFileName));

            if (File.Exists(DatabaseFileName)) File.Delete(DatabaseFileName);
            Assert.IsFalse(File.Exists(DatabaseFileName));
        }

        [TestMethod, TestCategory("Database")]
        public void TestCreateCompanyTable_CreatesTable()
        {
            if (File.Exists(DatabaseFileName)) File.Delete(DatabaseFileName);
            Assert.IsFalse(File.Exists(DatabaseFileName));
            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

            const string selectAllTablesByNameCompanyCommandString =
                "SELECT name FROM sqlite_master WHERE type='table' AND name='Company'";

            var connection = new SQLiteConnection(DatabaseConnectionString);

            var commandRunner = DatabaseCommunicator.Singleton;
            commandRunner.CreateCompanyTable(connection);

            connection.Open();

            var selectAllTablesByNameCompanyCommand = new SQLiteCommand(
                selectAllTablesByNameCompanyCommandString, connection);
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
                selectAllTablesByNameCompanyCommand.Dispose();
                connection.Close();
            }
        }

        [TestMethod, TestCategory("Database")]
        public void TestCreateCompanyTable_CreatesCorrectFields()
        {
            if (File.Exists(DatabaseFileName)) File.Delete(DatabaseFileName);
            Assert.IsFalse(File.Exists(DatabaseFileName));
            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

            var connection = new SQLiteConnection(DatabaseConnectionString);

            var commandRunner = DatabaseCommunicator.Singleton;
            commandRunner.CreateCompanyTable(connection);

            connection.Open();

            var selectAllFromCompanyTableCommand = new SQLiteCommand(
                SelectAllFromCompanyTableCommandString, connection);
            var selectAllFromCompanyTableCommandReader = selectAllFromCompanyTableCommand.ExecuteReader();

            try
            {
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
            }
            finally
            {
                selectAllFromCompanyTableCommand.Dispose();
                connection.Close();
            }
        }

        [TestMethod, TestCategory("Database")]
        public void TestAddCompanyToTable_CorrectlyAddsACompanyWithNameAndSymbol_AndNullDates()
        {
            if (File.Exists(DatabaseFileName)) File.Delete(DatabaseFileName);
            Assert.IsFalse(File.Exists(DatabaseFileName));
            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

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

            var connection = new SQLiteConnection(DatabaseConnectionString);

            var commandRunner = DatabaseCommunicator.Singleton;
            commandRunner.CreateCompanyTable(connection);

            commandRunner.AddCompanyToTable(connection, companyLookupResponse1);
            commandRunner.AddCompanyToTable(connection, companyLookupResponse2);

            connection.Open();

            var checkCompanyTableFieldsCommand = new SQLiteCommand(SelectAllFromCompanyTableCommandString, connection);
            var checkCompanyTableFieldsCommandReader = checkCompanyTableFieldsCommand.ExecuteReader();

            try
            {
                Assert.IsTrue(checkCompanyTableFieldsCommandReader.Read());
                Assert.AreEqual(5, checkCompanyTableFieldsCommandReader.FieldCount);
                Assert.AreEqual(expectedName1, checkCompanyTableFieldsCommandReader["name"]);
                Assert.AreEqual(expectedSymbol1, checkCompanyTableFieldsCommandReader["symbol"]);
                Assert.AreEqual("null", checkCompanyTableFieldsCommandReader["firstDate"]);
                Assert.AreEqual("null", checkCompanyTableFieldsCommandReader["recentDate"]);
                Assert.AreEqual(1L, checkCompanyTableFieldsCommandReader["collect"]);

                Assert.IsTrue(checkCompanyTableFieldsCommandReader.Read());
                Assert.AreEqual(5, checkCompanyTableFieldsCommandReader.FieldCount);
                Assert.AreEqual(expectedName2, checkCompanyTableFieldsCommandReader["name"]);
                Assert.AreEqual(expectedSymbol2, checkCompanyTableFieldsCommandReader["symbol"]);
                Assert.AreEqual("null", checkCompanyTableFieldsCommandReader["firstDate"]);
                Assert.AreEqual("null", checkCompanyTableFieldsCommandReader["recentDate"]);
                Assert.AreEqual(1L, checkCompanyTableFieldsCommandReader["collect"]);

                Assert.IsFalse(checkCompanyTableFieldsCommandReader.Read());
            }
            finally
            {
                checkCompanyTableFieldsCommand.Dispose();
                connection.Close();
            }
        }

        [TestMethod, TestCategory("Database")]
        public void TestAddCompanyToTable_AlsoAddsNewQuoteHistoryTable()
        {
            if (File.Exists(DatabaseFileName)) File.Delete(DatabaseFileName);
            Assert.IsFalse(File.Exists(DatabaseFileName));
            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

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

            var connection = new SQLiteConnection(DatabaseConnectionString);

            connection.Open();

            var createCompanyTableCommand = new SQLiteCommand(CreateCompanyTableCommandString, connection);
            createCompanyTableCommand.ExecuteNonQuery();
            createCompanyTableCommand.Dispose();

            connection.Close();

            var commandRunner = DatabaseCommunicator.Singleton;

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
                selectAllTablesCommand.Dispose();
                connection.Close();
            }
        }

        [TestMethod, TestCategory("Database")]
        public void TestAddCompanyToTable_WritesToConsole()
        {
            if (File.Exists(DatabaseFileName)) File.Delete(DatabaseFileName);
            Assert.IsFalse(File.Exists(DatabaseFileName));
            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

            const string name = "Apple Inc";
            const string symbol = "AAPL";
            var companyLookupResponse = new CompanyLookupResponse
            {
                Name = name,
                Symbol = symbol
            };

            var connection = new SQLiteConnection(DatabaseConnectionString);

            connection.Open();
            var createCompanyTableCommand = new SQLiteCommand(CreateCompanyTableCommandString, connection);
            createCompanyTableCommand.ExecuteNonQuery();
            createCompanyTableCommand.Dispose();
            connection.Close();

            var mockWriter = new Mock<TextWriter>();
            Console.SetOut(mockWriter.Object);

            var commandRunner = DatabaseCommunicator.Singleton;

            mockWriter.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);

            commandRunner.AddCompanyToTable(connection, companyLookupResponse);

            mockWriter.Verify(m => m.WriteLine("Added {0} to company lookup table, " +
                                               "and added a quote history table : {1}.", name, symbol), Times.Once);
        }

        [TestMethod, TestCategory("Database")]
        public void TestUpdateCompanyTimestamp_SetsRecentDateAsTimestampOfQuote_AndFirstDate_FirstDateNull()
        {
            if (File.Exists(DatabaseFileName)) File.Delete(DatabaseFileName);
            Assert.IsFalse(File.Exists(DatabaseFileName));
            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

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

            var addCompanyToTableCommandString1 =
                "INSERT INTO Company VALUES ('" +
                quoteLookupResponse1.Name + "', '" +
                quoteLookupResponse1.Symbol + "', 'null', 'null', 1)";
            var addCompanyToTableCommandString2 =
                "INSERT INTO Company VALUES ('" +
                quoteLookupResponse2.Name + "', '" +
                quoteLookupResponse2.Symbol + "', 'null', 'null', 1)";

            var connection = new SQLiteConnection(DatabaseConnectionString);

            connection.Open();

            var createCompanyTableCommand = new SQLiteCommand(CreateCompanyTableCommandString, connection);
            createCompanyTableCommand.ExecuteNonQuery();
            createCompanyTableCommand.Dispose();

            var addCompanyToTableCommand1 = new SQLiteCommand(addCompanyToTableCommandString1, connection);
            addCompanyToTableCommand1.ExecuteNonQuery();
            addCompanyToTableCommand1.Dispose();

            var addCompanyToTableCommand2 = new SQLiteCommand(addCompanyToTableCommandString2, connection);
            addCompanyToTableCommand2.ExecuteNonQuery();
            addCompanyToTableCommand2.Dispose();

            connection.Close();

            var commandRunner = DatabaseCommunicator.Singleton;
            commandRunner.UpdateCompanyTimestamp(connection, quoteLookupResponse1);
            commandRunner.UpdateCompanyTimestamp(connection, quoteLookupResponse2);

            connection.Open();

            var selectAllFromCompanyTableCommand = new SQLiteCommand(SelectAllFromCompanyTableCommandString, connection);
            var selectAllFromCompanyTableCommandReader = selectAllFromCompanyTableCommand.ExecuteReader();

            try
            {
                Assert.IsTrue(selectAllFromCompanyTableCommandReader.Read());
                Assert.AreEqual(5, selectAllFromCompanyTableCommandReader.FieldCount);
                Assert.AreEqual(expectedName1, selectAllFromCompanyTableCommandReader["name"]);
                Assert.AreEqual(expectedSymbol1, selectAllFromCompanyTableCommandReader["symbol"]);
                Assert.AreEqual(expectedTimestamp1, selectAllFromCompanyTableCommandReader["firstDate"]);
                Assert.AreEqual(expectedTimestamp1, selectAllFromCompanyTableCommandReader["recentDate"]);
                Assert.AreEqual(1, selectAllFromCompanyTableCommandReader["collect"]);

                Assert.IsTrue(selectAllFromCompanyTableCommandReader.Read());
                Assert.AreEqual(5, selectAllFromCompanyTableCommandReader.FieldCount);
                Assert.AreEqual(expectedName2, selectAllFromCompanyTableCommandReader["name"]);
                Assert.AreEqual(expectedSymbol2, selectAllFromCompanyTableCommandReader["symbol"]);
                Assert.AreEqual(expectedTimestamp2, selectAllFromCompanyTableCommandReader["firstDate"]);
                Assert.AreEqual(expectedTimestamp2, selectAllFromCompanyTableCommandReader["recentDate"]);
                Assert.AreEqual(1, selectAllFromCompanyTableCommandReader["collect"]);

                Assert.IsFalse(selectAllFromCompanyTableCommandReader.Read());
            }
            finally
            {
                selectAllFromCompanyTableCommand.Dispose();
                connection.Close();
            }
        }

        [TestMethod, TestCategory("Database")]
        public void TestUpdateCompanyTimestamp_SetsRecentDateAsTimestampOfQuote_FirstDateExists()
        {
            if (File.Exists(DatabaseFileName)) File.Delete(DatabaseFileName);
            Assert.IsFalse(File.Exists(DatabaseFileName));
            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

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

            var addCompanyToTableCommandString1 =
                "INSERT INTO Company VALUES ('" +
                quoteLookupResponse1.Name + "', '" +
                quoteLookupResponse1.Symbol + "', '" +
                initialTimestamp1 + "', '" +
                initialTimestamp1 + "', 1)";
            var addCompanyToTableCommandString2 =
                "INSERT INTO Company VALUES ('" +
                quoteLookupResponse2.Name + "', '" +
                quoteLookupResponse2.Symbol + "', '" +
                initialTimestamp2 + "', '" +
                initialTimestamp2 + "', 0)";

            var connection = new SQLiteConnection(DatabaseConnectionString);

            connection.Open();

            var createCompanyTableCommand = new SQLiteCommand(CreateCompanyTableCommandString, connection);
            createCompanyTableCommand.ExecuteNonQuery();
            createCompanyTableCommand.Dispose();

            var addCompanyToTableCommand1 = new SQLiteCommand(addCompanyToTableCommandString1, connection);
            addCompanyToTableCommand1.ExecuteNonQuery();
            addCompanyToTableCommand1.Dispose();

            var addCompanyToTableCommand2 = new SQLiteCommand(addCompanyToTableCommandString2, connection);
            addCompanyToTableCommand2.ExecuteNonQuery();
            addCompanyToTableCommand2.Dispose();

            connection.Close();

            var commandRunner = DatabaseCommunicator.Singleton;
            commandRunner.UpdateCompanyTimestamp(connection, quoteLookupResponse1);
            commandRunner.UpdateCompanyTimestamp(connection, quoteLookupResponse2);

            connection.Open();

            var selectAllFromCompanyTableCommand = new SQLiteCommand(SelectAllFromCompanyTableCommandString, connection);
            var selectAllFromCompanyTableCommandReader = selectAllFromCompanyTableCommand.ExecuteReader();

            try
            {
                Assert.IsTrue(selectAllFromCompanyTableCommandReader.Read());
                Assert.AreEqual(5, selectAllFromCompanyTableCommandReader.FieldCount);
                Assert.AreEqual(expectedName1, selectAllFromCompanyTableCommandReader["name"]);
                Assert.AreEqual(expectedSymbol1, selectAllFromCompanyTableCommandReader["symbol"]);
                Assert.AreEqual(initialTimestamp1, selectAllFromCompanyTableCommandReader["firstDate"]);
                Assert.AreEqual(expectedTimestamp1, selectAllFromCompanyTableCommandReader["recentDate"]);
                Assert.AreEqual(1, selectAllFromCompanyTableCommandReader["collect"]);

                Assert.IsTrue(selectAllFromCompanyTableCommandReader.Read());
                Assert.AreEqual(5, selectAllFromCompanyTableCommandReader.FieldCount);
                Assert.AreEqual(expectedName2, selectAllFromCompanyTableCommandReader["name"]);
                Assert.AreEqual(expectedSymbol2, selectAllFromCompanyTableCommandReader["symbol"]);
                Assert.AreEqual(initialTimestamp2, selectAllFromCompanyTableCommandReader["firstDate"]);
                Assert.AreEqual(expectedTimestamp2, selectAllFromCompanyTableCommandReader["recentDate"]);
                Assert.AreEqual(0, selectAllFromCompanyTableCommandReader["collect"]);

                Assert.IsFalse(selectAllFromCompanyTableCommandReader.Read());
            }
            finally
            {
                selectAllFromCompanyTableCommand.Dispose();
                connection.Close();
            }
        }

        [TestMethod, TestCategory("Database")]
        public void TestUpdateCompanyTimestamp_WritesToConsole()
        {
            if (File.Exists(DatabaseFileName)) File.Delete(DatabaseFileName);
            Assert.IsFalse(File.Exists(DatabaseFileName));
            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

            const string expectedName = "Apple Inc";
            const string expectedSymbol = "AAPL";
            const string initialTimestamp = "Tues May 26 05:17:42 UTC-04:00 2015";
            const string expectedTimestamp = "Thu Jun 4 00:00:00 UTC-04:00 2015";

            var quoteLookupResponse = new QuoteLookupResponse
            {
                Name = expectedName,
                Symbol = expectedSymbol,
                Timestamp = expectedTimestamp
            };

            var addCompanyToTableCommandString =
                "INSERT INTO Company VALUES ('" +
                quoteLookupResponse.Name + "', '" +
                quoteLookupResponse.Symbol + "', '" +
                initialTimestamp + "', '" +
                initialTimestamp + "', 1)";

            var connection = new SQLiteConnection(DatabaseConnectionString);

            connection.Open();

            var createCompanyTableCommand = new SQLiteCommand(CreateCompanyTableCommandString, connection);
            createCompanyTableCommand.ExecuteNonQuery();
            createCompanyTableCommand.Dispose();

            var addCompanyToTableCommand = new SQLiteCommand(addCompanyToTableCommandString, connection);
            addCompanyToTableCommand.ExecuteNonQuery();
            addCompanyToTableCommand.Dispose();

            connection.Close();

            var mockWriter = new Mock<TextWriter>();
            Console.SetOut(mockWriter.Object);

            var commandRunner = DatabaseCommunicator.Singleton;

            mockWriter.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);

            commandRunner.UpdateCompanyTimestamp(connection, quoteLookupResponse);

            mockWriter.Verify(m => m.WriteLine("Updating Timestamp: Company: {0}. Time: {1}",
                expectedSymbol, expectedTimestamp), Times.Once);
        }

        [TestMethod, TestCategory("Database")]
        public void TestGetCompanyLookupList_ReturnsCorrectCompanyLookupRequestList()
        {
            if (File.Exists(DatabaseFileName)) File.Delete(DatabaseFileName);
            Assert.IsFalse(File.Exists(DatabaseFileName));
            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

            const string expectedSymbol1 = "AAPL";
            const string expectedSymbol2 = "NFLX";
            const string timestamp1 = "Tues Jun 16";
            const string timestamp2 = "Tues Jun 16";

            const string addCompanyToTableCommandString1 = "INSERT INTO Company VALUES ('null', '" +
                                                           expectedSymbol1 + "', 'null', '" + timestamp1 + "', 1)";
            const string addCompanyToTableCommandString2 = "INSERT INTO Company VALUES ('null', '" +
                                                           expectedSymbol2 + "', 'null', '" + timestamp2 + "', 0)";

            var connection = new SQLiteConnection(DatabaseConnectionString);

            connection.Open();

            var createCompanyTableCommand = new SQLiteCommand(CreateCompanyTableCommandString, connection);
            createCompanyTableCommand.ExecuteNonQuery();
            createCompanyTableCommand.Dispose();

            var addCompanyToTableCommand1 = new SQLiteCommand(addCompanyToTableCommandString1, connection);
            addCompanyToTableCommand1.ExecuteNonQuery();
            addCompanyToTableCommand1.Dispose();

            var addCompanyToTableCommand2 = new SQLiteCommand(addCompanyToTableCommandString2, connection);
            addCompanyToTableCommand2.ExecuteNonQuery();
            addCompanyToTableCommand2.Dispose();

            connection.Close();

            var commandRunner = DatabaseCommunicator.Singleton;
            var quoteLookupsFromTable = commandRunner.GetQuoteLookupList(connection);

            Assert.AreEqual(2, quoteLookupsFromTable.Count);

            Assert.IsInstanceOfType(quoteLookupsFromTable[0], typeof (QuoteLookupRequest));
            Assert.IsInstanceOfType(quoteLookupsFromTable[1], typeof (QuoteLookupRequest));

            Assert.AreEqual(expectedSymbol1, quoteLookupsFromTable[0].Company);
            Assert.AreEqual(timestamp1, quoteLookupsFromTable[0].Timestamp);

            Assert.AreEqual(expectedSymbol2, quoteLookupsFromTable[1].Company);
            Assert.AreEqual(timestamp2, quoteLookupsFromTable[1].Timestamp);
        }

        [TestMethod, TestCategory("Database")]
        public void TestAddQuoteResponseToTable()
        {
            if (File.Exists(DatabaseFileName)) File.Delete(DatabaseFileName);
            Assert.IsFalse(File.Exists(DatabaseFileName));
            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

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

            var connection = new SQLiteConnection(DatabaseConnectionString);

            connection.Open();

            var createCompanyTableCommand1 = new SQLiteCommand(createCompanyTableCommandString1, connection);
            createCompanyTableCommand1.ExecuteNonQuery();

            var createCompanyTableCommand2 = new SQLiteCommand(createCompanyTableCommandString2, connection);
            createCompanyTableCommand2.ExecuteNonQuery();

            connection.Close();

            var commandRunner = DatabaseCommunicator.Singleton;
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
                selectAllTablesCommand1.Dispose();
                selectAllTablesCommand2.Dispose();

                connection.Close();
            }
        }

        [TestMethod, TestCategory("Database")]
        public void TestAddQuoteResponseToTable_WriteToConsole()
        {
            if (File.Exists(DatabaseFileName)) File.Delete(DatabaseFileName);
            Assert.IsFalse(File.Exists(DatabaseFileName));
            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

            const string expectedName = "Apple Inc";
            const string expectedSymbol = "AAPL";
            const string expectedTimestamp = "Thu Jun 4 00:00:00 UTC-04:00 2015";
            const double expectedLastPrice = 127.1f;
            const double expectedChange = 0.52f;
            const double expectedChangePercent = 0.0062f;

            var createCompanyTableCommandString =
                string.Format("CREATE TABLE {0} " +
                              "(name TEXT, symbol TEXT, timestamp TEXT, " +
                              "lastPrice REAL, change REAL, changePercent REAL)", expectedSymbol);

            var quoteLookupResponse = new QuoteLookupResponse
            {
                Name = expectedName,
                Symbol = expectedSymbol,
                Timestamp = expectedTimestamp,
                LastPrice = expectedLastPrice,
                Change = expectedChange,
                ChangePercent = expectedChangePercent
            };

            var connection = new SQLiteConnection(DatabaseConnectionString);

            connection.Open();

            var createCompanyTableCommand = new SQLiteCommand(createCompanyTableCommandString, connection);
            createCompanyTableCommand.ExecuteNonQuery();
            createCompanyTableCommand.Dispose();

            connection.Close();

            var mockWriter = new Mock<TextWriter>();
            Console.SetOut(mockWriter.Object);

            var commandRunner = DatabaseCommunicator.Singleton;

            mockWriter.Verify(m => m.WriteLine(It.IsAny<string>()), Times.Never);

            commandRunner.AddQuoteResponseToTable(connection, quoteLookupResponse);

            mockWriter.Verify(m => m.WriteLine("Adding Quote: Company: {0}. Time: {1}. Amount: {2}.",
                expectedSymbol, expectedTimestamp, expectedLastPrice), Times.Once);
        }

        [TestMethod, TestCategory("Database")]
        public void TestGetCompanyLookupEntryList()
        {
            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

            const string expectedSymbol1 = "AAPL";
            const string expectedName1 = "Apple";
            const string timestamp1 = "Tues Jun 16";

            const string expectedSymbol2 = "NFLX";
            const string expectedName2 = "Netflix";
            const string timestamp2 = "Wed Jun 17";

            var addCompanyToTableCommandString1 =
                string.Format("INSERT INTO Company VALUES ('{0}', '{1}', '{2}', 'null', {3})",
                    expectedName1, expectedSymbol1, timestamp1, 1);
            var addCompanyToTableCommandString2 =
                string.Format("INSERT INTO Company VALUES ('{0}', '{1}', '{2}', 'null', {3})",
                    expectedName2, expectedSymbol2, timestamp2, 0);

            var connection = new SQLiteConnection(DatabaseConnectionString);

            connection.Open();

            var createCompanyTableCommand = new SQLiteCommand(CreateCompanyTableCommandString, connection);
            createCompanyTableCommand.ExecuteNonQuery();
            createCompanyTableCommand.Dispose();

            var addCompanyToTableCommand1 = new SQLiteCommand(addCompanyToTableCommandString1, connection);
            addCompanyToTableCommand1.ExecuteNonQuery();
            addCompanyToTableCommand1.Dispose();

            var addCompanyToTableCommand2 = new SQLiteCommand(addCompanyToTableCommandString2, connection);
            addCompanyToTableCommand2.ExecuteNonQuery();
            addCompanyToTableCommand2.Dispose();

            connection.Close();

            var commandRunner = DatabaseCommunicator.Singleton;
            var companyLookupEntryList = commandRunner.GetCompanyLookupEntryList(connection);

            Assert.AreEqual(2, companyLookupEntryList.Count);

            Assert.AreEqual(expectedName1, companyLookupEntryList[0].Name);
            Assert.AreEqual(expectedSymbol1, companyLookupEntryList[0].Symbol);
            Assert.AreEqual(timestamp1, companyLookupEntryList[0].FirstDate);
            Assert.AreEqual("null", companyLookupEntryList[0].RecentDate);
            Assert.AreEqual(true, companyLookupEntryList[0].Collection);

            Assert.AreEqual(expectedName2, companyLookupEntryList[1].Name);
            Assert.AreEqual(expectedSymbol2, companyLookupEntryList[1].Symbol);
            Assert.AreEqual(timestamp2, companyLookupEntryList[1].FirstDate);
            Assert.AreEqual("null", companyLookupEntryList[1].RecentDate);
            Assert.AreEqual(false, companyLookupEntryList[1].Collection);
        }

        [TestMethod, TestCategory("Database")]
        public void TestGetQuoteHistoryEntryListForCompany()
        {
            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

            const string companySymbol = "AAPL";
            const string companyName = "Apple";

            const string companyTimestamp1 = "Tues Jun 15";
            const double companyLastPrice1 = 123.5;
            const double companyChange1 = 2.54;
            const double companyChangePercent1 = 0.564;

            const string companyTimestamp2 = "Wed Jun 16";
            const double companyLastPrice2 = 453.1;
            const double companyChange2 = 3.68;
            const double companyChangePercent2 = 1.45;

            var createCompanyTableCommandString =
                string.Format("CREATE TABLE {0} (name TEXT, symbol TEXT, timestamp TEXT, " +
                              "lastPrice REAL, change REAL, changePercent REAL)", companySymbol);

            var addQuoteToTableCommandString1 = string.Format(
                "INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', {4}, {5}, {6})",
                companySymbol, companyName, companySymbol, companyTimestamp1,
                companyLastPrice1, companyChange1, companyChangePercent1);
            var addQuoteToTableCommandString2 = string.Format(
                "INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', {4}, {5}, {6})",
                companySymbol, companyName, companySymbol, companyTimestamp2,
                companyLastPrice2, companyChange2, companyChangePercent2);

            var connection = new SQLiteConnection(DatabaseConnectionString);

            connection.Open();

            var createCompanyTableCommand = new SQLiteCommand(createCompanyTableCommandString, connection);
            createCompanyTableCommand.ExecuteNonQuery();
            createCompanyTableCommand.Dispose();

            var addQuoteToTableCommand1 = new SQLiteCommand(addQuoteToTableCommandString1, connection);
            addQuoteToTableCommand1.ExecuteNonQuery();
            addQuoteToTableCommand1.Dispose();

            var addQuoteToTableCommand2 = new SQLiteCommand(addQuoteToTableCommandString2, connection);
            addQuoteToTableCommand2.ExecuteNonQuery();
            addQuoteToTableCommand2.Dispose();

            connection.Close();

            var lookupEntry = new CompanyLookupEntry
            {
                Symbol = companySymbol
            };

            var commandRunner = DatabaseCommunicator.Singleton;
            var quoteHistoryEntryList = commandRunner.GetQuoteHistoryEntryList(connection, lookupEntry);

            Assert.AreEqual(2, quoteHistoryEntryList.Count);

            Assert.AreEqual(companyName, quoteHistoryEntryList[0].Name);
            Assert.AreEqual(companySymbol, quoteHistoryEntryList[0].Symbol);
            Assert.AreEqual(companyTimestamp1, quoteHistoryEntryList[0].Timestamp);
            Assert.AreEqual(companyLastPrice1, quoteHistoryEntryList[0].LastPrice);
            Assert.AreEqual(companyChange1, quoteHistoryEntryList[0].Change);
            Assert.AreEqual(companyChangePercent1, quoteHistoryEntryList[0].ChangePercent);

            Assert.AreEqual(companyName, quoteHistoryEntryList[1].Name);
            Assert.AreEqual(companySymbol, quoteHistoryEntryList[1].Symbol);
            Assert.AreEqual(companyTimestamp2, quoteHistoryEntryList[1].Timestamp);
            Assert.AreEqual(companyLastPrice2, quoteHistoryEntryList[1].LastPrice);
            Assert.AreEqual(companyChange2, quoteHistoryEntryList[1].Change);
            Assert.AreEqual(companyChangePercent2, quoteHistoryEntryList[1].ChangePercent);
        }
    }
}