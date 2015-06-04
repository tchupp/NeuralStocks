namespace NeuralStocks.ApiCommunication
{
    public class QuoteLookupResponse
    {
        public string Status { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public float LastPrice { get; set; }
        public float Change { get; set; }
        public float ChangePercent { get; set; }
        public string Timestamp { get; set; }
        public float MarketCap { get; set; }
        public float Volume { get; set; }
        public float ChangeYtd { get; set; }
        public float ChangePercentYtd { get; set; }
        public float High { get; set; }
        public float Low { get; set; }
        public float Open { get; set; }

        public QuoteLookupResponse()
        {
            Status = "";
            Name = "";
            Symbol = "";
            LastPrice = 0f;
            Change = 0f;
            ChangePercent = 0f;
            Timestamp = "";
            MarketCap = 0f;
            Volume = 0f;
            ChangeYtd = 0f;
            ChangePercentYtd = 0f;
            High = 0f;
            Low = 0f;
            Open = 0f;
        }
    }
}