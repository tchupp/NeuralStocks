using System.Data.SQLite;

namespace NeuralStocks.DatabaseLayer.Sqlite
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public DatabaseName DatabaseName { get; private set; }
        public SQLiteConnection WrappedConnection { get; private set; }

        public DatabaseConnection(DatabaseName databaseName)
        {
            DatabaseName = databaseName;
            WrappedConnection = new SQLiteConnection(DatabaseName.DatabaseConnectionString);
        }

        public void Open()
        {
            WrappedConnection.Open();
        }

        public void Close()
        {
            WrappedConnection.Close();
        }

        public IDatabaseCommand CreateCommand(string commandString)
        {
            var sqLiteCommand = WrappedConnection.CreateCommand();
            sqLiteCommand.CommandText = commandString;

            var databaseCommand = new DatabaseCommand(sqLiteCommand);
            return databaseCommand;
        }
    }
}