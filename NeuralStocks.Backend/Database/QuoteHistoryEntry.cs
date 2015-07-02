namespace NeuralStocks.Frontend.Database
{
    public class QuoteHistoryEntry
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Timestamp { get; set; }
        public double LastPrice { get; set; }
        public double Change { get; set; }
        public double ChangePercent { get; set; }

        public QuoteHistoryEntry()
        {
        }
    }
}