using System;
using System.Dynamic;
using System.Threading;
using Newtonsoft.Json;

namespace Ruley.Core.Inputs
{
    public class PollingInput : Input
    {
        [JsonProperty(Required = Required.Always)]
        public int Interval { get; set; }

        private Timer _timer;

        public override void Start()
        {
            _timer = new Timer(state =>
            {
                OnTick();
            });
            _timer.Change(0, Interval);
        }

        public override void Dispose()
        {
            _timer.Dispose();
        }

        public override void ValidateComposition()
        {
            if (Interval < 0)
                throw new Exception("Interval cannot be negative");
        }

        public virtual void OnTick()
        {
            OnNext(new ExpandoObject());
        }
    }
}