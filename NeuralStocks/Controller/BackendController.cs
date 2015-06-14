using System.Data.SQLite;
using System.Linq;
using NeuralStocks.ApiCommunication;
using NeuralStocks.SqlDatabase;

namespace NeuralStocks.Controller
{
    public class BackendController : IBackendController
    {
        public IStockMarketApiCommunicator Communicator { get; private set; }
        public ISqlDatabaseCommandRunner CommandRunner { get; private set; }
        public string DatabaseFileName { get; private set; }
        public IBackendTimer BackendTimer { get; set; }

        public BackendController(IStockMarketApiCommunicator communicator, ISqlDatabaseCommandRunner commandRunner,
            string databaseFileName)
        {
            Communicator = communicator;
            CommandRunner = commandRunner;
            DatabaseFileName = databaseFileName;
            BackendTimer = new BackendTimer(this);
        }

        public void UpdateCompanyQuotes()
        {
            var databaseConnectionString = "Data Source=" + DatabaseFileName + ";Version=3;";
            var connection = new SQLiteConnection(databaseConnectionString);
            var lookupsFromTable = CommandRunner.GetQuoteLookupsFromTable(connection);

            foreach (var lookupResponse in lookupsFromTable.Select(
                lookupRequest => Communicator.QuoteLookup(lookupRequest)))
            {
                CommandRunner.UpdateCompanyTimestamp(connection, lookupResponse);
                CommandRunner.AddQuoteResponseToTable(connection, lookupResponse);
            }
        }

        public void StartTimer()
        {
            BackendTimer.Start();
        }

        public void Dispose()
        {
            BackendTimer.Stop();
        }
    }
}