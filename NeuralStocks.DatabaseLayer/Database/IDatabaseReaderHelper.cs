using System.Collections.Generic;
using System.Data;
using NeuralStocks.DatabaseLayer.Sqlite;
using NeuralStocks.DatabaseLayer.StockApi;

namespace NeuralStocks.DatabaseLayer.Database
{
    public interface IDatabaseReaderHelper
    {
        DataTable CreateQuoteHistoryTable(IDatabaseReader reader);
        DataTable CreateQuoteLookupTable(IDatabaseReader reader);
        DataTable CreateCompanyLookupTable(IDatabaseReader reader);
        List<QuoteLookupRequest> CreateQuoteLookupList(IDatabaseReader reader);
    }
}