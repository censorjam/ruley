using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public class ThrottleFilter : Filter
    {
        [JsonProperty(Required = Required.Always)]
        public double Interval { get; set; }
        public string Key { get; set; }
        public string CountField { get; set; }
        public bool AllowEmpty { get; set; }

        private readonly Subject<Event> _subject = new Subject<Event>();

        protected override IObservable<Event> Observable(IObservable<Event> source)
        {
            if (string.IsNullOrEmpty(Key))
            {
                source.Buffer(TimeSpan.FromMilliseconds(Interval)).Subscribe(x => _subject.OnNext(Reduce(null, null, x)));

                return _subject.AsObservable();
            }
            else
            {
                source.GroupBy(m => m.Data.GetValue(Key)).Subscribe(i =>
                {
                    var keyField = Key;

                    i.Buffer(TimeSpan.FromMilliseconds(Interval)).Subscribe(x =>
                    {
                        if (AllowEmpty || x.Count > 0)
                        {
                            _subject.OnNext(Reduce(keyField, i.Key, x));
                        }
                    });
                });

                return _subject.AsObservable();
            }
        }

        private Event Reduce(string keyField, object key, IList<Event> msgs)
        {
            var count = msgs.Count;
            if (count == 0)
            {
                msgs = new List<Event>();
                msgs.Add(new Event());
                //msgs[0].SetValue(keyField, key);
            }

            msgs[0].Data.SetValue(CountField ?? "count", count);
            return msgs[0];
        }
    }
}