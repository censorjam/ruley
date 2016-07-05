using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Ruley.Core.Filters
{
    public abstract class ConditionalBaseFilter : InlineFilter
    {
        public Property<string> Field { get; set; }
        public Property<double> Level { get; set; }
        public Property<string> Match { get; set; }

        protected bool RunMatch(ExpandoObject m)
        {
            try
            {
                IDictionary<string, object> e = m;
                
                Console.WriteLine("casting value");
                var fieldName = Field.Get(m);
                var f = m.GetValue(fieldName);

                var value = Convert.ToDouble(f);

                Console.WriteLine("getting level");
                var level = Level.Get(m);

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
                m.AddError(e.Message);
                Console.WriteLine(e);
                return true;
            }
            return true;
        }
    }
}