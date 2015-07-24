using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NUnit.Framework;

namespace NeuralStocks.DatabaseLayer.Tests.Sqlite
{
    [TestFixture]
    public class DatabaseConnectionTest : AssertTestClass
    {
        [TearDown]
        [Category("Database")]
        public void TearDown()
        {
            GC.Collect();
            if (File.Exists(DatabaseFileName)) File.Delete(DatabaseFileName);
            Assert.IsFalse(File.Exists(DatabaseFileName));
            GC.WaitForFullGCComplete();
        }

        private const string DatabaseFileName = "TestSqliteDatabase.sqlite";

        [Test]
        [Category("Database")]
        public void TestCreateCommand()
        {
            const string commandString = "command to do stuff";

            var connectionName = new DatabaseName {Name = DatabaseFileName};
            var connection = new DatabaseConnection(connectionName);

            var command = AssertIsOfTypeAndGet<DatabaseCommand>(connection.CreateCommand(commandString));

            var sqLiteCommand = AssertIsOfTypeAndGet<SQLiteCommand>(command.WrappedCommand);
            Assert.AreEqual(commandString, sqLiteCommand.CommandText);
        }

        [Test]
        [Category("Database")]
        public void TestDatabaseConnectionNamePassedIn()
        {
            var connectionName = new DatabaseName();

            var connection = new DatabaseConnection(connectionName);

            Assert.AreSame(connectionName, connection.DatabaseName);
        }

        [Test]
        [Category("Database")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IDatabaseConnection), typeof (DatabaseConnection));
        }

        [Test]
        [Category("Database")]
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
    }
}