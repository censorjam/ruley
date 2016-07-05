using System.Dynamic;
using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public class CountFilter : InlineFilter
    {
        private long _count;
        
        [JsonProperty(Required = Required.Always)]
        public Property<string> Destination { get; set; }

        public override ExpandoObject Apply(ExpandoObject msg)
        {
            _count++;
            var destination = Destination.Get(msg);
            msg.SetValue(destination, _count);
            return msg;
        }
    }
}
