using System.Web.Mvc;
using NeuralStocks.DatabaseLayer.StockApi;

namespace NeuralStocks.WebApp.Controllers
{
    public class AnalysisController : Controller
    {
        private readonly IStockMarketApiCommunicator _stockApiCommunicator;
        private readonly IJsonConversionHelper _helper;

        public AnalysisController()
        {
            _stockApiCommunicator = StockMarketApiCommunicator.Singleton;
            _helper = JsonConversionHelper.Singleton;
        }

        public AnalysisController(IStockMarketApiCommunicator stockApiCommunicator, IJsonConversionHelper helper)
        {
            _stockApiCommunicator = stockApiCommunicator;
            _helper = helper;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetCompanyLookup(string companySearch)
        {
            var responseList = _stockApiCommunicator.CompanyLookup(companySearch);
            return _helper.Serialize(responseList);
        }
    }
}