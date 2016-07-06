using System;
using System.Dynamic;
using Newtonsoft.Json;

namespace Ruley.Core.Outputs
{
    public class ConsoleOutput : Output
    {
        public Property<string> Template { get; set; }

        public override void Do(Event x)
        {
            if (Template != null)
            {
                Console.WriteLine(Templater.ApplyTemplate(Template.Get(x), x));
            }
            else
            {
                Console.WriteLine(JsonConvert.SerializeObject(x, Formatting.Indented));
            }
        }
    }
}