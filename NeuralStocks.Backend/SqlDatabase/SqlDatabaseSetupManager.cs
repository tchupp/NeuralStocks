using System.Data.SQLite;

namespace NeuralStocks.Backend.SqlDatabase
{
    public class SqlDatabaseSetupManager : ISqlDatabaseSetupManager
    {
        private readonly ISqlDatabaseCommandRunner _commandRunner;

        public ISqlDatabaseCommandRunner CommandRunner
        {
            get { return _commandRunner; }
        }

        public SqlDatabaseSetupManager(ISqlDatabaseCommandRunner commandRunner)
        {
            _commandRunner = commandRunner;
        }

        public void InitializeDatabase(string databaseFileName)
        {
            var databaseConnectionString = "Data Source=" + databaseFileName + ";Version=3;";
            var connection = new SQLiteConnection(databaseConnectionString);

            _commandRunner.CreateDatabase(databaseFileName);
            _commandRunner.CreateCompanyTable(connection);
        }
    }
}