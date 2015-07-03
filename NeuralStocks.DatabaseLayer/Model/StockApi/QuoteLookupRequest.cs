namespace NeuralStocks.DatabaseLayer.Model.StockApi
{
    public class QuoteLookupRequest
    {
        public string Company { get; set; }
        public string Timestamp { get; set; }
    }
}