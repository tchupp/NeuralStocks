using System;
using System.IO;

namespace NeuralStocks.DatabaseLayer.Sqlite
{
    public static class DatabaseConfiguration
    {
        public static string FullDatabaseFileName { get; private set; }

        static DatabaseConfiguration()
        {
            const string fileName = "NeuralStocksDatabase.sqlite";
            var solutionDirectory =
                Path.GetDirectoryName(Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory))));

            FullDatabaseFileName = solutionDirectory + "\\Database\\" + fileName;
        }
    }
}