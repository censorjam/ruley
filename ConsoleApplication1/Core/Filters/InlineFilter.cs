using System;
using System.Dynamic;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Ruley.Core.Filters
{
    public abstract class InlineFilter : Filter
    {
        private readonly Subject<ExpandoObject> _subject = new Subject<ExpandoObject>();
        public abstract ExpandoObject Do(ExpandoObject msg);

        protected override IObservable<ExpandoObject> Observable(IObservable<ExpandoObject> source)
        {
            source.Subscribe(m =>
            {
                var next = Do(m);
                if (next != null)
                {
                    _subject.OnNext(next);
                }
            });
            return _subject.AsObservable();
        }
    }
}