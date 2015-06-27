using System.Collections.Generic;
using System.Data;
using NeuralStocks.Backend.ApiCommunication;

namespace NeuralStocks.Frontend.Controller
{
    public class DataTableFactory : IDataTableFactory
    {
        private const string NewCompanySearchTableName = "NewCompanySearchTable";
        public static readonly IDataTableFactory Factory = new DataTableFactory();

        private DataTableFactory()
        {
        }

        public DataTable BuildCompanySearchTable(List<CompanyLookupResponse> lookupResponseList)
        {
            var companySearchTable = new DataTable(NewCompanySearchTableName);
            companySearchTable.Columns.Add("Name");
            companySearchTable.Columns.Add("Symbol");
            companySearchTable.Columns.Add("Exchange");

            foreach (var lookupResponse in lookupResponseList)
            {
                var name = lookupResponse.Name;
                var symbol = lookupResponse.Symbol;
                var exchange = lookupResponse.Exchange;

                companySearchTable.Rows.Add(name, symbol, exchange);
            }
            return companySearchTable;
        }
    }
}