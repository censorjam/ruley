using System;
using System.Dynamic;
using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public class TimestampFilter : InlineFilter
    {
        [JsonProperty(Required = Required.Always)]
        public string Destination { get; set; }

        public override Event Apply(Event msg)
        {
            msg.Data.SetValue(Destination, DateTime.Now);
            return msg;
        }
    }
}
