namespace NeuralStocks.DatabaseLayer.Database
{
    public interface IDatabaseSetupManager
    {
        void InitializeDatabase(string databaseFileName);
    }
}