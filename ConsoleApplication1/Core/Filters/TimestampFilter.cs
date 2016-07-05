using System;
using System.Dynamic;
using Newtonsoft.Json;
using Ruley.Core.Outputs;

namespace Ruley.Core.Filters
{
    public class TimestampFilter : InlineFilter
    {
        [JsonProperty(Required = Required.Always)]
        public string Destination { get; set; }

        public override ExpandoObject Do(ExpandoObject msg)
        {
            msg.SetValue(Destination, DateTime.Now);
            return msg;
        }
    }

    public class SlackFilter : InlineFilter
    {
        private SlackClient _slackClient;
        public string WebHookUrl { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public string Emoji { get; set; }

        public override ExpandoObject Do(ExpandoObject msg)
        {
            if (_slackClient == null)
                _slackClient = new SlackClient(WebHookUrl);

            var payload = new Payload()
            {
                Text = Template(Message, msg),
                Username = Template(Username, msg),
                Emoji = Template(Emoji, msg)
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
