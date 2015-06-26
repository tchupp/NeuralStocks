using System.Data;

namespace NeuralStocks.Frontend.Controller
{
    public interface IFrontendController
    {
        DataTable GetSearchResultsForCompany(string company);
    }
}