namespace NeuralStocks.ApiCommunication
{
    public class QuoteLookupRequest
    {
        public string Company { get; private set; }

        public QuoteLookupRequest(string company)
        {
            Company = company;
        }
    }
}