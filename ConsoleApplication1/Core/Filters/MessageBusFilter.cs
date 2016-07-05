using System.Dynamic;

namespace Ruley.Core.Filters
{
    public class MessageBusPublisherFilter : InlineFilter
    {
        public string Key { get; set; }

        public override ExpandoObject Do(ExpandoObject msg)
        {
            RuleManager.MessageBus.Publish(Get<string>(Key), msg);
            return msg;
        }
    }
}
