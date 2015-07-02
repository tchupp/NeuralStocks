using System;
using System.Collections.Generic;
using System.Data.SQLite;
using NeuralStocks.Backend.ApiCommunication;
using NeuralStocks.Frontend.Database;

namespace NeuralStocks.Backend.Database
{
    public class DatabaseCommunicator : IDatabaseCommunicator
    {
        public static readonly DatabaseCommunicator Singleton = new DatabaseCommunicator();

        private DatabaseCommunicator()
        {
        }

        public void CreateDatabase(string databaseName)
        {
            SQLiteConnection.CreateFile(databaseName);
        }

        public void CreateCompanyTable(SQLiteConnection connection)
        {
            const string createCompanyTableCommandString =
                "CREATE TABLE Company (name TEXT, symbol TEXT, firstDate TEXT, recentDate TEXT, collect INTEGER)";

            connection.Open();

            var createCompanyTableCommand = new SQLiteCommand(createCompanyTableCommandString, connection);
            createCompanyTableCommand.ExecuteNonQuery();

            connection.Close();
        }

        public void AddCompanyToTable(SQLiteConnection connection, CompanyLookupResponse company)
        {
            var addCompanyToTableCommandString =
                string.Format("INSERT INTO Company VALUES ('{0}', '{1}', 'null', 'null', 1)",
                    company.Name, company.Symbol);
            var createCompanyTableCommandString =
                string.Format("CREATE TABLE {0} (name TEXT, symbol TEXT, timestamp TEXT, " +
                              "lastPrice REAL, change REAL, changePercent REAL)", company.Symbol);

            connection.Open();

            var addCompanyToTableCommand = new SQLiteCommand(addCompanyToTableCommandString, connection);
            addCompanyToTableCommand.ExecuteNonQuery();

            var createCompanyTableCommand = new SQLiteCommand(createCompanyTableCommandString, connection);
            createCompanyTableCommand.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine("Added {0} to company lookup table, " +
                              "and added a quote history table : {1}.", company.Name, company.Symbol);
        }

        public void UpdateCompanyTimestamp(SQLiteConnection connection, QuoteLookupResponse response)
        {
            var updateCompanyRecentDateCommandString = string.Format(
                "UPDATE Company SET recentDate = '{0}' WHERE Symbol = '{1}'",
                response.Timestamp, response.Symbol);
            var updateCompanyFirstDateCommandString = string.Format(
                "UPDATE Company SET firstDate = '{0}' WHERE Symbol = '{1}' AND firstDate = 'null'",
                response.Timestamp, response.Symbol);

            connection.Open();

            var updateCompanyRecentDateCommand = new SQLiteCommand(updateCompanyRecentDateCommandString, connection);
            updateCompanyRecentDateCommand.ExecuteNonQuery();

            var updateCompanyFirstDateCommand = new SQLiteCommand(updateCompanyFirstDateCommandString, connection);
            updateCompanyFirstDateCommand.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine("Updating Timestamp: Company: {0}. Time: {1}", response.Symbol, response.Timestamp);
        }

        public List<QuoteLookupRequest> GetQuoteLookupList(SQLiteConnection connection)
        {
            const string selectFromCompanyCommandString = "SELECT symbol, recentDate FROM Company";

            connection.Open();

            var selectFromCompanyCommand = new SQLiteCommand(selectFromCompanyCommandString, connection);
            var selectFromCompanyCommandReader = selectFromCompanyCommand.ExecuteReader();

            var lookupRequests = new List<QuoteLookupRequest>();
            while (selectFromCompanyCommandReader.Read())
            {
                var companySymbol = selectFromCompanyCommandReader.GetString(0);
                var companyTimestamp = selectFromCompanyCommandReader.GetString(1);
                var companyLookupRequest = new QuoteLookupRequest(companySymbol, companyTimestamp);
                lookupRequests.Add(companyLookupRequest);
            }

            connection.Close();
            return lookupRequests;
        }

        public void AddQuoteResponseToTable(SQLiteConnection connection, QuoteLookupResponse response)
        {
            var addQuoteToTableCommandString = string.Format(
                "INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', {4}, {5}, {6})",
                response.Symbol, response.Name, response.Symbol, response.Timestamp,
                response.LastPrice, response.Change, response.ChangePercent);

            connection.Open();

            var addQuoteToTableCommand = new SQLiteCommand(addQuoteToTableCommandString, connection);
            addQuoteToTableCommand.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine("Adding Quote: Company: {0}. Time: {1}. Amount: {2}.",
                response.Symbol, response.Timestamp, response.LastPrice);
        }

        public List<CompanyLookupEntry> GetCompanyLookupEntryList(SQLiteConnection connection)
        {
            const string selectAllFromCompanyCommandString = "SELECT * FROM Company";

            connection.Open();

            var selectAllFromCompanyCommand = new SQLiteCommand(selectAllFromCompanyCommandString, connection);
            var selectAllFromCompanyCommandReader = selectAllFromCompanyCommand.ExecuteReader();

            var companyLookupEntryList = new List<CompanyLookupEntry>();
            while (selectAllFromCompanyCommandReader.Read())
            {
                var lookupEntry = new CompanyLookupEntry
                {
                    Name = selectAllFromCompanyCommandReader.GetString(0),
                    Symbol = selectAllFromCompanyCommandReader.GetString(1),
                    FirstDate = selectAllFromCompanyCommandReader.GetString(2),
                    RecentDate = selectAllFromCompanyCommandReader.GetString(3),
                    Collection = selectAllFromCompanyCommandReader.GetBoolean(4)
                };
                companyLookupEntryList.Add(lookupEntry);
            }
            connection.Close();

            return companyLookupEntryList;
        }

        public List<QuoteHistoryEntry> GetQuoteHistoryEntryList(SQLiteConnection connection,
            CompanyLookupEntry company)
        {
            var selectAllFromCompanyCommandString = string.Format("SELECT * FROM {0}", company.Symbol);

            connection.Open();

            var selectAllFromCompanyCommand = new SQLiteCommand(selectAllFromCompanyCommandString, connection);
            var selectAllFromCompanyCommandReader = selectAllFromCompanyCommand.ExecuteReader();

            var quoteHistoryEntryList = new List<QuoteHistoryEntry>();
            while (selectAllFromCompanyCommandReader.Read())
            {
                var historyEntry = new QuoteHistoryEntry
                {
                    Name = selectAllFromCompanyCommandReader.GetFieldValue<string>(0),
                    Symbol = selectAllFromCompanyCommandReader.GetString(1),
                    Timestamp = selectAllFromCompanyCommandReader.GetString(2),
                    LastPrice = selectAllFromCompanyCommandReader.GetDouble(3),
                    Change = selectAllFromCompanyCommandReader.GetDouble(4),
                    ChangePercent = selectAllFromCompanyCommandReader.GetDouble(5)
                };
                quoteHistoryEntryList.Add(historyEntry);
            }
            return quoteHistoryEntryList;
        }
    }
}