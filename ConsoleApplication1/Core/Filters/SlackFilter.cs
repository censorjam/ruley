using System;
using Newtonsoft.Json;
using Ruley.Core.Outputs;

namespace Ruley.Core.Filters
{
    public class SlackFilter : InlineFilter
    {
        private SlackClient _slackClient;
        public Property<string> WebHookUrl { get; set; }
        public Property<string> Username { get; set; }
        public Property<string> Message { get; set; }
        public Property<string> Emoji { get; set; }

        public override Event Apply(Event @event)
        {
            if (_slackClient == null)
                _slackClient = new SlackClient(WebHookUrl.Get(@event));

            var payload = new Payload()
            {
                Text = Message.Get(@event),
                Username = Username.Get(@event),
                Emoji = Emoji.Get(@event)
            };

            try
            {
                _slackClient.PostMessage(payload);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            Logger.Debug(JsonConvert.SerializeObject(payload));
            return @event;
        }
    }
}