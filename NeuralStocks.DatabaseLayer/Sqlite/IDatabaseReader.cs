using System;

namespace NeuralStocks.DatabaseLayer.Sqlite
{
    public interface IDatabaseReader
    {
        int FieldCount { get; }
        bool Read();
        T Field<T>(string name);
        Type GetFieldType(int ordinal);
        string GetColumnName(int ordinal);
        object Field(string name);
    }
}