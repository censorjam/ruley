using System;
using System.Dynamic;

namespace Ruley.Core
{
    public class Event
    {
        public DateTime? Created { get; set; }
        public DateTime? Processed { get; set; }
        public ExpandoObject Data { get; set; }

        public Event()
        {
            Data = new ExpandoObject();
        }

        public Event(ExpandoObject data)
        {
            Data = data;
        }
    }
}