using System.Data.SQLite;

namespace NeuralStocks.SqlDatabase
{
    public class SqlDatabaseSetupSetupManager : ISqlDatabaseSetupManager
    {
        private readonly ISqlDatabaseCommandRunner _commandRunner;

        public ISqlDatabaseCommandRunner CommandRunner
        {
            get { return _commandRunner; }
        }

        public SqlDatabaseSetupSetupManager(ISqlDatabaseCommandRunner commandRunner)
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