using System.Collections.Generic;
using NeuralStocks.DatabaseLayer.StockApi;

namespace NeuralStocks.DatabaseLayer.Database
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