using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.DatabaseLayer.Model.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NeuralStocks.Frontend.Controller;

namespace NeuralStocks.Frontend.Tests.Controller
{
    [TestClass]
    public class DataTableFactoryTest : AssertTestClass
    {
        [TestMethod, TestCategory("Frontend")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IDataTableFactory), typeof (DataTableFactory));
        }

        [TestMethod, TestCategory("Frontend")]
        public void TestSingleton()
        {
            AssertPrivateContructor(typeof (DataTableFactory));
            Assert.AreSame(DataTableFactory.Factory, DataTableFactory.Factory);
        }

        [TestMethod, TestCategory("Frontend")]
        public void TestBuildCompanySearchTable()
        {
            var lookupResponseList = new List<CompanyLookupResponse>
            {
                new CompanyLookupResponse {Symbol = "AAPL", Name = "Apple Inc", Exchange = "NASDAQ"},
                new CompanyLookupResponse {Symbol = "APLE", Name = "Apple REIT INC", Exchange = "NYSE"},
                new CompanyLookupResponse {Symbol = "APLE", Name = "Apple REIT INC", Exchange = "BATS Trading Inc"},
                new CompanyLookupResponse {Symbol = "VXAPL", Name = "CBOE Apple VIC", Exchange = "Market Data Express"}
            };

            var factory = DataTableFactory.Factory;

            var companySearchTable = factory.BuildNewCompanySearchTable(lookupResponseList);

            Assert.AreEqual(3, companySearchTable.Columns.Count);
            Assert.AreEqual("Name", companySearchTable.Columns[0].ToString());
            Assert.AreEqual("Symbol", companySearchTable.Columns[1].ToString());
            Assert.AreEqual("Exchange", companySearchTable.Columns[2].ToString());

            Assert.AreEqual(4, companySearchTable.Rows.Count);
            Assert.AreEqual("Apple Inc", companySearchTable.Rows[0]["Name"]);
            Assert.AreEqual("AAPL", companySearchTable.Rows[0]["Symbol"]);
            Assert.AreEqual("NASDAQ", companySearchTable.Rows[0]["Exchange"]);

            Assert.AreEqual("Apple REIT INC", companySearchTable.Rows[1]["Name"]);
            Assert.AreEqual("APLE", companySearchTable.Rows[1]["Symbol"]);
            Assert.AreEqual("NYSE", companySearchTable.Rows[1]["Exchange"]);

            Assert.AreEqual("Apple REIT INC", companySearchTable.Rows[2]["Name"]);
            Assert.AreEqual("APLE", companySearchTable.Rows[2]["Symbol"]);
            Assert.AreEqual("BATS Trading Inc", companySearchTable.Rows[2]["Exchange"]);

            Assert.AreEqual("CBOE Apple VIC", companySearchTable.Rows[3]["Name"]);
            Assert.AreEqual("VXAPL", companySearchTable.Rows[3]["Symbol"]);
            Assert.AreEqual("Market Data Express", companySearchTable.Rows[3]["Exchange"]);
        }
    }
}