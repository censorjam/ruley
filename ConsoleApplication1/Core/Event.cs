using System;
using System.Collections.Generic;
using Ruley.Dynamic;

namespace Ruley.Core
{
    public class Event
    {
        public DynamicDictionary Data { get; set; }

        public Event()
        {
            Data = new DynamicDictionary();
        }

        public Event(DynamicDictionary data)
        {
            Data = data;
        }

        internal static Event Create(DynamicDictionary data)
        {
            return new Event(data);
        }
    }
}