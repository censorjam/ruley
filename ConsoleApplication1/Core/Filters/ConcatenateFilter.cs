using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public class ConcatFilter : InlineFilter
    {
        [JsonProperty(Required = Required.Always)]
        List<string> Fields { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Destination { get; set; }

        public override ExpandoObject Apply(ExpandoObject msg)
        {
            string value = string.Empty;
            foreach (var field in Fields)
            {
                value += msg.GetValue(Get<string>(field)).ToString();
            }
            msg.SetValue(Get<string>(Destination), value);
            return msg;
        }
    }
}