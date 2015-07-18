using System;
using System.Collections.Generic;
using System.Data;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.StockApi;

namespace NeuralStocks.DatabaseLayer.Database
{
    public class DatabaseCommunicator : IDatabaseCommunicator
    {
        public IDatabaseConnection Connection { get; private set; }
        public IDatabaseCommandStringFactory Factory { get; set; }
        public IDatabaseReaderHelper ReaderHelper { get; set; }

        public DatabaseCommunicator(IDatabaseConnection connection)
        {
            Connection = connection;
            Factory = DatabaseCommandStringFactory.Singleton;
            ReaderHelper = DatabaseReaderHelper.Singleton;
        }

        public void CreateCompanyTable()
        {
            var commandString = Factory.BuildCreateCompanyLookupTableCommandString();
            var databaseCommand = Connection.CreateCommand(commandString);

            Connection.Open();
            databaseCommand.ExecuteNonQuery();
            Connection.Close();
        }

        public void InsertCompanyToTable(CompanyLookupResponse response)
        {
            var createQuoteTableCommandString = Factory.BuildCreateQuoteHistoryTableCommandString(response);
            var insertCompanyLookupCommandString = Factory.BuildInsertCompanyToLookupTableCommandString(response);
            var createQuoteTableCommand = Connection.CreateCommand(createQuoteTableCommandString);
            var insertCompanyLookupCommand = Connection.CreateCommand(insertCompanyLookupCommandString);

            Connection.Open();
            createQuoteTableCommand.ExecuteNonQuery();
            insertCompanyLookupCommand.ExecuteNonQuery();
            Connection.Close();

            Console.WriteLine("Added {0} to company lookup table, " +
                              "and added a quote history table : {1}.", response.Name, response.Symbol);
        }

        public void InsertQuoteResponseToTable(QuoteLookupResponse response)
        {
            var addQuoteToTableCommandString = Factory.BuildInsertQuoteToHistoryTableCommandString(response);
            var addQuoteToTableCommand = Connection.CreateCommand(addQuoteToTableCommandString);

            Connection.Open();
            addQuoteToTableCommand.ExecuteNonQuery();
            Connection.Close();

            Console.WriteLine("Adding Quote: Company: {0}. Time: {1}. Amount: {2}.",
                response.Symbol, response.Timestamp, response.LastPrice);
        }

        public void UpdateCompanyTimestamp(QuoteLookupResponse response)
        {
            var updateFirstDateCommandString = Factory.BuildUpdateCompanyFirstDateCommandString(response);
            var updateRecentDateCommandString = Factory.BuildUpdateCompanyRecentTimestampCommandString(response);
            var updateFirstDateCommand = Connection.CreateCommand(updateFirstDateCommandString);
            var updateRecentDateCommand = Connection.CreateCommand(updateRecentDateCommandString);

            Connection.Open();
            updateFirstDateCommand.ExecuteNonQuery();
            updateRecentDateCommand.ExecuteNonQuery();
            Connection.Close();

            Console.WriteLine("Updating Timestamp: Company: {0}. Time: {1}", response.Symbol, response.Timestamp);
        }

        public List<QuoteLookupRequest> SelectQuoteLookupTable()
        {
            var selectFromCompanyCommandString = Factory.BuildSelectAllCompaniesFromLookupTableCommandString();
            var selectFromCompanyCommand = Connection.CreateCommand(selectFromCompanyCommandString);

            Connection.Open();
            var dataReader = selectFromCompanyCommand.ExecuteReader();
            var lookupRequestList = ReaderHelper.CreateQuoteLookupList(dataReader);
            Connection.Close();

            return lookupRequestList;
        }

        public DataTable SelectCompanyLookupTable()
        {
            var selectAllFromCompanyCommandString = Factory.BuildSelectAllCompaniesFromLookupTableCommandString();
            var selectAllFromCompanyCommand = Connection.CreateCommand(selectAllFromCompanyCommandString);

            Connection.Open();
            var dataReader = selectAllFromCompanyCommand.ExecuteReader();
            var companyLookupTable = ReaderHelper.CreateCompanyLookupTable(dataReader);
            Connection.Close();

            return companyLookupTable;
        }

        public DataTable SelectCompanyQuoteHistoryTable(CompanyLookupEntry company)
        {
            var selectAllFromCompanyCommandString = Factory.BuildSelectAllQuotesFromHistoryTableCommandString(company);
            var selectAllFromCompanyCommand = Connection.CreateCommand(selectAllFromCompanyCommandString);

            Connection.Open();
            var dataReader = selectAllFromCompanyCommand.ExecuteReader();
            var quoteHistoryTable = ReaderHelper.CreateQuoteHistoryTable(dataReader);
            Connection.Close();

            return quoteHistoryTable;
        }
    }
}