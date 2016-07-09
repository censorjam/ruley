using System;
using Ruley.Core;
using Ruley.Core.Filters;
using StackExchange.Redis;

namespace Ruley.Redis
{
    public class RedisPing : InlineFilter
    {
        public string ConnectionString { get; set; }
        private IServer _server;
        private readonly object _lock = new object();

        public override Event Apply(Event msg)
        {
            lock (_lock)
            {
                if (_server == null)
                {
                    //todo send unhealthy if this fails
                    var options = ConfigurationOptions.Parse(ConnectionString);
                    options.AllowAdmin = true;
                    var redis = ConnectionMultiplexer.Connect(options);
                    _server = redis.GetServer(redis.GetEndPoints()[0]);
                }
            }

            var elapsed = _server.Info();
            dynamic payload = msg.Data.AsDynamic();
            payload.Ping = elapsed;
            return msg;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
