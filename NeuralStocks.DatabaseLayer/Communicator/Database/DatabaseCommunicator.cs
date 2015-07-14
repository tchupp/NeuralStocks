using System;
using System.Collections.Generic;
using System.Data.SQLite;
using NeuralStocks.DatabaseLayer.Model.Database;
using NeuralStocks.DatabaseLayer.Model.StockApi;
using NeuralStocks.DatabaseLayer.Sqlite;

namespace NeuralStocks.DatabaseLayer.Communicator.Database
{
    public class DatabaseCommunicator : IDatabaseCommunicator
    {
        public IDatabaseConnection Connection { get; private set; }
        public IDatabaseCommandStringFactory Factory { get; set; }

        public DatabaseCommunicator(IDatabaseConnection connection)
        {
            Connection = connection;
            Factory = DatabaseCommandStringFactory.Singleton;
        }

        public void CreateDatabase(string databaseName)
        {
        }

        public List<QuoteHistoryEntry> GetQuoteHistoryEntryList(CompanyLookupEntry company)
        {
            var selectAllFromCompanyCommandString = Factory.BuildSelectAllQuotesFromHistoryTableCommandString(company);

            var selectAllFromCompanyCommand = Connection.CreateCommand(selectAllFromCompanyCommandString);
            var selectAllFromCompanyCommandReader = selectAllFromCompanyCommand.ExecuteReader();

            var quoteHistoryEntryList = new List<QuoteHistoryEntry>();
            while (selectAllFromCompanyCommandReader.Read())
            {
                var historyEntry = new QuoteHistoryEntry
                {
                    Name = selectAllFromCompanyCommandReader.Field<string>("name"),
                    Symbol = selectAllFromCompanyCommandReader.Field<string>("symbol"),
                    Timestamp = selectAllFromCompanyCommandReader.Field<string>("timestamp"),
                    LastPrice = selectAllFromCompanyCommandReader.Field<double>("lastPrice"),
                    Change = selectAllFromCompanyCommandReader.Field<double>("change"),
                    ChangePercent = selectAllFromCompanyCommandReader.Field<double>("changePercent")
                };
                quoteHistoryEntryList.Add(historyEntry);
            }

            return quoteHistoryEntryList;
        }

        public List<QuoteLookupRequest> GetQuoteLookupList()
        {
            var selectFromCompanyCommandString = Factory.BuildSelectAllCompaniesFromLookupTableCommandString();

            var selectFromCompanyCommand = Connection.CreateCommand(selectFromCompanyCommandString);
            var selectFromCompanyCommandReader = selectFromCompanyCommand.ExecuteReader();

            var lookupRequests = new List<QuoteLookupRequest>();
            while (selectFromCompanyCommandReader.Read())
            {
                var symbol = selectFromCompanyCommandReader.Field<string>("symbol");
                var timestamp = selectFromCompanyCommandReader.Field<string>("recentDate");
                var request = new QuoteLookupRequest {Company = symbol, Timestamp = timestamp};
                lookupRequests.Add(request);
            }

            return lookupRequests;
        }

        public List<CompanyLookupEntry> GetCompanyLookupEntryList()
        {
            var selectAllFromCompanyCommandString = Factory.BuildSelectAllCompaniesFromLookupTableCommandString();

            var selectAllFromCompanyCommand = Connection.CreateCommand(selectAllFromCompanyCommandString);
            var selectAllFromCompanyCommandReader = selectAllFromCompanyCommand.ExecuteReader();

            var companyLookupEntryList = new List<CompanyLookupEntry>();
            while (selectAllFromCompanyCommandReader.Read())
            {
                var lookupEntry = new CompanyLookupEntry
                {
                    Name = selectAllFromCompanyCommandReader.Field<string>("name"),
                    Symbol = selectAllFromCompanyCommandReader.Field<string>("symbol"),
                    FirstDate = selectAllFromCompanyCommandReader.Field<string>("firstDate"),
                    RecentDate = selectAllFromCompanyCommandReader.Field<string>("recentDate"),
                    Collection = selectAllFromCompanyCommandReader.Field<int>("collect") > 0
                };
                companyLookupEntryList.Add(lookupEntry);
            }

            return companyLookupEntryList;
        }

        public void CreateCompanyTable()
        {
            var commandString = Factory.BuildCreateCompanyLookupTableCommandString();
            var databaseCommand = Connection.CreateCommand(commandString);

            databaseCommand.ExecuteNonQuery();
        }

        public void AddCompanyToTable(CompanyLookupResponse response)
        {
            var createQuoteTableCommandString = Factory.BuildCreateQuoteHistoryTableCommandString(response);
            var insertCompanyLookupCommandString = Factory.BuildInsertCompanyToLookupTableCommandString(response);
            var createQuoteTableCommand = Connection.CreateCommand(createQuoteTableCommandString);
            var insertCompanyLookupCommand = Connection.CreateCommand(insertCompanyLookupCommandString);

            createQuoteTableCommand.ExecuteNonQuery();
            insertCompanyLookupCommand.ExecuteNonQuery();

            Console.WriteLine("Added {0} to company lookup table, " +
                              "and added a quote history table : {1}.", response.Name, response.Symbol);
        }

        public void UpdateCompanyTimestamp(QuoteLookupResponse response)
        {
            var updateFirstDateCommandString = Factory.BuildUpdateCompanyFirstDateCommandString(response);
            var updateRecentDateCommandString = Factory.BuildUpdateCompanyRecentTimestampCommandString(response);
            var updateFirstDateCommand = Connection.CreateCommand(updateFirstDateCommandString);
            var updateRecentDateCommand = Connection.CreateCommand(updateRecentDateCommandString);

            updateFirstDateCommand.ExecuteNonQuery();
            updateRecentDateCommand.ExecuteNonQuery();

            Console.WriteLine("Updating Timestamp: Company: {0}. Time: {1}", response.Symbol, response.Timestamp);
        }

        public void AddQuoteResponseToTable(QuoteLookupResponse response)
        {
            var addQuoteToTableCommandString = Factory.BuildInsertQuoteToHistoryTableCommandString(response);
            var addQuoteToTableCommand = Connection.CreateCommand(addQuoteToTableCommandString);

            addQuoteToTableCommand.ExecuteNonQuery();

            Console.WriteLine("Adding Quote: Company: {0}. Time: {1}. Amount: {2}.",
                response.Symbol, response.Timestamp, response.LastPrice);
        }
    }
}