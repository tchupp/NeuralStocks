using System.Web.Mvc;
using NeuralStocks.DatabaseLayer.StockApi;

namespace NeuralStocks.WebApp.Controllers
{
    public class AnalysisController : Controller
    {
        private readonly IStockMarketApiCommunicator _stockApiCommunicator;

        public AnalysisController()
        {
            _stockApiCommunicator = StockMarketApiCommunicator.Singleton;
        }

        public AnalysisController(IStockMarketApiCommunicator stockApiCommunicator)
        {
            _stockApiCommunicator = stockApiCommunicator;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public void GetCompanyLookup(string companySearch)
        {
            var responseList = _stockApiCommunicator.CompanyLookup(new CompanyLookupRequest {Company = companySearch});
        }
    }
}