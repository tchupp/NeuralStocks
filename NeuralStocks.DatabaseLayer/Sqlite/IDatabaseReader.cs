namespace NeuralStocks.DatabaseLayer.Sqlite
{
    public interface IDatabaseReader
    {
        int FieldCount { get; }
        bool Read();
        T Field<T>(string name);
    }
}