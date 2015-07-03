using NeuralStocks.DatabaseLayer.Model.StockApi;

namespace NeuralStocks.DatabaseLayer.StockApi
{
    public interface ITimestampParser
    {
        QuoteLookupResponse Parse(QuoteLookupResponse response);
    }
}