using System;
using System.Dynamic;
using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public class TimestampFilter : InlineFilter
    {
        [JsonProperty(Required = Required.Always)]
        public string Destination { get; set; }

        public override ExpandoObject Apply(ExpandoObject msg)
        {
            msg.SetValue(Destination, DateTime.Now);
            return msg;
        }
    }
}
