using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Ruley.Core.Filters;
using Ruley.Core.Inputs;
using Ruley.Core.Outputs;

namespace Ruley.Core
{
    public class Rule : IDisposable
    {
        internal string FileName { get; set; }
        public bool Debug { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Input Input { get; set; }
        public List<Output> Outputs { get; set; }
        public List<Filter> Filters { get; set; }
        public event Action<Exception> OnError;

        internal void Validate()
        {
            if (Input == null)
                throw new Exception("Rule requires an input");

            if (Outputs == null || Outputs.Count == 0)
                throw new Exception("Rules require one or more outputs");

            GetComponents().ForEach(f => f.Validate());
        }

        private List<Component> GetComponents()
        {
            var list = new List<Component>();
            list.Add(Input);
            list.AddRange(Filters);
            list.AddRange(Outputs);
            return list;
        }

        public void Start()
        {
            Console.WriteLine("Starting rule '{0}' ({1})", Name, FileName);
            if (Filters == null)
                Filters = new List<Filter>();

            Filters.Add(new TimestampFilter() { Destination = "$processedUtc" });

            var logger = new Logger { IsDebugEnabled = Debug };
            
            GetComponents().ForEach(c =>
            {
                c.Logger = new Logger {IsDebugEnabled = Debug || c.Debug};
            });

            var stream = Input.Source;

            stream.Subscribe(o => { /*noop*/ }, e =>
            {
                if (OnError != null) OnError(e);
            });

            foreach (var filter in Filters)
            {
                if (filter.Enabled)
                {
                    var f = filter;
                    stream = stream.Do(m => logger.Debug("Applying filter {0}", f.GetType()));
                    stream = f.Extend(stream);
                }
            }

            foreach (var output in Outputs)
            {
                var o = output;
                if (o.Enabled)
                {
                    stream.Subscribe(m =>
                    {
                        //force outputs to execute sequentially in case they modify the payload
                        lock (m)
                        {
                            o.Do(m);
                        }
                    });
                }
            }

            Input.Start();
        }

        public void Dispose()
        {
            Console.WriteLine("Stopping rule '{0}' ({1})", Name, FileName);
            Input.Dispose();
        }
    }
}