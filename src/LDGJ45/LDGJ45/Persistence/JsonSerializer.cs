using System.Text;
using Newtonsoft.Json;

namespace LDGJ45.Persistence
{
    public class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public byte[] Serialize(object value)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value, _settings));
        }

        public T Deserialize<T>(byte[] value)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(value), _settings);
        }
    }
}