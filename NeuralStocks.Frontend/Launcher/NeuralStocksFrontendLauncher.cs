﻿using System;
using System.Windows.Forms;
using NeuralStocks.Frontend.UI;

namespace NeuralStocks.Frontend.Launcher
{
    public static class NeuralStocksFrontendLauncher
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