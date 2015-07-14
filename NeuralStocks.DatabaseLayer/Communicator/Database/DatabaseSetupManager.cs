using System.Data.SQLite;

namespace NeuralStocks.DatabaseLayer.Communicator.Database
{
    public class DatabaseSetupManager : IDatabaseSetupManager
    {
        public IDatabaseCommunicator DatabaseCommunicator { get; private set; }

        public DatabaseSetupManager(IDatabaseCommunicator databaseCommunicator)
        {
            DatabaseCommunicator = databaseCommunicator;
        }

        public void InitializeDatabase(string databaseFileName)
        {
            var databaseConnectionString = "Data Source=" + databaseFileName + ";Version=3;";
            var connection = new SQLiteConnection(databaseConnectionString);

            DatabaseCommunicator.CreateDatabase(databaseFileName);
            DatabaseCommunicator.CreateCompanyTable(connection);
        }
    }
}