using System.Dynamic;
using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public class CountFilter : InlineFilter
    {
        private long _count;
        
        [JsonProperty(Required = Required.Always)]
        public Property<string> Destination { get; set; }

        public override Event Apply(Event msg)
        {
            _count++;
            var destination = Destination.Get(msg);
            msg.Data.SetValue(destination, _count);
            return msg;
        }
    }
}
