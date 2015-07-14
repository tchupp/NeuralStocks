using System.Collections.Generic;
using System.Data;
using NeuralStocks.DatabaseLayer.Model.Database;
using NeuralStocks.DatabaseLayer.Model.StockApi;

namespace NeuralStocks.Frontend.Controller
{
    public interface IDataTableFactory
    {
        DataTable BuildNewCompanySearchTable(IEnumerable<CompanyLookupResponse> lookupResponseList);
        DataTable BuildCurrentCompanySearchTable(IEnumerable<QuoteHistoryEntry> lookupResponseList);
    }
}