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

        public void CreateDatabase(string databaseName)
        {
            SQLiteConnection.CreateFile(databaseName);
        }

        public void CreateCompanyTable(SQLiteConnection connection)
        {
            const string createCompanyTableCommandString =
                "CREATE TABLE Company (name TEXT, symbol TEXT, firstDate TEXT, recentDate TEXT)";

            connection.Open();

            var createCompanyTableCommand = new SQLiteCommand(createCompanyTableCommandString, connection);
            createCompanyTableCommand.ExecuteNonQuery();

            connection.Close();
        }

        public void AddCompanyToTable(CompanyLookupResponse company, SQLiteConnection connection)
        {
            var addCompanyToTableCommandString =
                "INSERT INTO Company VALUES ('" + company.Name + "', '" + company.Symbol + "', 'null', 'null')";

            connection.Open();

            var addCompanyToTableCommand = new SQLiteCommand(addCompanyToTableCommandString, connection);
            addCompanyToTableCommand.ExecuteNonQuery();

            connection.Close();
        }

        public void UpdateCompanyTimestamp(QuoteLookupResponse response, SQLiteConnection connection)
        {
            var updateCompanyRecentDateCommandString = "UPDATE Company SET recentDate = '" + response.Timestamp +
                                                       "' WHERE Symbol = '" + response.Symbol + "'";

            var updateCompanyFirstDateCommandString = "UPDATE Company SET firstDate = '" + response.Timestamp +
                                                      "' WHERE Symbol = '" + response.Symbol +
                                                      "' AND firstDate = 'null'";

            connection.Open();

            var updateCompanyRecentDateCommand = new SQLiteCommand(updateCompanyRecentDateCommandString, connection);
            updateCompanyRecentDateCommand.ExecuteNonQuery();

            var updateCompanyFirstDateCommand = new SQLiteCommand(updateCompanyFirstDateCommandString, connection);
            updateCompanyFirstDateCommand.ExecuteNonQuery();

            connection.Close();
        }
    }
}