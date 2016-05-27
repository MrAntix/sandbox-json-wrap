using Newtonsoft.Json;

namespace Sandbox.Json.Wrap
{
    public class SerializationService :
        ISerializationService
    {
        public static readonly JsonSerializerSettings SerializerSettings
            = new JsonSerializerSettings
            {
                ContractResolver = new SerializationContractResolver()
            };

        public static readonly JsonSerializer Serializer
            = new JsonSerializer
            {
                ContractResolver = SerializerSettings.ContractResolver
            };

        string ISerializationService.Serialize<T>(
            T data)
        {
            return JsonConvert.SerializeObject(data, SerializerSettings);
        }

        T ISerializationService.Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, SerializerSettings);
        }
    }
}