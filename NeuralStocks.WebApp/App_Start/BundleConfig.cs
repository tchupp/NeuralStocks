using System.Web.Optimization;

namespace NeuralStocks.WebApp
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/jquery.dataTables.css",
                "~/Content/bootstrap.css",
                "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/external").Include(
                "~/Scripts/External/jquery.dataTables.min.js",
                "~/Scripts/External/jquery-{version}.js",
                "~/Scripts/External/jquery.validate*",

                "~/Scripts/External/bootstrap.js",
                "~/Scripts/External/respond.js",
                "~/Scripts/External/highstock.js"));

            bundles.Add(new ScriptBundle("~/bundles/siteSpecific").Include(
                "~/Scripts/SiteSpecific/Analysis/AnalysisMain.js",
                "~/Scripts/SiteSpecific/Analysis/StockSearchPresenter.js",
                "~/Scripts/SiteSpecific/Analysis/StockSearchView.js",

                "~/Scripts/SiteSpecific/Util/AjaxRequestHandler.js",

                "~/Scripts/SiteSpecific/Components/Chart/ChartOptions.js",
                "~/Scripts/SiteSpecific/Components/Chart/ChartSetupManager.js",
                "~/Scripts/SiteSpecific/Components/Table/TableOptions.js",
                "~/Scripts/SiteSpecific/Components/Table/TableSetupManager.js"));
        }
    }
}