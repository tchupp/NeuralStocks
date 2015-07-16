namespace NeuralStocks.DatabaseLayer.StockApi
{
    public class CompanyLookupResponse
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
    }
}