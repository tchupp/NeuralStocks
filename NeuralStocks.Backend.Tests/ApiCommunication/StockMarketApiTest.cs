using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.Backend.ApiCommunication;
using NeuralStocks.Backend.Tests.Testing;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace NeuralStocks.Backend.Tests.ApiCommunication
{
    [TestClass]
    public class StockMarketApiTest : AssertTestClass
    {
        private const string JsonSchemaLookup =
            "{\"$schema\":\"http://json-schema.org/draft-04/schema#\"," +
            "\"id\":\"\",\"type\":\"array\"," +
            "\"items\":{\"id\":\"/2\",\"type\":\"object\"," +
            "\"properties\":{" +
            "\"Symbol\":{\"id\":\"/2/Symbol\",\"type\":\"string\"}," +
            "\"Name\":{\"id\":\"/2/Name\",\"type\":\"string\"}," +
            "\"Exchange\":{\"id\":\"/2/Exchange\",\"type\":\"string\"}}}}";

        private const string JsonSchemaQuote =
            "{\"$schema\":\"http://json-schema.org/draft-04/schema#\"," +
            "\"id\":\"\",\"type\":\"object\"," +
            "\"properties\":{" +
            "\"Status\":{\"id\":\"/Status\",\"type\":\"string\"}," +
            "\"Name\":{\"id\":\"/Name\",\"type\":\"string\"}," +
            "\"Symbol\":{\"id\":\"/Symbol\",\"type\":\"string\"}," +
            "\"LastPrice\":{\"id\":\"/LastPrice\",\"type\":\"number\"}," +
            "\"Change\":{\"id\":\"/Change\",\"type\":\"number\"}," +
            "\"ChangePercent\":{\"id\":\"/ChangePercent\",\"type\":\"number\"}," +
            "\"Timestamp\":{\"id\":\"/Timestamp\",\"type\":\"string\"}," +
            "\"MSDate\":{\"id\":\"/MSDate\",\"type\":\"number\"}," +
            "\"MarketCap\":{\"id\":\"/MarketCap\",\"type\":\"integer\"}," +
            "\"Volume\":{\"id\":\"/Volume\",\"type\":\"integer\"}," +
            "\"ChangeYTD\":{\"id\":\"/ChangeYTD\",\"type\":\"number\"}," +
            "\"ChangePercentYTD\":{\"id\":\"/ChangePercentYTD\",\"type\":\"number\"}," +
            "\"High\":{\"id\":\"/High\",\"type\":\"number\"}," +
            "\"Low\":{\"id\":\"/Low\",\"type\":\"number\"}," +
            "\"Open\":{\"id\":\"/Open\",\"type\":\"number\"}}}";

        [TestMethod]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (IStockMarketApi), typeof (StockMarketApi));
        }

        [TestMethod]
        public void TestSingleton()
        {
            AssertPrivateContructor(typeof (StockMarketApi));
            Assert.AreSame(StockMarketApi.Singleton, StockMarketApi.Singleton);
        }

        [TestMethod]
        public void TestCompanyLookup()
        {
            var schema = JsonSchema.Parse(JsonSchemaLookup);

            var stockMarketApi = StockMarketApi.Singleton;

            var actualLookupApple = stockMarketApi.CompanyLookup("AAPL");
            var parsedLookupApple = JArray.Parse(actualLookupApple);

            Assert.IsTrue(parsedLookupApple.IsValid(schema));

            var actualLookupNetflix = stockMarketApi.CompanyLookup("NFLX");
            var parsedLookupNetflix = JArray.Parse(actualLookupNetflix);

            Assert.IsTrue(parsedLookupNetflix.IsValid(schema));
        }

        [TestMethod]
        public void TestStockQuote()
        {
            var schema = JsonSchema.Parse(JsonSchemaQuote);

            var stockMarketApi = StockMarketApi.Singleton;

            var actualQuoteApple = stockMarketApi.QuoteLookup("AAPL");
            var parsedQuoteApple = JObject.Parse(actualQuoteApple);

            Assert.IsTrue(parsedQuoteApple.IsValid(schema));

            var actualQuoteNetflix = stockMarketApi.QuoteLookup("NFLX");
            var parsedQuoteNetflix = JObject.Parse(actualQuoteNetflix);

            Assert.IsTrue(parsedQuoteNetflix.IsValid(schema));
        }

        [TestMethod]
        public void TestStockRange()
        {
            var stockMarketApi = StockMarketApi.Singleton;
            Assert.AreEqual("", stockMarketApi.RangeLookup(""));
        }
    }
}