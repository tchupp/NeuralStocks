using NeuralStocks.DatabaseLayer.Model.StockApi;

namespace NeuralStocks.DatabaseLayer.Communicator.StockApi
{
    public interface ITimestampParser
    {
        QuoteLookupResponse Parse(QuoteLookupResponse response);
    }
}