using System.Collections.Generic;
using System.Data;
using NeuralStocks.Backend.ApiCommunication;

namespace NeuralStocks.Frontend.Controller
{
    public interface IDataTableFactory
    {
        DataTable BuildCompanySearchTable(List<CompanyLookupResponse> lookupResponseList);
    }
}