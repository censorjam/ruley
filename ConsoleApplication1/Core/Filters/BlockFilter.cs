using System.Dynamic;

namespace Ruley.Core.Filters
{
    public class BlockFilter : InlineFilter
    {
        public override ExpandoObject Apply(ExpandoObject msg)
        {
            return null;
        }
    }
}