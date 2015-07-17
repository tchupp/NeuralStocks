using System.Collections.Generic;
using System.Data;
using NeuralStocks.DatabaseLayer.StockApi;

namespace NeuralStocks.DatabaseLayer.Database
{
    public interface IDatabaseCommunicator
    {
        void CreateCompanyTable();
        void InsertCompanyToTable(CompanyLookupResponse company);
        void InsertQuoteResponseToTable(QuoteLookupResponse response);
        void UpdateCompanyTimestamp(QuoteLookupResponse response);
        List<QuoteLookupRequest> SelectQuoteLookupTable();
        DataTable SelectCompanyLookupTable();
        DataTable SelectCompanyQuoteHistoryTable(CompanyLookupEntry company);
    }
}