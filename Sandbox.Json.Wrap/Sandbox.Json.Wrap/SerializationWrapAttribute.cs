using System;

namespace Sandbox.Json.Wrap
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SerializationWrapAttribute : Attribute
    {
        public SerializationWrapAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}