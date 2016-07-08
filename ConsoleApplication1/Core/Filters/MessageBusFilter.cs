namespace Ruley.Core.Filters
{
    public class MessageBusPublisherFilter : InlineFilter
    {
        public Property<string> Key { get; set; }

        public override Event Apply(Event msg)
        {
            RuleManager.MessageBus.Publish(Key.Get(msg), msg.Data);
            return msg;
        }
    }
}
