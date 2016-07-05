using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public class MapFilter : InlineFilter
    {
        [JsonProperty(Required = Required.Always)]
        public string Source { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Destination { get; set; }

        [JsonProperty(Required = Required.Always)]
        public List<object[]> Mapping { get; set; }

        //todo add this
        public object MatchCase { get; set; }

        public override void Validate()
        {
            //todo
        }

        public override ExpandoObject Apply(ExpandoObject msg)
        {
            bool matched = false;
            foreach (var mapping in Mapping)
            {
                var s = mapping[0].ToString();
                if (msg.GetValue(Source).ToString() == s)
                {
                    matched = true;
                    msg.SetValue(Destination, mapping[1]);
                    break;
                }
            }

            if (!matched)
            {
                var def = Mapping.FirstOrDefault(m => m[0].ToString() == "$default");

                if (def != null)
                {
                    msg.SetValue(Destination, def[1]);
                }
                else
                {
                    msg.SetValue(Destination, null);
                }
            }

            return msg;
        }
    }
}
