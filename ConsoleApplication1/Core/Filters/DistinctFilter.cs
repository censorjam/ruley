using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public class DistinctFilter : InlineFilter
    {
        public Property<string> Value { get; set; }
        public Property<string> Field { get; set; }

        private bool _hadValue;
        private string _prevValue;

        public override Event Apply(Event msg)
        {
            object n;
            if (Field != null)
            {
                var field = Field.Get(msg);
                n = msg.Data[field];
            }
            else
            {
                n = Value.Get(msg);
            }

            if (n != null)
            {
                var next = JsonConvert.SerializeObject(n);
                if (!next.Equals(_prevValue) || !_hadValue)
                {
                    _hadValue = true;
                    _prevValue = next;
                    return msg;
                }
                
                return null;
            }
            else
            {
                if (_prevValue != null || !_hadValue)
                {
                    _hadValue = true;
                    _prevValue = null;
                    return msg;
                }
                return null;
            }
        }
    }
}
