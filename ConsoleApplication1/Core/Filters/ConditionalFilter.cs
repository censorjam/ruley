using System.Dynamic;
using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public class ConditionalFilter : ConditionalBaseFilter
    {
        [JsonProperty(Required = Required.Always)]
        public Property<string> Destination { get; set; }

        public override ExpandoObject Apply(ExpandoObject msg)
        {
            var match = RunMatch(msg);
            msg.SetValue(Destination.Get(msg), match);
            return msg;
        }
    }
}