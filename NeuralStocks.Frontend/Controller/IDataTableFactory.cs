using System.Collections.Generic;
using System.Data;
using NeuralStocks.Backend.ApiCommunication;
using NeuralStocks.Frontend.Database;

namespace NeuralStocks.Frontend.Controller
{
    public interface IDataTableFactory
    {
        DataTable BuildNewCompanySearchTable(IEnumerable<CompanyLookupResponse> lookupResponseList);
        DataTable BuildCurrentCompanySearchTable(IEnumerable<QuoteHistoryEntry> lookupResponseList);
    }
}