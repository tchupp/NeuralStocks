namespace NeuralStocks.DatabaseLayer.ApiCommunication
{
    public interface IStockMarketApi
    {
        string CompanyLookup(string company);
        string QuoteLookup(string company);
        string RangeLookup(string parameters);
    }
}