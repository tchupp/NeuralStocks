namespace NeuralStocks.ApiCommunication
{
    public class CompanyLookupResponse
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }

        public CompanyLookupResponse()
        {
            Symbol = "";
            Name = "";
            Exchange = "";
        }

        public CompanyLookupResponse(string symbol, string name, string exchange)
        {
            Symbol = symbol;
            Name = name;
            Exchange = exchange;
        }
    }
}