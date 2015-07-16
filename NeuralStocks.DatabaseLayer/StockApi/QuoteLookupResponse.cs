namespace NeuralStocks.DatabaseLayer.StockApi
{
    public class QuoteLookupResponse
    {
        public string Status { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double LastPrice { get; set; }
        public double Change { get; set; }
        public double ChangePercent { get; set; }
        public string Timestamp { get; set; }
        public double MarketCap { get; set; }
        public double Volume { get; set; }
        public double ChangeYtd { get; set; }
        public double ChangePercentYtd { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
    }
}