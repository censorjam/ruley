using System.Dynamic;

namespace Ruley.Core.Filters
{
    public class PassThroughFilter : InlineFilter
    {
        public override ExpandoObject Do(ExpandoObject msg)
        {
            return msg;
        }
    }
}