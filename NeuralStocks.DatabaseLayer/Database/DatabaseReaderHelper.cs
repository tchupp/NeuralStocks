using System.Data;
using NeuralStocks.DatabaseLayer.Sqlite;

namespace NeuralStocks.DatabaseLayer.Database
{
    public class DatabaseReaderHelper : IDatabaseReaderHelper
    {
        public static readonly IDatabaseReaderHelper Singleton = new DatabaseReaderHelper();

        private DatabaseReaderHelper()
        {
        }

        public DataTable CreateQuoteHistoryTable(IDatabaseReader reader)
        {
            var quoteHistoryDataTable = new DataTable();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                var columnName = reader.GetColumnName(i);
                var fieldType = reader.GetFieldType(i);
                quoteHistoryDataTable.Columns.Add(columnName, fieldType);
            }

            while (reader.Read())
            {
                var args = new object[reader.FieldCount];
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    args[i] = reader.Field(reader.GetColumnName(i));
                }
                quoteHistoryDataTable.Rows.Add(args);
            }
            return quoteHistoryDataTable;
        }

        public DataTable CreateQuoteLookupTable(IDatabaseReader reader)
        {
            return new DataTable();
        }

        public DataTable CreateCompanyLookupTable(IDatabaseReader reader)
        {
            return new DataTable();
        }
    }
}