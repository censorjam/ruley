//using System;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Ruley.Core;
//using Ruley.Core.Inputs;

//namespace Ruley.Monitoring.Redis
//{
//    public class RedisPing : PollingInput
//    {
//        public string ConnectionString { get; set; }

//        private ConnectionMultiplexer _redis { get; set; }

//        public override void Start()
//        {
//            var options = ConfigurationOptions.Parse(ConnectionString);
//            options.AllowAdmin = true;
//            _redis = ConnectionMultiplexer.Connect(options);
//            base.Start();
//        }

//        public override Event OnTick()
//        {
//            var elapsed = _redis.GetServer(_redis.GetEndPoints()[0]).Info();
//            dynamic payload = new ExpandoObject();
//            payload.Ping = elapsed;
//            return new Event(payload);
//        }
//    }


//    class Program
//    {
//        static void Main(string[] args)
//        {
//        }
//    }
//}
