namespace NeuralStocks.DatabaseLayer.StockApi
{
    public interface IJsonConversionHelper
    {
        string Serialize(object obj);
        T Deserialize<T>(string objString);
    }
}