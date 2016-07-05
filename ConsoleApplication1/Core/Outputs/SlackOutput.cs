using System.Dynamic;
using Newtonsoft.Json;

namespace Ruley.Core.Outputs
{
    public class SlackOutput : Output
    {
        private SlackClient _slackClient;
        public string WebHookUrl { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public string Emoji { get; set; }

        public override void Do(ExpandoObject msg)
        {
            if (_slackClient == null)
                _slackClient = new SlackClient(WebHookUrl);
            
            var payload = new Payload()
            {
                Text = Template(Message, msg),
                Username = Template(Username, msg),
                Emoji = Template(Emoji, msg)
            };
            _slackClient.PostMessage(payload);
            Logger.Debug(JsonConvert.SerializeObject(payload));
        }
    }
}