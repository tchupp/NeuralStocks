namespace NeuralStocks.DatabaseLayer.Sqlite
{
    public class DatabaseName
    {
        public string Name { get; set; }

        public string DatabaseConnectionString
        {
            get { return "Data Source=" + Name + ";Version=3;"; }
        }
    }
}