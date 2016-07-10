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
                    try
                    {
                        //todo send unhealthy if this fails
                        var options = ConfigurationOptions.Parse(ConnectionString);
                        options.AllowAdmin = true;
                        var redis = ConnectionMultiplexer.Connect(options);
                        _server = redis.GetServer(redis.GetEndPoints()[0]);
                    }
                    catch (Exception e)
                    {
                        dynamic m = msg.Data;
                        m.exception = e;
                        m.ping = null;
                        return msg;
                    }
                }
            }

            try
            {
                var elapsed = _server.Info();
                dynamic payload = msg.Data;
                payload.exception = null;
                payload.Ping = elapsed;
                return msg;
            }
            catch (Exception e)
            {
                dynamic m = msg.Data;
                m.exception = e;
                m.ping = null;
                return msg;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
