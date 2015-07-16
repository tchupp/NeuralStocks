using NeuralStocks.DatabaseLayer.Database;
using NeuralStocks.DatabaseLayer.StockApi;

namespace NeuralStocks.DatabaseLayer.Sqlite
{
    public class DatabaseCommandStringFactory : IDatabaseCommandStringFactory
    {
        public static readonly IDatabaseCommandStringFactory Singleton = new DatabaseCommandStringFactory();

        private DatabaseCommandStringFactory()
        {
        }

        public string BuildCreateCompanyLookupTableCommandString()
        {
            return "CREATE TABLE Company (name TEXT, symbol TEXT, firstDate TEXT, recentDate TEXT, collect INTEGER)";
        }

        public string BuildCreateQuoteHistoryTableCommandString(CompanyLookupResponse company)
        {
            return string.Format("CREATE TABLE {0} (name TEXT, symbol TEXT, timestamp TEXT, " +
                                 "lastPrice REAL, change REAL, changePercent REAL)", company.Symbol);
        }

        public string BuildInsertCompanyToLookupTableCommandString(CompanyLookupResponse company)
        {
            return string.Format("INSERT INTO Company VALUES ('{0}', '{1}', 'null', 'null', 1)",
                company.Name, company.Symbol);
        }

        public string BuildInsertQuoteToHistoryTableCommandString(QuoteLookupResponse response)
        {
            return string.Format(
                "INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', {4}, {5}, {6})",
                response.Symbol, response.Name, response.Symbol, response.Timestamp,
                response.LastPrice, response.Change, response.ChangePercent);
        }

        public string BuildUpdateCompanyFirstDateCommandString(QuoteLookupResponse response)
        {
            return string.Format(
                "UPDATE Company SET firstDate = '{0}' WHERE Symbol = '{1}' AND firstDate = 'null'",
                response.Timestamp, response.Symbol);
        }

        public string BuildUpdateCompanyRecentTimestampCommandString(QuoteLookupResponse response)
        {
            return string.Format(
                "UPDATE Company SET recentDate = '{0}' WHERE Symbol = '{1}'",
                response.Timestamp, response.Symbol);
        }

        public string BuildSelectAllCompaniesFromLookupTableCommandString()
        {
            return "SELECT * FROM Company";
        }

        public string BuildSelectAllQuotesFromHistoryTableCommandString(CompanyLookupEntry company)
        {
            return string.Format("SELECT * FROM {0}", company.Symbol);
        }
    }
}