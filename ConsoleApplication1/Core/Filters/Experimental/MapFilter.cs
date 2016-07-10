using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Ruley.Dynamic;

namespace Ruley.Core.Filters
{
    public class Mapping
    {
        public object In { get; set; }
        public DynamicDictionary Out { get; set; }
    }

    public class MapFilter : InlineFilter
    {
        [JsonProperty(Required = Required.Always)]
        public Property<string> Field { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Property<string> Destination { get; set; }

        [JsonProperty(Required = Required.Always)]
        public List<object[]> Mapping { get; set; }

        public List<Mapping> Map { get; set; }

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
                var value = msg.Data.GetValue(Field.Get(msg));
                var valueString = value == null ? "null" :value.ToString();

                if (valueString == s)
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
