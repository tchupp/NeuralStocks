namespace NeuralStocks.DatabaseLayer.StockApi
{
    public interface IStockMarketApi
    {
        string CompanyLookup(string company);
        string QuoteLookup(string company);
        string RangeLookup(string parameters);
    }
}