using System.Dynamic;

namespace Ruley.Core.Filters
{
    public class PassThroughFilter : InlineFilter
    {
        public override ExpandoObject Apply(ExpandoObject msg)
        {
            return msg;
        }
    }
}