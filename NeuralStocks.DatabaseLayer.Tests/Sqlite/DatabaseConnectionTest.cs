using System.Data;
using System.Data.SQLite;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.DatabaseLayer.Tests.Sqlite
{
    [TestClass]
    public class DatabaseConnectionTest : AssertTestClass
    {
        private const string DatabaseFileName = "TestSqliteDatabase.sqlite";

        [TestMethod, TestCategory("Database")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IDatabaseConnection), typeof (DatabaseConnection));
        }

        [TestMethod, TestCategory("Database")]
        public void TestDatabaseConnectionNamePassedIn()
        {
            var connectionName = new DatabaseName();

            var connection = new DatabaseConnection(connectionName);

            Assert.AreSame(connectionName, connection.DatabaseName);
        }

        [TestMethod, TestCategory("Database")]
        public void TestOpenCallsOpenOnWrappedConnection_CloseCallsClose()
        {
            if (File.Exists(DatabaseFileName)) File.Delete(DatabaseFileName);
            Assert.IsFalse(File.Exists(DatabaseFileName));
            SQLiteConnection.CreateFile(DatabaseFileName);
            Assert.IsTrue(File.Exists(DatabaseFileName));

            var connectionName = new DatabaseName {Name = DatabaseFileName};

            var connection = new DatabaseConnection(connectionName);
            var wrappedConnection = connection.WrappedConnection;

            Assert.AreEqual(ConnectionState.Closed, wrappedConnection.State);
            connection.Open();
            Assert.AreEqual(ConnectionState.Open, wrappedConnection.State);
            connection.Close();
            Assert.AreEqual(ConnectionState.Closed, wrappedConnection.State);
        }

        [TestMethod, TestCategory("Database")]
        public void TestCreateCommand()
        {
            const string commandString = "command to do stuff";

            var connectionName = new DatabaseName {Name = DatabaseFileName};
            var connection = new DatabaseConnection(connectionName);

            var command = AssertIsOfTypeAndGet<DatabaseCommand>(connection.CreateCommand(commandString));

            var sqLiteCommand = AssertIsOfTypeAndGet<SQLiteCommand>(command.WrappedCommand);
            Assert.AreEqual(commandString, sqLiteCommand.CommandText);
        }
    }
}