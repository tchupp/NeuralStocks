using System.Collections.Generic;
using System.Data;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.StockApi;

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
            return ReadToDataTable(reader);
        }

        public DataTable CreateQuoteLookupTable(IDatabaseReader reader)
        {
            return ReadToDataTable(reader);
        }

        public DataTable CreateCompanyLookupTable(IDatabaseReader reader)
        {
            return ReadToDataTable(reader);
        }

        public List<QuoteLookupRequest> CreateQuoteLookupList(IDatabaseReader reader)
        {
            var lookupRequests = new List<QuoteLookupRequest>();
            while (reader.Read())
            {
                var company = reader.Field<string>("symbol");
                var timestamp = reader.Field<string>("recentDate");
                var lookupRequest = new QuoteLookupRequest()
                {
                    Company = company,
                    Timestamp = timestamp
                };
                lookupRequests.Add(lookupRequest);
            }
            return lookupRequests;
        }

        private static DataTable ReadToDataTable(IDatabaseReader reader)
        {
            var dataTable = new DataTable();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                var columnName = reader.GetColumnName(i);
                var fieldType = reader.GetFieldType(i);
                dataTable.Columns.Add(columnName, fieldType);
            }

            while (reader.Read())
            {
                var args = new object[reader.FieldCount];
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    args[i] = reader.Field(reader.GetColumnName(i));
                }
                dataTable.Rows.Add(args);
            }
            return dataTable;
        }
    }
}