using System;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Ruley.Core.Inputs
{
    public class HttpPollerInput : PollingInput
    {
        public Property<string> Url { get; set; }
        public Property<string> Username { get; set; }
        public Property<string> Password { get; set; }
        public Property<string> Authentication { get; set; }

        public override async void OnTick()
        {
            dynamic ex = new ExpandoObject();
            ex.Properties = Properties;
            var ev = new Event(ex);

            using (var client = new HttpClient())
            {
                if (Authentication.Get(ev) == "basic")
                {
                    var encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(String.Format("{0}:{1}", Username.Get(ev), Password.Get(ev))));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);
                }

                dynamic n = new ExpandoObject();
                try
                {
                    var url = Url.Get(ev);
                    var response = await client.GetAsync(url);
                    n.StatusCode = (long)response.StatusCode; 
                    n.GotResponse = true;
                }
                catch (Exception e)
                {
                    n.StatusCode = -1;
                    n.GotResponse = false;
                    n.ExceptionMessage = e.Message;
                }

                OnNext(n);
            }
        }
    }
}