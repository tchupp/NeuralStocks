using System.Web.Mvc;

namespace NeuralStocks.WebApp.Tests.Controllers
{
    public class JasmineController : Controller
    {
        public ViewResult Run()
        {
            return View("SpecRunner");
        }
    }
}