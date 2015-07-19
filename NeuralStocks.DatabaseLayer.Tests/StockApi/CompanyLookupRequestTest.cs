using NeuralStocks.DatabaseLayer.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace NeuralStocks.DatabaseLayer.Tests.StockApi
{
    [TestFixture]
    public class CompanyLookupRequestTest : AssertTestClass
    {
        [Test]
        [Category("StockApi")]
        public void TestCompanyLookupRequestConstructedWithCorrectCompany()
        {
            const string company1 = "NFLX";
            const string company2 = "AAPL";

            var companyLookupRequest1 = new CompanyLookupRequest
            {
                Company = company1
            };
            var companyLookupRequest2 = new CompanyLookupRequest
            {
                Company = company2
            };

            Assert.AreEqual(company1, companyLookupRequest1.Company);
            Assert.AreEqual(company2, companyLookupRequest2.Company);
        }
    }
}