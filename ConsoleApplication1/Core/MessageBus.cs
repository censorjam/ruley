using System;
using System.Collections.Generic;
using System.Dynamic;
using Ruley.Core.Filters;

namespace Ruley.Core
{
    public class MessageBus
    {
        public void Publish(string key, ExpandoObject msg)
        {
            lock (_subs)
            {
                List<Action<ExpandoObject>> subs;
                _subs.TryGetValue(key, out subs);

                if (subs != null)
                {
                    foreach (var s in subs)
                    {
                        s(msg);
                    }
                }
            }
        }

        private Dictionary<string, List<Action<ExpandoObject>>> _subs = new Dictionary<string, List<Action<ExpandoObject>>>();
        public void Subscribe(string key, Action<ExpandoObject> handler)
        {
            lock (_subs)
            {
                List<Action<ExpandoObject>> subs;
                _subs.TryGetValue(key, out subs);

                if (subs != null)
                {
                    subs.Add(handler);
                }
                else
                {
                    _subs.Add(key, new List<Action<ExpandoObject>>() { handler });
                }
            }
        }
    }
}