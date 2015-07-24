using System.Web.Optimization;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NUnit.Framework;

namespace NeuralStocks.WebApp.Tests
{
    [TestFixture]
    public class BundleConfigTest : AssertTestClass
    {
        private const int ExternalScriptCount = 7;

        private static void CheckExternalScriptsBundle(BundleCollection bundle)
        {
            Assert.AreEqual(ExternalScriptCount, bundle.Count);

            var bundleCollection = bundle.GetRegisteredBundles();
            Assert.AreEqual("~/bundles/jquery", bundleCollection[0].Path);
            Assert.AreEqual("~/bundles/jqueryval", bundleCollection[1].Path);
            Assert.AreEqual("~/bundles/modernizr", bundleCollection[2].Path);
            Assert.AreEqual("~/bundles/bootstrap", bundleCollection[3].Path);
            Assert.AreEqual("~/Content/css", bundleCollection[4].Path);
            Assert.AreEqual("~/bundles/sinon", bundleCollection[5].Path);
            Assert.AreEqual("~/bundles/highchart", bundleCollection[6].Path);
        }

        [Test]
        public void TestBundle()
        {
            var bundle = new BundleCollection();
            BundleConfig.RegisterBundles(bundle);

            CheckExternalScriptsBundle(bundle);
        }
    }
}