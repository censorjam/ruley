using System;
using System.Dynamic;
using System.Reactive.Linq;
using Newtonsoft.Json;
using Ruley.Core.Outputs;

namespace Ruley.Core.Filters
{
    public abstract class Filter : Component
    {
        public IObservable<ExpandoObject> Extend(IObservable<ExpandoObject> source)
        {
            return Observable(source.Do(m =>
            {
                Logger.Debug("Entering {0}", GetType().Name);
            }));
        }
      
        protected abstract IObservable<ExpandoObject> Observable(IObservable<ExpandoObject> source);
    }
}