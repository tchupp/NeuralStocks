namespace NeuralStocks.Backend.Database
{
    public interface IDatabaseSetupManager
    {
        void InitializeDatabase(string databaseFileName);
    }
}