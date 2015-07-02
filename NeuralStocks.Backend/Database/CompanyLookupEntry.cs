namespace NeuralStocks.Frontend.Database
{
    public class CompanyLookupEntry
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string FirstDate { get; set; }
        public string RecentDate { get; set; }
        public bool Collection { get; set; }
    }
}