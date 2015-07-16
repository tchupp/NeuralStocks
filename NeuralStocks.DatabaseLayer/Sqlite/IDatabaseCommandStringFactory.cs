using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.StockApi;

namespace NeuralStocks.DatabaseLayer.Sqlite
{
    public interface IDatabaseCommandStringFactory
    {
        string BuildCreateCompanyLookupTableCommandString();
        string BuildCreateQuoteHistoryTableCommandString(CompanyLookupResponse company);
        string BuildInsertCompanyToLookupTableCommandString(CompanyLookupResponse company);
        string BuildInsertQuoteToHistoryTableCommandString(QuoteLookupResponse response);
        string BuildUpdateCompanyFirstDateCommandString(QuoteLookupResponse response);
        string BuildUpdateCompanyRecentTimestampCommandString(QuoteLookupResponse response);
        string BuildSelectAllCompaniesFromLookupTableCommandString();
        string BuildSelectAllQuotesFromHistoryTableCommandString(CompanyLookupEntry company);
    }
}