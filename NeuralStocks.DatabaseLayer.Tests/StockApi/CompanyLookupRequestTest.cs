using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.DatabaseLayer.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;

namespace NeuralStocks.DatabaseLayer.Tests.StockApi
{
    [TestClass]
    public class CompanyLookupRequestTest : AssertTestClass
    {
        [TestMethod, TestCategory("StockApi")]
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