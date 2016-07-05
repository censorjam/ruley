using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Ruley.Core.Filters
{
    public class ConditionalFilter : InlineFilter
    {
        public string Field { get; set; }
        public Property<double> Level { get; set; }
        public string Match { get; set; }
        public int MatchCount { get; set; }
        private int _continuousMatches;

        public Property<string> Destination { get; set; }

        private readonly string[] _allowableMatches = { ">", "<", ">=", "<=", "=", "!=" };

        public InlineFilter True { get; set; }
        public InlineFilter False { get; set; }

        public override void Validate()
        {
            if (!_allowableMatches.Contains(Match))
                throw new Exception(string.Format("Invalid Match, valid values are {0}", String.Join(", ", _allowableMatches)));
        }

        public override ExpandoObject Do(ExpandoObject x)
        {
            True = True ?? new PassThroughFilter();
            False = False ?? new BlockFilter();

            ExpandoObject next;

            var match = RunMatch(x);

            if (Destination != null)
            {
                x.SetValue(Destination.Get(x), match);
            }

            if (match)
            {
                next = DoTrue(x);
            }
            else
            {
                next = DoFalse(x);
            }

            return next;

            //if (!next)
            //{
            //    _continuousMatches = 0;
            //    return null;
            //}

            //_continuousMatches++;
            //if (_continuousMatches >= MatchCount)
            //{
            //    _continuousMatches = 0;
            //    return x;
            //}
            //return null;
        }

        private ExpandoObject DoFalse(ExpandoObject m)
        {
            Console.WriteLine("Executing false branch value");

          

            return False.Do(m);
        }

        private ExpandoObject DoTrue(ExpandoObject m)
        {
            Console.WriteLine("Executing true branch");
            return True.Do(m);
        }

        private bool RunMatch(ExpandoObject m)
        {
            try
            {
                IDictionary<string, object> e = m;
                
                Console.WriteLine("casting value");
                var value = Convert.ToDouble(m.GetValue(Field));

                Console.WriteLine("getting level");
                var level = Level.Get(m);

                Console.WriteLine("value: {0}, level: {1}, match: {2}", value, level, Match);

                switch (Match)
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
                Console.WriteLine(e);
                return true;
            }
            return true;
        }
    }
}