namespace NeuralStocks.Backend.Controller
{
    public interface IBackendLock
    {
        bool Lock();
        void Unlock();
    }
}