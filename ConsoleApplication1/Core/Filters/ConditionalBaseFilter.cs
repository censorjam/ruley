using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public abstract class ConditionalBaseFilter : InlineFilter
    {
        [JsonProperty(Required = Required.Always)]
        public Property<string> Field { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Property<object> Level { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Property<string> Match { get; set; }

        protected bool RunMatch(Event m)
        {
            try
            {
                IDictionary<string, object> e = m.Data;
                
                Console.WriteLine("casting value");
                var fieldName = Field.Get(m);
                var f = m.Data.GetValue(fieldName);

                var value = Convert.ToDouble(f);

                //value = f;

                Console.WriteLine("getting level");
                var level = Convert.ToDouble(Level.Get(m));

                Console.WriteLine("value: {0}, level: {1}, match: {2}", value, level, Match);

                switch (Match.Get(m))
                {
                    case ">":
                        if (!(value > level))
                            return false;
                        break;

                    case ">=":
                        if (!(value >= level))
                            return false;
                        break;

                    case "<=":
                        if (!(value <= level))
                            return false;
                        break;

                    case "<":
                        if (!(value < level))
                            return false;
                        break;

                    case "=":
                        if (!(value == level))
                            return false;
                        break;

                    case "!=":
                        if (!(value != level))
                            return false;
                        break;
                }
            }
            catch (Exception e)
            {
               // m.AddError(e.Message);
                Console.WriteLine(e);
                return true;
            }
            return true;
        }
    }
}