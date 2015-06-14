namespace NeuralStocks.Controller
{
    public interface IBackendController
    {
        void UpdateCompanyQuotes();
        void StartTimer();
        void Dispose();
    }
}