using System.Data;
using System.Data.SQLite;

namespace NeuralStocks.DatabaseLayer.Sqlite
{
    public class DatabaseCommand : IDatabaseCommand
    {
        public SQLiteCommand WrappedCommand { get; private set; }

        public DatabaseCommand(SQLiteCommand wrappedCommand)
        {
            WrappedCommand = wrappedCommand;
        }

        public void ExecuteNonQuery()
        {
            using (WrappedCommand)
            {
                WrappedCommand.ExecuteNonQuery();
            }
        }

        public IDatabaseReader ExecuteReader()
        {
            DatabaseReader databaseReader;
            using (WrappedCommand)
            {
                databaseReader = new DatabaseReader(WrappedCommand.ExecuteReader(CommandBehavior.CloseConnection));
            }
            return databaseReader;
        }
    }
}