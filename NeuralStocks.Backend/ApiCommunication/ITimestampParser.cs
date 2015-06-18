namespace NeuralStocks.Backend.ApiCommunication
{
    public interface ITimestampParser
    {
        QuoteLookupResponse Parse(QuoteLookupResponse response);
    }
}