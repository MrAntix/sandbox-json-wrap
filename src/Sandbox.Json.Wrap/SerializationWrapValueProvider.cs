using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Sandbox.Json.Wrap
{
    public class SerializationWrapValueProvider :
        IValueProvider
    {
        readonly PropertyInfo _member;
        readonly string _name;

        public SerializationWrapValueProvider(PropertyInfo member, string name)
        {
            _member = member;
            _name = name;
        }

        public void SetValue(object target, object value)
        {
            var jToken = value as JToken;
            if (jToken == null) return;

            _member.SetValue(target, jToken[_name].ToObject(_member.PropertyType));
        }

        public object GetValue(object target)
        {
            var value = _member.GetValue(target);
            if (value == null) return null;

            var wrapper = new JObject
            {
                {
                    _name, JToken.FromObject(
                        value,
                        SerializationService.Serializer)
                }
            };

            return wrapper;
        }
    }
}