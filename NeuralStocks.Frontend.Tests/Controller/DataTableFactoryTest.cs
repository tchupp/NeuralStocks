using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.Backend.ApiCommunication;
using NeuralStocks.Backend.Tests.Testing;
using NeuralStocks.Frontend.Controller;

namespace NeuralStocks.Frontend.Tests.Controller
{
    [TestClass]
    public class DataTableFactoryTest : AssertTestClass
    {
        [TestMethod]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IDataTableFactory), typeof (DataTableFactory));
        }

        [TestMethod]
        public void TestSingleton()
        {
            AssertPrivateContructor(typeof (DataTableFactory));
            Assert.AreSame(DataTableFactory.Factory, DataTableFactory.Factory);
        }

        [TestMethod]
        public void TestBuildCompanySearchTable()
        {
            var lookupResponseList = new List<CompanyLookupResponse>
            {
                new CompanyLookupResponse("AAPL", "Apple Inc", "NASDAQ"),
                new CompanyLookupResponse("APLE", "Apple REIT INC", "NYSE"),
                new CompanyLookupResponse("APLE", "Apple REIT INC", "BATS Trading Inc"),
                new CompanyLookupResponse("VXAPL", "CBOE Apple VIC Index", "Market Data Express")
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

            Assert.AreEqual("CBOE Apple VIC Index", companySearchTable.Rows[3]["Name"]);
            Assert.AreEqual("VXAPL", companySearchTable.Rows[3]["Symbol"]);
            Assert.AreEqual("Market Data Express", companySearchTable.Rows[3]["Exchange"]);
        }
    }
}