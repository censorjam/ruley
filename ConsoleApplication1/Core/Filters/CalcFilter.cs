using System;
using System.Dynamic;
using NCalc;

namespace Ruley.Core.Filters
{
    public class CalcFilter : InlineFilter
    {
        public string Expression { get; set; }
        public Property<string> Destination { get; set; }

        public override Event Apply(Event ev)
        {
            try
            {
                var expression = Get<string>(Expression, ev);
                Logger.Debug("Expression is {0}", expression);
                var result = new Expression(expression).Evaluate();
                Logger.Debug("Result is {0}", result);
                ev.Data.SetValue(Destination.Get(ev), result);
            }
            catch(Exception e)
            {
                Logger.Error(e);
            }

            return ev;
        }
    }
}
