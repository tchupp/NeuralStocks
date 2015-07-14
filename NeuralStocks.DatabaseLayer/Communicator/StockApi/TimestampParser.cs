using NeuralStocks.DatabaseLayer.Model.StockApi;

namespace NeuralStocks.DatabaseLayer.Communicator.StockApi
{
    public class TimestampParser : ITimestampParser
    {
        public static readonly TimestampParser Singleton = new TimestampParser();

        private TimestampParser()
        {
        }

        public QuoteLookupResponse Parse(QuoteLookupResponse response)
        {
            var timestamp = response.Timestamp;
            var split = timestamp.Split(' ');

            var year = split[5];
            var month = MonthParse(split[1]);
            var day = split[2];
            var time = split[3];

            response.Timestamp = string.Format("D{0}{1}{2}T{3}", year, month, day, time);

            return response;
        }

        private static string MonthParse(string month)
        {
            switch (month)
            {
                case "Jan":
                    return "01";
                case "Feb":
                    return "02";
                case "Mar":
                    return "03";
                case "Apr":
                    return "04";
                case "May":
                    return "05";
                case "Jun":
                    return "06";
                case "Jul":
                    return "07";
                case "Aug":
                    return "08";
                case "Sep":
                    return "09";
                case "Oct":
                    return "10";
                case "Nov":
                    return "11";
                case "Dec":
                    return "12";
                default:
                    return "99";
            }
        }
    }
}