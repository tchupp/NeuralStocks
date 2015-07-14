namespace NeuralStocks.DatabaseLayer.Communicator.Database
{
    public interface IDatabaseSetupManager
    {
        void InitializeDatabase(string databaseFileName);
    }
}