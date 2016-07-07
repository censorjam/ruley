using System;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Ruley.Core.Inputs
{
    public class HttpPollerInput : PollingInput
    {
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Authentication { get; set; }

        public override async void OnTick()
        {
            using (var client = new HttpClient())
            {
                if (Authentication == "basic")
                {
                    var encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(String.Format("{0}:{1}", Username, Password)));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);
                }

                dynamic n = new ExpandoObject();
                try
                {
                    var response = await client.GetAsync(Url);
                    n.StatusCode = (long)response.StatusCode; 
                    n.Content = response.Content;
                    n.GotResponse = true;
                }
                catch (Exception e)
                {
                    n.StatusCode = -1;
                    n.GotResponse = false;
                    n.ExceptionMessage = e.Message;
                }

                OnNext(new Event(n));
            }
        }
    }

    public class TestInput : PollingInput
    {
        private int _number = 1;
        private Random r = new Random();

        public override void OnTick()
        {
            string json;
            if (r.NextDouble() > 0.5)
            {
                json = "{ 'appName':'finscoreservice', 'host':'L1APDEV001', 'o': { 'value': 1000 } }";
            }
            else
            {
                json = "{ 'appName':'sportscore', 'host':'L1APDEV001', 'o': { 'value': 1000 } }";
            }

            dynamic x = FromJson(json);
            _number = _number + 10;
            x.o.value = _number;
            OnNext(new Event(x));
        }
    }
}