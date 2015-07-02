﻿using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using NeuralStocks.Backend.ApiCommunication;
using NeuralStocks.Backend.Database;
using NeuralStocks.Frontend.Database;

namespace NeuralStocks.Frontend.Controller
{
    public class FrontendController : IFrontendController
    {
        public IStockMarketApiCommunicator StockCommunicator { get; private set; }
        public IDataTableFactory TableFactory { get; private set; }
        public IDatabaseCommunicator DatabaseCommunicator { get; private set; }

        public FrontendController(IStockMarketApiCommunicator stockCommunicator, IDataTableFactory tableFactory, IDatabaseCommunicator databaseCommunicator)
        {
            StockCommunicator = stockCommunicator;
            TableFactory = tableFactory;
            DatabaseCommunicator = databaseCommunicator;
        }

        public DataTable GetSearchResultsForNewCompany(string company)
        {
            var lookupRequest = new CompanyLookupRequest(company);
            var lookupResponseList = StockCommunicator.CompanyLookup(lookupRequest);
            var buildCompanySearchTable = TableFactory.BuildNewCompanySearchTable(lookupResponseList);

            return buildCompanySearchTable;
        }

        public DataTable GetSearchResultsForCurrentCompany(CompanyLookupEntry company)
        {
            var quoteHistoryEntryList = DatabaseCommunicator.GetQuoteHistoryEntryList(new SQLiteConnection(), company);
            var buildCompanySearchTable = TableFactory.BuildCurrentCompanySearchTable(quoteHistoryEntryList);

            return buildCompanySearchTable;
        }
    }
}