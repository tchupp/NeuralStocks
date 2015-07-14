namespace NeuralStocks.DatabaseLayer.Sqlite
{
    public interface IDatabaseCommand
    {
        void ExecuteNonQuery();
        IDatabaseReader ExecuteReader();
    }
}