using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ruley.Core.Filters
{
    public class PropertyConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new Exception("not written");
        }

        private Property<T> Create<T>(object value)
        {
            return new Property<T>(value);
        } 

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            MethodInfo method = typeof(PropertyConverter).GetMethod("Create", BindingFlags.Instance | BindingFlags.NonPublic);
            MethodInfo generic = method.MakeGenericMethod(objectType.GenericTypeArguments[0]);

            var returnValue = generic.Invoke(this, new object[] { reader.Value });
            return returnValue;
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsGenericType)
            {
                return objectType.GetGenericTypeDefinition() == typeof(Property<>);
            }
            return false;
        }
    }


    public class MapFilter : InlineFilter
    {
        [JsonProperty(Required = Required.Always)]
        public Property<string> Field { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Property<string> Destination { get; set; }

        [JsonProperty(Required = Required.Always)]
        public List<object[]> Mapping { get; set; }

        public Property<object> Default { get; set; }

        public object Test { get; set; }

        public override void ValidateComposition()
        {
        }

        public override Event Apply(Event msg)
        {
            foreach (var mapping in Mapping)
            {
                var s = mapping[0] == null ? "null" : mapping[0].ToString();

                if (msg.Data.GetValue(Field.Get(msg)).ToString() == s)
                {
                    msg.Data.SetValue(Destination.Get(msg), mapping[1]);
                    return msg;
                }
            }

            if (Default == null)
                throw new Exception("No match and no default value set");

            msg.Data.SetValue(Destination.Get(msg), Default.Get(msg));
            return msg;
        }
    }

    public class TestFilter : InlineFilter
    {
        public Property<string> Default { get; set; }

        public override Event Apply(Event msg)
        {
            return msg;
        }
    }
}
