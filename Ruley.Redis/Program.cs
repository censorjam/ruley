using System;
using System.Dynamic;
using Ruley.Core;
using Ruley.Core.Filters;
using Ruley.Core.Inputs;
using Ruley.Dynamic;
using StackExchange.Redis;

namespace Ruley.Redis
{
    public class RedisBusInput : Input
    {
        public string ConnectionString { get; set; }
        public string Channel { get; set; }

        public override void Start()
        {
            var redis = ConnectionMultiplexer.Connect(ConnectionString);
            redis.GetSubscriber().Subscribe(Channel, (a, b) =>
            {
                try
                {
                    OnNext(DynamicDictionary.Create(b));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }
    }

    public class RedisPing : InlineFilter
    {
        private IServer _server;
        private readonly object _lock = new object();

        public Property<string> Host { get; set; }
        public Property<string> Password { get; set; }
        public Property<long> Port { get; set; }

        public override Event Apply(Event msg)
        {
            string connectionString = Host.Get(msg);
            if (Port != null)
            {
                connectionString += ":" + Port.Get(msg);
            }
            if (Password != null)
            {
                connectionString += ",password=" + Password.Get(msg);
            }

            lock (_lock)
            {
                if (_server == null)
                {
                    try
                    {
                        //todo send unhealthy if this fails
                        var options = ConfigurationOptions.Parse(connectionString);
                        options.AllowAdmin = true;
                        var redis = ConnectionMultiplexer.Connect(options);
                        _server = redis.GetServer(redis.GetEndPoints()[0]);
                    }
                    catch (Exception e)
                    {
                        dynamic m = msg.Data;
                        m.exception = e;
                        m.ping = null;
                        m.pingMs = null;
                        return msg;
                    }
                }
            }

            try
            {
                var elapsed = _server.Ping();
                dynamic payload = msg.Data;
                payload.exception = null;
                payload.ping = elapsed;
                payload.pingMs = elapsed.TotalMilliseconds;
                return msg;
            }
            catch (Exception e)
            {
                dynamic m = msg.Data;
                m.exception = e;
                m.ping = null;
                m.pingMs = null;
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
