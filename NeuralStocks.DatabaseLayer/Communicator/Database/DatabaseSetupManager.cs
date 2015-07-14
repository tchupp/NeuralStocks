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
            DatabaseCommunicator.CreateDatabase(databaseFileName);
            DatabaseCommunicator.CreateCompanyTable();
        }
    }
}