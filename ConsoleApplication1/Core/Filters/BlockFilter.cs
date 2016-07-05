using System.Dynamic;

namespace Ruley.Core.Filters
{
    public class BlockFilter : InlineFilter
    {
        public override ExpandoObject Do(ExpandoObject msg)
        {
            return null;
        }
    }
}