using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NeuralStocks.WebApp.Startup))]
namespace NeuralStocks.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
