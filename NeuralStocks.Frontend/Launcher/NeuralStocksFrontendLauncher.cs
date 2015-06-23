using System;
using System.Windows.Forms;
using NeuralStocks.Frontend.UI;

namespace NeuralStocks.Frontend.Launcher
{
    public class NeuralStocksFrontendLauncher : INeuralStocksFrontendLauncher
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}