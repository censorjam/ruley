﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Ruley.Core.Filters
{
    public class HttpFilter : InlineFilter
    {
        public Property<string> Url { get; set; }
        public Property<string> Username { get; set; }
        public Property<string> Password { get; set; }
        public Property<string> Authentication { get; set; }
        public Property<long> Timeout { get; set; }

        public override Event Apply(Event ev)
        {
            using (var client = new HttpClient())
            {
                if (Timeout != null)
                    client.Timeout = TimeSpan.FromMilliseconds(Timeout.Get(ev));

                if (Authentication.Get(ev) == "basic")
                {
                    var encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(String.Format("{0}:{1}", Username.Get(ev), Password.Get(ev))));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);
                }

                dynamic data = ev.Data;
                try
                {
                    var url = Url.Get(ev);
                    var response = client.GetAsync(url).Result;
                    data.statusCode = (long)response.StatusCode;
                    data.gotResponse = true;
                }
                catch (Exception e)
                {
                    data.statusCode = null;
                    data.gotResponse = false;
                    data.exception = e;
                }
            }
            return ev;
        }
    }
}