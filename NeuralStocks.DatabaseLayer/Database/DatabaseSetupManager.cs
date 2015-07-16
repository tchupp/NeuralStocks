namespace NeuralStocks.DatabaseLayer.Database
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
            DatabaseCommunicator.CreateCompanyTable();
        }
    }
}