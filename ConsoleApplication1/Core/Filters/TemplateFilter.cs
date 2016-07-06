using System;
using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public class TemplateFilter : InlineFilter
    {
        [JsonProperty(Required = Required.Always)]
        public Property<string> Template { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Destination { get; set; }

        public override Event Apply(Event msg)
        {
            msg.Data.SetValue(Destination, Templater.ApplyTemplate(Template.Get(msg), msg));
            return msg;
        }
    }
}