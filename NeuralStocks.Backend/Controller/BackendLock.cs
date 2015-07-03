using System.Net;
using System.Net.Sockets;

namespace NeuralStocks.Backend.Controller
{
    public class BackendLock : IBackendLock
    {
        public TcpListener WrappedListener { get; private set; }
        public int Port { get; private set; }

        public BackendLock(int port)
        {
            Port = port;
            WrappedListener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
        }

        public bool Lock()
        {
            var locked = true;
            try
            {
                WrappedListener.Start();
            }
            catch (SocketException)
            {
                locked = false;
            }
            return locked;
        }

        public void Unlock()
        {
            WrappedListener.Stop();
        }
    }
}