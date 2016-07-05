using System;
using System.Dynamic;
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

        public override ExpandoObject Apply(ExpandoObject msg)
        {
            if (_slackClient == null)
                _slackClient = new SlackClient(WebHookUrl);

            var payload = new Payload()
            {
                Text = Message.Get(msg),
                Username = Username.Get(msg),
                Emoji = Emoji.Get(msg)
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
            return msg;
        }
    }
}