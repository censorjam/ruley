using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Ruley.Dynamic;

namespace Ruley.Core.Filters
{
    public class Mapping
    {
        public object Key { get; set; }
        public DynamicDictionary Value { get; set; }
    }

    public class MapFilter : InlineFilter
    {
        [JsonProperty(Required = Required.Always)]
        public Property<string> Value { get; set; }

        public List<Mapping> Map { get; set; }

        public DynamicDictionary Default { get; set; }

        public object Test { get; set; }

        public override void ValidateComposition()
        {
        }

        public override Event Apply(Event msg)
        {
            foreach (var mapping in Map)
            {
                var s = mapping.Key == null ? "null" : mapping.Key.ToString();
                var value = Value.GetValue(msg);
                var valueString = value == null ? "null" :value.ToString();

                if (valueString == s)
                {
                    msg.Data.Merge(mapping.Value);
                    return msg;
                }
            }

            if (Default == null)
                throw new Exception("No match and no default value set");

            msg.Data.Merge(Default);
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
