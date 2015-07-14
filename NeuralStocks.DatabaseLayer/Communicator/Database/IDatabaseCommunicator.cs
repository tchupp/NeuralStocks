using System.Collections.Generic;
using System.Data.SQLite;
using NeuralStocks.DatabaseLayer.Model.Database;
using NeuralStocks.DatabaseLayer.Model.StockApi;

namespace NeuralStocks.DatabaseLayer.Communicator.Database
{
    public interface IDatabaseCommunicator
    {
        void CreateDatabase(string databaseName);
        void CreateCompanyTable(SQLiteConnection connection);
        void AddCompanyToTable(SQLiteConnection connection, CompanyLookupResponse company);
        void UpdateCompanyTimestamp(SQLiteConnection connection, QuoteLookupResponse response);
        List<QuoteLookupRequest> GetQuoteLookupList(SQLiteConnection connection);
        void AddQuoteResponseToTable(SQLiteConnection connection, QuoteLookupResponse response);
        List<CompanyLookupEntry> GetCompanyLookupEntryList(SQLiteConnection connection);

        List<QuoteHistoryEntry> GetQuoteHistoryEntryList(SQLiteConnection connection,
            CompanyLookupEntry company);
    }
}