using System;
using System.Diagnostics;
using System.Dynamic;
using System.Net;
using System.Reactive.Linq;
using System.Text;
using EasyNetQ;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ruley.Core;
using Ruley.Core.Inputs;
using Ruley.Dynamic;

namespace Ruley.RabbitMq
{
  
    public class RabbitMqInput : Input
    {
        public string ConnectionString { get; set; }
        public string ExchangeName { get; set; }
        public string ExchangeType { get; set; }
        public string QueueName { get; set; }

        private IBus _bus;

        public override void Start()
        {
            _bus = RabbitHutch.CreateBus(ConnectionString);
            var queue = _bus.Advanced.QueueDeclare(QueueName, false, true, false, true);
            var exchange = _bus.Advanced.ExchangeDeclare(ExchangeName, ExchangeType);

            //todo currently only works with topic
            _bus.Advanced.Bind(exchange, queue, "#");
            _bus.Advanced.Consume(queue, (x, b, c) =>
            {
                var s = Encoding.UTF8.GetString(x);
                OnNext(DynamicDictionary.Create(s));
            });
        }

        public override void Dispose()
        {
            _bus.Dispose();
            base.Dispose();
        }
    }
}
