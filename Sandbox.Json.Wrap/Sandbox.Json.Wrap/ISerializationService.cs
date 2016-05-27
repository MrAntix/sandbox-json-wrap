
namespace Sandbox.Json.Wrap
{
    public interface ISerializationService
    {
        string Serialize<T>(T model);
        T Deserialize<T>(string json);
    }
}