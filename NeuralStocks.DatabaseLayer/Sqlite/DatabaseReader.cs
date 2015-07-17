using System;
using System.Data.SQLite;

namespace NeuralStocks.DatabaseLayer.Sqlite
{
    public class DatabaseReader : IDatabaseReader
    {
        public SQLiteDataReader WrappedReader { get; private set; }

        public DatabaseReader(SQLiteDataReader wrappedReader)
        {
            WrappedReader = wrappedReader;
        }

        public int FieldCount
        {
            get { return WrappedReader.FieldCount; }
        }

        public bool Read()
        {
            return WrappedReader.Read();
        }

        public T Field<T>(string name)
        {
            return (T) WrappedReader[name];
        }

        public Type GetFieldType(int ordinal)
        {
            return WrappedReader.GetFieldType(ordinal);
        }

        public string GetColumnName(int ordinal)
        {
            return WrappedReader.GetName(ordinal);
        }

        public object Field(string name)
        {
            return WrappedReader[name];
        }
    }
}