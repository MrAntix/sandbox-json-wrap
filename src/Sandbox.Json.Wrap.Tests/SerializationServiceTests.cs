using Newtonsoft.Json;
using Xunit;

namespace Sandbox.Json.Wrap.Tests
{
    public class SerializationServiceTests
    {
        readonly Item[] items = {new Item {Name = "item1"}, new Item {Name = "item2"}};

        static ISerializationService GetService()
        {
            return new SerializationService();
        }

        [Fact]
        public void wraps()
        {
            var service = GetService();

            var expected = JsonConvert
                .SerializeObject(new
                {
                    items = new
                    {
                        item = new[] {new {name = "item1"}, new {name = "item2"}}
                    }
                });

            var actual = service
                .Serialize(new Parent {Items = items});

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void unwraps()
        {
            var service = GetService();

            var json = JsonConvert
                .SerializeObject(new
                {
                    items = new
                    {
                        item = items
                    }
                });

            var result = service.Deserialize<Parent>(json);

            Assert.Equal(2, result.Items.Length);
        }

        class Parent
        {
            [SerializationWrap("item")]
            public Item[] Items { get; set; }
        }

        class Item
        {
            public string Name { get; set; }
        }
    }
}