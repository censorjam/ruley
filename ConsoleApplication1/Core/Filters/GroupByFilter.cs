using System;
using System.Dynamic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Newtonsoft.Json;

namespace Ruley.Core.Filters
{
    public class FilterContainer
    {
        public Filter Filter { get; set; }
    }

    public class GroupByFilter : Filter
    {
        public string Key { get; set; }
        public Filter Filter { get; set; }

        private Subject<Event> _subject = new Subject<Event>();
        protected override IObservable<Event> Observable(IObservable<Event> source)
        {
            source.GroupBy(m => m.Data.GetValue(Key)).Subscribe(i =>
            {
                var subject = new Subject<Event>();
                var serialize = JsonConvert.SerializeObject(new FilterContainer() { Filter = Filter }, new JsonSerializerSettings() {TypeNameHandling = TypeNameHandling.Auto});

                var filter = JsonConvert.DeserializeObject<FilterContainer>(serialize,
                    new JsonSerializerSettings() {TypeNameHandling = TypeNameHandling.Auto}).Filter;

                filter.Extend(subject.AsObservable()).Subscribe(_subject);
                i.Subscribe(subject);
            });

            return _subject.AsObservable();
        }
    }
}
