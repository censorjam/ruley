using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ruley.Core.Filters
{
    public class MapFilter : InlineFilter
    {
        [JsonProperty(Required = Required.Always)]
        public Property<string> Field { get; set; }

        public Property<bool> Replace { get; set; }

        [JsonProperty(Required = Required.Always)]
        public List<ExpandoObject> Mapping { get; set; }

        public override void ValidateComposition()
        {
        }

        public override Event Apply(Event msg)
        {
            //var replace = Replace.Get(msg);
            //var next = replace ? new ExpandoObject() : msg.Data;

            //foreach (var mapping in Mapping)
            //{
            //    var s = mapping[0].ToString();
            //    if (msg.Data.GetValue(Field.Get(msg)).ToString() == s)
            //    {
            //        foreach (var property in ((JObject)mapping[1]).Properties())
            //        {
            //            next.SetValue(property.Name, ((JObject)mapping[1]).GetValue(property.Name));
            //        }
            //    }
            //}

            //msg.Data = next;
            ////msg.Data.SetValue(Destination.Get(msg), Default.Get(msg));
            return msg;
        }
    }
}
