using System;
using System.Dynamic;
using System.Reactive.Linq;

namespace Ruley.Core.Filters
{
    public abstract class InlineFilter : Filter
    {
        public abstract Event Apply(Event msg);

        protected override IObservable<Event> Observable(IObservable<Event> source)
        {
            return source.Select(Apply);
        }
    }
}