using System;
using System.Collections.Generic;
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

        public Property<object> Default { get; set; }

        public override void ValidateComposition()
        {
        }

        public override Event Apply(Event msg)
        {
            foreach (var mapping in Mapping)
            {
                var s = mapping[0].ToString();
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
}
