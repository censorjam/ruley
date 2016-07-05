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

        private Subject<ExpandoObject> _subject = new Subject<ExpandoObject>();
        protected override IObservable<ExpandoObject> Observable(IObservable<ExpandoObject> source)
        {
            source.GroupBy(m => m.GetValue(Key)).Subscribe(i =>
            {
                var subject = new Subject<ExpandoObject>();
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
