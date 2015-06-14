using System;
using System.Collections.Generic;
using System.Data.SQLite;
using NeuralStocks.Backend.ApiCommunication;

namespace NeuralStocks.Backend.SqlDatabase
{
    public class SqlDatabaseCommandRunner : ISqlDatabaseCommandRunner
    {
        public static readonly SqlDatabaseCommandRunner Singleton = new SqlDatabaseCommandRunner();

        private SqlDatabaseCommandRunner()
        {
        }

        public void CreateDatabase(string databaseName)
        {
            SQLiteConnection.CreateFile(databaseName);
        }

        public void CreateCompanyTable(SQLiteConnection connection)
        {
            const string createCompanyTableCommandString =
                "CREATE TABLE Company (name TEXT, symbol TEXT, firstDate TEXT, recentDate TEXT)";

            connection.Open();

            var createCompanyTableCommand = new SQLiteCommand(createCompanyTableCommandString, connection);
            createCompanyTableCommand.ExecuteNonQuery();

            connection.Close();
        }

        public void AddCompanyToTable(SQLiteConnection connection, CompanyLookupResponse company)
        {
            var addCompanyToTableCommandString =
                "INSERT INTO Company VALUES ('" + company.Name + "', '" + company.Symbol + "', 'null', 'null')";
            var createCompanyTableCommandString =
                "CREATE TABLE " + company.Symbol +
                " (name TEXT, symbol TEXT, timestamp TEXT, lastPrice REAL, change REAL, changePercent REAL)";

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
            var updateCompanyRecentDateCommandString =
                "UPDATE Company SET recentDate = '" + response.Timestamp +
                "' WHERE Symbol = '" + response.Symbol + "'";
            var updateCompanyFirstDateCommandString =
                "UPDATE Company SET firstDate = '" + response.Timestamp +
                "' WHERE Symbol = '" + response.Symbol + "' AND firstDate = 'null'";

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
            var addQuoteToTableCommandString =
                "INSERT INTO " + response.Symbol + " VALUES ('" + response.Name +
                "', '" + response.Symbol + "', '" + response.Timestamp + "', " + response.LastPrice + ", " +
                response.Change + ", " + response.ChangePercent + ")";

            connection.Open();

            var addQuoteToTableCommand = new SQLiteCommand(addQuoteToTableCommandString, connection);
            addQuoteToTableCommand.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine("Adding Quote: Company: {0}. Time: {1}. Amount: {2}.",
                response.Symbol, response.Timestamp, response.LastPrice);
        }
    }
}