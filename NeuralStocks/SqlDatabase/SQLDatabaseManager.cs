using System.Data.SQLite;
using System.IO;

namespace NeuralStocks.SqlDatabase
{
    public class SqlDatabaseManager : ISqlDatabaseManager
    {
        private const string DatabaseFileName = "NeuralStocksDatabase.sqlite";
        private const string DatabaseConnectionString = "Data Source=" + DatabaseFileName + ";Version=3;";

        public void InitializeDatabase()
        {
            if (File.Exists(DatabaseFileName)) return;

            SQLiteConnection.CreateFile(DatabaseFileName);

            var connection = new SQLiteConnection(DatabaseConnectionString);
            connection.Open();

            const string createInitialTableCommandString =
                "CREATE TABLE Company (name TEXT, firstDate TEXT, recentDate TEXT)";

            var createInitialTableCommand = new SQLiteCommand(createInitialTableCommandString, connection);
            createInitialTableCommand.ExecuteNonQuery();

            connection.Close();
        }
    }
}