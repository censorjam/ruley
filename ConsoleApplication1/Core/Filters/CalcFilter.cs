using System;
using System.Dynamic;
using NCalc;

namespace Ruley.Core.Filters
{
    public class CalcFilter : InlineFilter
    {
        public string Expression { get; set; }
        public string Destination { get; set; }

        public override ExpandoObject Do(ExpandoObject msg)
        {
            try
            {
                var expression = Get<string>(Expression);
                Logger.Debug("Expression is {0}", expression);
                var result = new Expression(expression).Evaluate();
                Logger.Debug("Result is {0}", result);
                msg.SetValue(Get<string>(Destination), result);
            }
            catch(Exception e)
            {
                Logger.Error(e);
            }

            return msg;
        }
    }
}
