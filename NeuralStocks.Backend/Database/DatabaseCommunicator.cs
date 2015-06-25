using System;
using System.Collections.Generic;
using System.Data.SQLite;
using NeuralStocks.Backend.ApiCommunication;

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

        public List<QuoteLookupRequest> GetQuoteLookupsFromTable(SQLiteConnection connection)
        {
            const string selectFromCompanyCommandString = "SELECT symbol, recentDate FROM Company";

            connection.Open();

            var selectFromCompanyCommand = new SQLiteCommand(selectFromCompanyCommandString, connection);
            var selectFromCompanyCommandReader = selectFromCompanyCommand.ExecuteReader();

            var lookupRequests = new List<QuoteLookupRequest>();
            while (selectFromCompanyCommandReader.Read())
            {
                var companySymbol = selectFromCompanyCommandReader["symbol"] as string;
                var companyTimestamp = selectFromCompanyCommandReader["recentDate"] as string;
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
    }
}