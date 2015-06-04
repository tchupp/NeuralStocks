using System.Data.SQLite;
using NeuralStocks.ApiCommunication;

namespace NeuralStocks.SqlDatabase
{
    public interface ISqlDatabaseCommandRunner
    {
        void CreateCompanyTable(SQLiteConnection connection);
        void AddCompanyToTable(CompanyLookupResponse company, SQLiteConnection connection);
    }
}