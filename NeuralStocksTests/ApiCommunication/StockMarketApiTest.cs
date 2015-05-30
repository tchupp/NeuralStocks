using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeuralStocks.ApiCommunication;
using NeuralStocksTests.Testing;

namespace NeuralStocksTests.ApiCommunication
{
    [TestClass]
    public class StockMarketApiTest
    {
        [TestMethod]
        public void TestImplementsInterface()
        {
            MoreAssert.ImplementsInterface(typeof (IStockMarketApi), typeof (StockMarketApi));
        }

        [TestMethod]
        public void TestCompanyLookup()
        {
            var stockMarketApi = new StockMarketApi();

            var expectedLookupNetflix = GetUrlResponse("http://dev.markitondemand.com/Api/v2/Lookup/jsonp?input=NFLX");
            var actualLookupNetflix = stockMarketApi.CompanyLookup("NFLX");

            Assert.AreEqual(expectedLookupNetflix, actualLookupNetflix);

            var expectedLookupApple = GetUrlResponse("http://dev.markitondemand.com/Api/v2/Lookup/jsonp?input=AAPL");
            var actualLookupApple = stockMarketApi.CompanyLookup("AAPL");

            Assert.AreEqual(expectedLookupApple, actualLookupApple);
        }

        [TestMethod]
        public void TestStockQuote()
        {
            var stockMarketApi = new StockMarketApi();

            var expectedQuoteNetflix = GetUrlResponse("http://dev.markitondemand.com/Api/v2/Quote/jsonp?symbol=NFLX");
            var actualQuoteNetflix = stockMarketApi.StockQuote("NFLX");

            Assert.AreEqual(expectedQuoteNetflix, actualQuoteNetflix);

            var expectedQuoteApple = GetUrlResponse("http://dev.markitondemand.com/Api/v2/Quote/jsonp?symbol=AAPL");
            var actualQuoteApple = stockMarketApi.StockQuote("AAPL");

            Assert.AreEqual(expectedQuoteApple, actualQuoteApple);
        }

        [TestMethod]
        public void TestStockRange()
        {
            Assert.IsTrue(true, "Unimplemented");
        }

        private static string GetUrlResponse(string url)
        {
            WebResponse response = null;
            var streamReader = StreamReader.Null;
            try
            {
                var request = WebRequest.Create(url);

                Thread.Sleep(100);
                response = request.GetResponse();
                var responseStream = response.GetResponseStream();

                if (responseStream == null)
                {
                    Assert.Fail("Response Stream was null");
                    return "";
                }

                streamReader = new StreamReader(responseStream, Encoding.UTF8);

                var read = streamReader.ReadToEnd();


                read = read.Remove(0, 19);
                read = read.Remove(read.Length - 2, 2);

                return read;
            }
            catch (WebException ex)
            {
                Assert.Fail(ex.StackTrace + ". " + ex.Message);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
                streamReader.Close();
            }

            return "";
        }
    }
}