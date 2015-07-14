using System.Collections.Generic;
using System.Data.SQLite;
using NeuralStocks.DatabaseLayer.Model.Database;
using NeuralStocks.DatabaseLayer.Model.StockApi;

namespace NeuralStocks.DatabaseLayer.Communicator.Database
{
    public interface IDatabaseCommunicator
    {
        void CreateDatabase(string databaseName);
        void CreateCompanyTable();
        void AddCompanyToTable(CompanyLookupResponse company);
        void UpdateCompanyTimestamp(QuoteLookupResponse response);
        List<QuoteLookupRequest> GetQuoteLookupList();
        void AddQuoteResponseToTable(QuoteLookupResponse response);
        List<CompanyLookupEntry> GetCompanyLookupEntryList();
        List<QuoteHistoryEntry> GetQuoteHistoryEntryList(CompanyLookupEntry company);
    }
}