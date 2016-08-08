//using System;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Ruley.Graphite
//{
//    public class GraphiteInput : PollingInput
//    {
//        public string From { get; set; }
//        public string To { get; set; }
//        public string Query { get; set; }

//        private long _lastSent;

//        public override Event GetNext()
//        {
//            using (var client = new HttpClient())
//            {
//                client.BaseAddress = new Uri("http://lapgrp001:8080");
//                client.DefaultRequestHeaders.Accept.Clear();
//                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//                var query = "render?target=" + Query + "&format=json&from=" + From + "&to=" + To;
//                var response = client.GetAsync(query).Result;
//                if (response.IsSuccessStatusCode)
//                {
//                    string data = response.Content.ReadAsStringAsync().Result;
//                    dynamic payload = FromJson("{ 'graphitedata': " + data + " }");
//                    List<object> p = payload.graphitedata[0].datapoints;// ["graphitedata"];
//                    p.Reverse();

//                    foreach (IList<object> o in p)
//                    {
//                        if ((long)o[1] > _lastSent && o[0] != null)
//                        {
//                            _lastSent = (long)o[1];
//                            dynamic e = new ExpandoObject();
//                            e.Value = o[0];
//                            e.Timestamp = o[1];
//                            e.Query = query;
//                            return new Event(e);
//                        }
//                    }
//                }
//                throw new Exception("graphite call failed");
//            }
//        }
//    }
//}
