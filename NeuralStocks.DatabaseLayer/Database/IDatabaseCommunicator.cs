using System.Collections.Generic;
using NeuralStocks.DatabaseLayer.StockApi;

namespace NeuralStocks.DatabaseLayer.Database
{
    public interface IDatabaseCommunicator
    {
        void CreateCompanyTable();
        void InsertCompanyToTable(CompanyLookupResponse company);
        void UpdateCompanyTimestamp(QuoteLookupResponse response);
        List<QuoteLookupRequest> SelectQuoteLookupList();
        void InsertQuoteResponseToTable(QuoteLookupResponse response);
        List<CompanyLookupEntry> SelectCompanyLookupEntryList();
        List<QuoteHistoryEntry> SelectQuoteHistoryEntryList(CompanyLookupEntry company);
    }
}