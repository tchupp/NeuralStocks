using System.Data.SQLite;

namespace NeuralStocks.DatabaseLayer.Sqlite
{
    public class DatabaseReader : IDatabaseReader
    {
        public SQLiteDataReader WrappedReader { get; private set; }

        public int FieldCount
        {
            get { return WrappedReader.FieldCount; }
        }

        public DatabaseReader(SQLiteDataReader wrappedReader)
        {
            WrappedReader = wrappedReader;
        }

        public bool Read()
        {
            return WrappedReader.Read();
        }

        public T Field<T>(string name)
        {
            return (T) WrappedReader[name];
        }
    }
}