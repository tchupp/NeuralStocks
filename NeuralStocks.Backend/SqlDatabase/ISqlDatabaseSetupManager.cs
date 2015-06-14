namespace NeuralStocks.Backend.SqlDatabase
{
    public interface ISqlDatabaseSetupManager
    {
        void InitializeDatabase(string databaseFileName);
    }
}