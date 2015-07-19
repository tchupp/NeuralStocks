using NeuralStocks.DatabaseLayer.StockApi;
using NeuralStocks.DatabaseLayer.Tests.Testing;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace NeuralStocks.DatabaseLayer.Tests.StockApi
{
    [TestFixture]
    public class TimestampParserTest : AssertTestClass
    {
        [Test]
        [Category("StockApi")]
        public void TestImplementsInterface()
        {
            AssertImplementsInterface(typeof (ITimestampParser), typeof (TimestampParser));
        }

        [Test]
        [Category("StockApi")]
        public void TestParseCorrectlyParsesTimestampToDateTimeFormat_EachMonth()
        {
            const string initialTimestampJan = "Tue Jan 16 11:49:10 UTC-04:00 2015";
            const string initialTimestampFeb = "Tue Feb 16 11:49:10 UTC-04:00 2015";
            const string initialTimestampMar = "Tue Mar 16 11:49:10 UTC-04:00 2015";
            const string initialTimestampApr = "Tue Apr 16 11:49:10 UTC-04:00 2015";
            const string initialTimestampMay = "Tue May 16 11:49:10 UTC-04:00 2015";
            const string initialTimestampJun = "Tue Jun 16 11:49:10 UTC-04:00 2015";
            const string initialTimestampJul = "Tue Jul 16 11:49:10 UTC-04:00 2015";
            const string initialTimestampAug = "Tue Aug 16 11:49:10 UTC-04:00 2015";
            const string initialTimestampSep = "Tue Sep 16 11:49:10 UTC-04:00 2015";
            const string initialTimestampOct = "Tue Oct 16 11:49:10 UTC-04:00 2015";
            const string initialTimestampNov = "Tue Nov 16 11:49:10 UTC-04:00 2015";
            const string initialTimestampDec = "Tue Dec 16 11:49:10 UTC-04:00 2015";
            const string expecetedTimestampJan = "D20150116T11:49:10";
            const string expecetedTimestampFeb = "D20150216T11:49:10";
            const string expecetedTimestampMar = "D20150316T11:49:10";
            const string expecetedTimestampApr = "D20150416T11:49:10";
            const string expecetedTimestampMay = "D20150516T11:49:10";
            const string expecetedTimestampJun = "D20150616T11:49:10";
            const string expecetedTimestampJul = "D20150716T11:49:10";
            const string expecetedTimestampAug = "D20150816T11:49:10";
            const string expecetedTimestampSep = "D20150916T11:49:10";
            const string expecetedTimestampOct = "D20151016T11:49:10";
            const string expecetedTimestampNov = "D20151116T11:49:10";
            const string expecetedTimestampDec = "D20151216T11:49:10";

            var quoteLookupResponseJan = new QuoteLookupResponse {Timestamp = initialTimestampJan};
            var quoteLookupResponseFeb = new QuoteLookupResponse {Timestamp = initialTimestampFeb};
            var quoteLookupResponseMar = new QuoteLookupResponse {Timestamp = initialTimestampMar};
            var quoteLookupResponseApr = new QuoteLookupResponse {Timestamp = initialTimestampApr};
            var quoteLookupResponseMay = new QuoteLookupResponse {Timestamp = initialTimestampMay};
            var quoteLookupResponseJun = new QuoteLookupResponse {Timestamp = initialTimestampJun};
            var quoteLookupResponseJul = new QuoteLookupResponse {Timestamp = initialTimestampJul};
            var quoteLookupResponseAug = new QuoteLookupResponse {Timestamp = initialTimestampAug};
            var quoteLookupResponseSep = new QuoteLookupResponse {Timestamp = initialTimestampSep};
            var quoteLookupResponseOct = new QuoteLookupResponse {Timestamp = initialTimestampOct};
            var quoteLookupResponseNov = new QuoteLookupResponse {Timestamp = initialTimestampNov};
            var quoteLookupResponseDec = new QuoteLookupResponse {Timestamp = initialTimestampDec};

            var parser = TimestampParser.Singleton;
            parser.Parse(quoteLookupResponseJan);
            parser.Parse(quoteLookupResponseFeb);
            parser.Parse(quoteLookupResponseMar);
            parser.Parse(quoteLookupResponseApr);
            parser.Parse(quoteLookupResponseMay);
            parser.Parse(quoteLookupResponseJun);
            parser.Parse(quoteLookupResponseJul);
            parser.Parse(quoteLookupResponseAug);
            parser.Parse(quoteLookupResponseSep);
            parser.Parse(quoteLookupResponseOct);
            parser.Parse(quoteLookupResponseNov);
            parser.Parse(quoteLookupResponseDec);

            Assert.AreEqual(expecetedTimestampJan, quoteLookupResponseJan.Timestamp);
            Assert.AreEqual(expecetedTimestampFeb, quoteLookupResponseFeb.Timestamp);
            Assert.AreEqual(expecetedTimestampMar, quoteLookupResponseMar.Timestamp);
            Assert.AreEqual(expecetedTimestampApr, quoteLookupResponseApr.Timestamp);
            Assert.AreEqual(expecetedTimestampMay, quoteLookupResponseMay.Timestamp);
            Assert.AreEqual(expecetedTimestampJun, quoteLookupResponseJun.Timestamp);
            Assert.AreEqual(expecetedTimestampJul, quoteLookupResponseJul.Timestamp);
            Assert.AreEqual(expecetedTimestampAug, quoteLookupResponseAug.Timestamp);
            Assert.AreEqual(expecetedTimestampSep, quoteLookupResponseSep.Timestamp);
            Assert.AreEqual(expecetedTimestampOct, quoteLookupResponseOct.Timestamp);
            Assert.AreEqual(expecetedTimestampNov, quoteLookupResponseNov.Timestamp);
            Assert.AreEqual(expecetedTimestampDec, quoteLookupResponseDec.Timestamp);
        }

        [Test]
        [Category("StockApi")]
        public void TestParseCorrectlyParsesTimestampToDateTimeFormat_SameQuoteLookupObject()
        {
            const string initialTimestamp = "Tue Jun 16 11:49:10 UTC-04:00 2015";
            const string expecetedTimestamp = "D20150616T11:49:10";

            var quoteLookupResponse = new QuoteLookupResponse {Timestamp = initialTimestamp};

            var parser = TimestampParser.Singleton;
            var newResponse = parser.Parse(quoteLookupResponse);

            Assert.AreEqual(expecetedTimestamp, newResponse.Timestamp);
        }

        [Test]
        [Category("StockApi")]
        public void TestSingleton()
        {
            AssertPrivateContructor(typeof (TimestampParser));
            Assert.AreSame(TimestampParser.Singleton, TimestampParser.Singleton);
        }
    }
}