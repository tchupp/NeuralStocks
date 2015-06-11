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

        public BackendController(IStockMarketApiCommunicator communicator, ISqlDatabaseCommandRunner commandRunner)
        {
            Communicator = communicator;
            CommandRunner = commandRunner;
            DatabaseFileName = "NeuralStocksDatabase.sqlite";
        }

        public void UpdateCompanyQuotes()
        {
            var connection = new SQLiteConnection(DatabaseFileName);
            var lookupsFromTable = CommandRunner.GetQuoteLookupsFromTable(connection);

            foreach (var lookupResponse in lookupsFromTable.Select(
                lookupRequest => Communicator.QuoteLookup(lookupRequest)))
            {
                CommandRunner.UpdateCompanyTimestamp(connection, lookupResponse);
                CommandRunner.AddQuoteResponseToTable(connection, lookupResponse);
            }
        }
    }
}