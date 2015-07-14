namespace NeuralStocks.DatabaseLayer.Sqlite
{
    public interface IDatabaseReader
    {
        bool Read();
        T Field<T>(string name);
        int FieldCount { get; }
    }
}