using System.Timers;

namespace NeuralStocks.Backend.Controller
{
    public class BackendTimer : IBackendTimer
    {
        private readonly Timer _timer;

        public Timer Timer
        {
            get { return _timer; }
        }

        public IBackendController Controller { get; private set; }

        public double Interval
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }

        public BackendTimer(IBackendController controller)
        {
            Controller = controller;
            _timer = new Timer(60000);
            _timer.Elapsed += (sender, args) => Controller.UpdateCompanyQuotes();
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}