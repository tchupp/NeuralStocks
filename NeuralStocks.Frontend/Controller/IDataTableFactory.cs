using System.Collections.Generic;
using System.Data;
using NeuralStocks.DatabaseLayer.StockApi;

namespace NeuralStocks.Frontend.Controller
{
    public interface IDataTableFactory
    {
        DataTable BuildNewCompanySearchTable(IEnumerable<CompanyLookupResponse> lookupResponseList);
    }
}