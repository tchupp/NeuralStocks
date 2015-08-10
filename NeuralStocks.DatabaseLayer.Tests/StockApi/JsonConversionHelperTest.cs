using System.Data;
using NeuralStocks.DatabaseLayer.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NUnit.Framework;

namespace NeuralStocks.DatabaseLayer.Tests.StockApi
{
    [TestFixture]
    [Category("Database")]
    public class JsonConversionHelperTest : AssertTestClass
    {
        [Test]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IJsonConversionHelper), typeof (JsonConversionHelper));
        }

        [Test]
        public void TestSingleton()
        {
            AssertPrivateContructor(typeof (JsonConversionHelper));
            Assert.AreSame(JsonConversionHelper.Singleton, JsonConversionHelper.Singleton);
            AssertIsOfTypeAndGet<JsonConversionHelper>(JsonConversionHelper.Singleton);
        }

        [Test]
        public void TestDeserializeCompanyLookupToDataTable()
        {
            var conversionHelper = JsonConversionHelper.Singleton;

            const string lookupResponse =
                "[{\"Symbol\":\"AAPL\"," +
                "\"Name\":\"Apple Inc\"," +
                "\"Exchange\":\"NASDAQ\"}," +
                "{\"Symbol\":\"AVSPY\"," +
                "\"Name\":\"AAPL ALPHA INDEX\"," +
                "\"Exchange\":\"NASDAQ\"}," +
                "{\"Symbol\":\"AIX\"," +
                "\"Name\":\"NAS OMX Alpha   AAPL vs. SPY  Settle\"," +
                "\"Exchange\":\"NASDAQ\"}]";

            var deserializedTable = conversionHelper.Deserialize<DataTable>(lookupResponse);

            Assert.AreEqual(3, deserializedTable.Columns.Count);
            Assert.AreEqual(3, deserializedTable.Rows.Count);

            Assert.AreEqual("AAPL", deserializedTable.Rows[0]["Symbol"]);
            Assert.AreEqual("Apple Inc", deserializedTable.Rows[0]["Name"]);
            Assert.AreEqual("NASDAQ", deserializedTable.Rows[0]["Exchange"]);

            Assert.AreEqual("AVSPY", deserializedTable.Rows[1]["Symbol"]);
            Assert.AreEqual("AAPL ALPHA INDEX", deserializedTable.Rows[1]["Name"]);
            Assert.AreEqual("NASDAQ", deserializedTable.Rows[1]["Exchange"]);

            Assert.AreEqual("AIX", deserializedTable.Rows[2]["Symbol"]);
            Assert.AreEqual("NAS OMX Alpha   AAPL vs. SPY  Settle", deserializedTable.Rows[2]["Name"]);
            Assert.AreEqual("NASDAQ", deserializedTable.Rows[2]["Exchange"]);
        }
    }
}