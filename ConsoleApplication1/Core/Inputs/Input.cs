using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ruley.Core.Outputs;

namespace Ruley.Core.Inputs
{
    public abstract class Input : Component
    {
        public abstract void Start();
        private readonly Subject<ExpandoObject> _subject = new Subject<ExpandoObject>();
        private readonly object _lock = new object();

        public IObservable<ExpandoObject> Source
        {
            get { return _subject.AsObservable(); }
        }

        public void OnNext(ExpandoObject next)
        {
            ((IDictionary<string,object>)next)["$createdUtc"] = DateTime.UtcNow;
            lock (_lock)
            {
                _subject.OnNext(next);
            }
        }

        public ExpandoObject ToExpando(object obj)
        {
            var converter = new ExpandoObjectConverter();
            return JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(obj), converter);
        }

        public ExpandoObject FromJson(string json)
        {
            var converter = new ExpandoObjectConverter();
            return JsonConvert.DeserializeObject<ExpandoObject>(json, converter);
        }
    }

    public class MessageBusInput : Input
    {
        public string Key { get; set; }

        public override void Start()
        {
            RuleManager.MessageBus.Subscribe(Key, OnNext);
        }
    }
}