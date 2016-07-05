using System;
using System.Dynamic;
using System.Reactive.Linq;

namespace Ruley.Core.Filters
{
    public abstract class InlineFilter : Filter
    {
        public abstract ExpandoObject Apply(ExpandoObject msg);

        protected override IObservable<ExpandoObject> Observable(IObservable<ExpandoObject> source)
        {
            return source.Select(Apply);
        }
    }
}