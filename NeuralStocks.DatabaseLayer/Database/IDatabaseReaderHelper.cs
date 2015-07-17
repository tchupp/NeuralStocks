using System.Data;
using NeuralStocks.DatabaseLayer.Sqlite;

namespace NeuralStocks.DatabaseLayer.Database
{
    public interface IDatabaseReaderHelper
    {
        DataTable CreateQuoteHistoryTable(IDatabaseReader reader);
        DataTable CreateQuoteLookupTable(IDatabaseReader reader);
        DataTable CreateCompanyLookupTable(IDatabaseReader reader);
    }
}