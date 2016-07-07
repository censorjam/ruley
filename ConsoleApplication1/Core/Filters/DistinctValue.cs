namespace Ruley.Core.Filters
{
    public class DistinctValueFilter : InlineFilter
    {
        public Property<string> Field { get; set; }
        private string _prevValue;

        public override Event Apply(Event msg)
        {
            var n = msg.Data.GetValue(Field.Get(msg));
            var next = n != null ? n.ToString() : null;

            if (next != _prevValue)
            {
                _prevValue = next;
                return msg;
            }
            else
            {
                _prevValue = next;
                return null;
            }
        }
    }
}
