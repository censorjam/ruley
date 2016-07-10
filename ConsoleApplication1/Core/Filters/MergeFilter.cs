using Newtonsoft.Json;
using Ruley.Dynamic;

namespace Ruley.Core.Filters
{
    public class MergeFilter : InlineFilter
    {
        [JsonRequired]
        public DynamicDictionary Data { get; set; }
        
        public override Event Apply(Event e)
        {
            foreach (var v in Data)
            {
                var str = v.Value as string;
                if (str != null)
                {
                    e.Data[v.Key] = Templater.ApplyTemplate(str, e.Data);
                }
                else
                {
                    e.Data[v.Key] = v.Value;
                }
            }
            return e;
        }
    }
}
