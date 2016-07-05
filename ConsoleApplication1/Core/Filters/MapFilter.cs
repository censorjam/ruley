using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public class MapFilter : InlineFilter
    {
        [JsonProperty(Required = Required.Always)]
        public Property<string> Field { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Property<string> Destination { get; set; }

        [JsonProperty(Required = Required.Always)]
        public List<object[]> Mapping { get; set; }

        public Property<object> DefaultValue { get; set; }

        public override void ValidateComposition()
        {
        }

        public override ExpandoObject Apply(ExpandoObject msg)
        {
            foreach (var mapping in Mapping)
            {
                var s = mapping[0].ToString();
                if (msg.GetValue(Field.Get(msg)).ToString() == s)
                {
                    msg.SetValue(Destination.Get(msg), mapping[1]);
                    return msg;
                }
            }
            
            if (DefaultValue == null)
                throw new Exception("No match and no default value set");

            msg.SetValue(Destination.Get(msg), DefaultValue.Get(msg));
            return msg;
        }
    }
}
