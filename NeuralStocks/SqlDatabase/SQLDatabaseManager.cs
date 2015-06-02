using System.Data.SQLite;

namespace NeuralStocks.SqlDatabase
{
    public class SqlDatabaseManager : ISqlDatabaseManager
    {
        public void InitializeDatabase()
        {
            SQLiteConnection.CreateFile("TestStocksDatabase.sqlite");
        }
    }
}