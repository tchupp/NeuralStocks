namespace NeuralStocks.DatabaseLayer.Sqlite
{
    public interface IDatabaseConnection
    {
        void Open();
        void Close();
        IDatabaseCommand CreateCommand(string commandString);
    }
}