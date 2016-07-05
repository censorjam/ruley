using System.Dynamic;

namespace Ruley.Core.Filters
{
    public class MessageBusPublisherFilter : InlineFilter
    {
        public Property<string> Key { get; set; }

        public override ExpandoObject Apply(ExpandoObject msg)
        {
            RuleManager.MessageBus.Publish(Key.Get(msg), msg);
            return msg;
        }
    }
}
