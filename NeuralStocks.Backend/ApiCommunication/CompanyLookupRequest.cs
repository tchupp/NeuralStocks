namespace NeuralStocks.Backend.ApiCommunication
{
    public class CompanyLookupRequest
    {
        public string Company { get; private set; }

        public CompanyLookupRequest(string company)
        {
            Company = company;
        }
    }
}