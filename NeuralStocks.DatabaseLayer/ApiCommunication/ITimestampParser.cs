namespace NeuralStocks.DatabaseLayer.ApiCommunication
{
    public interface ITimestampParser
    {
        QuoteLookupResponse Parse(QuoteLookupResponse response);
    }
}