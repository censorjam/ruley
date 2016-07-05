using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public class ConcatFilter : InlineFilter
    {
        [JsonProperty(Required = Required.Always)]
        List<Property<string>> Fields { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Property<string> Destination { get; set; }

        public override ExpandoObject Apply(ExpandoObject msg)
        {
            string value = string.Empty;
            foreach (var field in Fields)
            {
                value += msg.GetValue(field.Get(msg)).ToString();
            }
            msg.SetValue(Destination.Get(msg), value);
            return msg;
        }
    }
}