using System.Data.SQLite;
using NeuralStocks.ApiCommunication;

namespace NeuralStocks.SqlDatabase
{
    public class SqlDatabaseCommandRunner : ISqlDatabaseCommandRunner
    {
        public static SqlDatabaseCommandRunner Singleton = new SqlDatabaseCommandRunner();

        private SqlDatabaseCommandRunner()
        {
        }

        public void CreateCompanyTable(SQLiteConnection connection)
        {
            const string createCompanyTableCommandString =
                "CREATE TABLE Company (name TEXT, symbol TEXT, firstDate TEXT, recentDate TEXT)";

            connection.Open();

            var createInitialTableCommand = new SQLiteCommand(createCompanyTableCommandString, connection);
            createInitialTableCommand.ExecuteNonQuery();

            connection.Close();
        }

        public void AddCompanyToTable(CompanyLookupResponse company, SQLiteConnection connection)
        {
            var addCompanyToTableCommandString =
                "INSERT INTO Company VALUES ('" + company.Name + "', '" + company.Symbol + "', 'null', 'null')";

            connection.Open();

            var createInitialTableCommand = new SQLiteCommand(addCompanyToTableCommandString, connection);
            createInitialTableCommand.ExecuteNonQuery();

            connection.Close();
        }
    }
}