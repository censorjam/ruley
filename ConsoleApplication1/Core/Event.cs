using System;
using System.Dynamic;

namespace Ruley.Core
{
    public class Event
    {
        public DateTime? Created { get; set; }
        public DateTime? Processed { get; set; }
        public ExpandoObject Data { get; set; }
        
        private Event(ExpandoObject data)
        {
            Data = data;
        }

        internal static Event Create(ExpandoObject data)
        {
            return new Event(data);
        }
    }
}