namespace NeuralStocks.ApiCommunication
{
    public class QuoteLookupRequest
    {
        public string Company { get; private set; }
        public string Timestamp { get; private set; }

        public QuoteLookupRequest(string company, string timestamp)
        {
            Company = company;
            Timestamp = timestamp;
        }
    }
}