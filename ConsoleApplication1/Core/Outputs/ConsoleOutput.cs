using System;
using System.Dynamic;
using Newtonsoft.Json;

namespace Ruley.Core.Outputs
{
    public class ConsoleOutput : Output
    {
        public override void Do(Event x)
        {
            Console.WriteLine(JsonConvert.SerializeObject(x, Formatting.Indented));
        }
    }
}