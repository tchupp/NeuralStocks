namespace NeuralStocks.SqlDatabase
{
    public interface ISqlDatabaseSetupManager
    {
        void InitializeDatabase(string databaseFileName);
    }
}