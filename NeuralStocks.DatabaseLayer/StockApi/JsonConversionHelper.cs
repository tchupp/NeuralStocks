using Newtonsoft.Json;

namespace NeuralStocks.DatabaseLayer.StockApi
{
    public class JsonConversionHelper : IJsonConversionHelper
    {
        public static readonly JsonConversionHelper Singleton = new JsonConversionHelper();

        private JsonConversionHelper()
        {
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string objString)
        {
            return JsonConvert.DeserializeObject<T>(objString);
        }
    }
}