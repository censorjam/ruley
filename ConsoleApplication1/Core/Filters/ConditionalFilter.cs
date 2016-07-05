using System.Dynamic;

namespace Ruley.Core.Filters
{
    public class ConditionalFilter : ConditionalBaseFilter
    {
        public Property<string> Destination { get; set; }

        public override ExpandoObject Apply(ExpandoObject msg)
        {
            var match = RunMatch(msg);
            msg.SetValue(Destination.Get(msg), match);
            return msg;
        }
    }
}