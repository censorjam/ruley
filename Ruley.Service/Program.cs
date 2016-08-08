using System;
using Nancy;
using Nancy.Hosting.Self;
using Ruley.Core;

namespace TestApp
{
    //public class RedisPing : PollingInput
    //{
    //    public string ConnectionString { get; set; }

    //    private ConnectionMultiplexer _redis { get; set; }

    //    public override void Start()
    //    {
    //        var options = ConfigurationOptions.Parse(ConnectionString);
    //        options.AllowAdmin = true;
    //        _redis = ConnectionMultiplexer.Connect(options);
    //        base.Start();
    //    }

    //    public override Event GetNext()
    //    {
    //        var elapsed = _redis.GetServer(_redis.GetEndPoints()[0]).Info();
    //        dynamic payload = new ExpandoObject();
    //        payload.Ping = elapsed;
    //        return new Event(payload);
    //    }
    //}

    public class RuleModule : NancyModule
    {
        public static RuleManager RuleManager { get; set; }

        public RuleModule()
        {
            Get["/rules"] = _ => Response.AsJson(RuleManager.GetRuleNames());

            Put["/rules/{name}"] = o =>
            {
                return "ok";
            };
        }
    }

    internal class Program
    {
        private static NancyHost _nancy;

        private static void Main(string[] args)
        {
            var rm = new RuleManager();
            RuleModule.RuleManager = rm;
            rm.Start();
            //rm.StartFileWatcher();
            
            //var uri = new Uri("http://localhost:12345/");
            //_nancy = new NancyHost(uri);
            //_nancy.Start();

            Console.ReadLine();
        }
    }
}
