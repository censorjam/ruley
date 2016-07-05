using System.Dynamic;
using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public class PrevFilter : InlineFilter
    {
        [JsonProperty(Required = Required.Always)]
        public string Destination { get; set; }

        private ExpandoObject _prev;

        public override ExpandoObject Apply(ExpandoObject msg)
        {
            msg.SetValue(Destination, _prev);
            _prev = msg.Clone();
            _prev.DeleteValue(Destination);
            return msg;
        }
    }
}