using System.Dynamic;
using System.Text;
using EasyNetQ;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ruley.Core;
using Ruley.Core.Inputs;

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
            var _bus = RabbitHutch.CreateBus(ConnectionString);
            var queue = _bus.Advanced.QueueDeclare(QueueName);
            var exchange = _bus.Advanced.ExchangeDeclare(ExchangeName, ExchangeType, true, true);

            _bus.Advanced.Bind(exchange, queue, "#");
            _bus.Advanced.Consume(queue, (x, b, c) =>
            {
                var s = Encoding.UTF8.GetString(x);
                var expandoObjectConverter = new ExpandoObjectConverter();
                var o = JsonConvert.DeserializeObject<ExpandoObject>(s, expandoObjectConverter);
                OnNext(new Event(o));
            });
        }

        public override void Dispose()
        {
            _bus.Dispose();
            base.Dispose();
        }
    }
}
