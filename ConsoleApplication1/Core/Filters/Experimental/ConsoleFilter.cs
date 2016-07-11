using System;
using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public class ConsoleFilter : InlineFilter
    {
        public Property<string> Message { get; set; }

        public override Event Apply(Event x)
        {
            if (Message != null)
            {
                Console.WriteLine(Templater.ApplyTemplate(Message.Get(x), x.Data));
            }
            else
            {
                Console.WriteLine(JsonConvert.SerializeObject(x, Formatting.Indented));
            }
            return x;
        }
    }
}