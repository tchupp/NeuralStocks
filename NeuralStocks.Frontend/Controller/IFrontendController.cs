using System.Data;
using NeuralStocks.DatabaseLayer.Model.Database;

namespace NeuralStocks.Frontend.Controller
{
    public interface IFrontendController
    {
        DataTable GetSearchResultsForNewCompany(string company);
        DataTable GetSearchResultsForCurrentCompany(CompanyLookupEntry company);
    }
}