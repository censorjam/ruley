using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reactive.Linq;
using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public class JsonifyFilter : InlineFilter
    {
        [JsonProperty(Required = Required.Always)]
        public string Source { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Destination { get; set; }

        public override ExpandoObject Do(ExpandoObject msg)
        {
            throw new NotImplementedException();
        }
    }
}
