namespace NeuralStocks.ApiCommunication
{
    public interface IStockMarketApi
    {
        string CompanyLookup(string company);
        string StockQuote(string company);
        string StockRange(string parameters);
    }
}