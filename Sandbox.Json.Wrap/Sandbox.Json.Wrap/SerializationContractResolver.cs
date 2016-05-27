using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Sandbox.Json.Wrap
{
    public class SerializationContractResolver :
        CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(
            MemberInfo member, MemberSerialization memberSerialization)
        {
            var jsonProperty = base.CreateProperty(member, memberSerialization);
            var property = member as PropertyInfo;

            if (property != null)
            {
                var attr = member
                    .GetCustomAttributes(typeof(SerializationWrapAttribute), false)
                    .Cast<SerializationWrapAttribute>()
                    .SingleOrDefault();

                if (attr != null)
                {
                    jsonProperty.PropertyType = typeof(object);
                    jsonProperty.ValueProvider = new SerializationWrapValueProvider(property, attr.Name);
                }
            }

            return jsonProperty;
        }
    }
}