using System;
using Ruley.Dynamic;

namespace Ruley.Core
{
    public class Event
    {
        public DateTime? Created { get; set; }
        public DateTime? Processed { get; set; }
        public DataBag Data { get; set; }

        public Event()
        {
            Data = new DataBag();
        }

        public Event(DataBag data)
        {
            Data = data;
        }

        internal static Event Create(DataBag data)
        {
            return new Event(data);
        }
    }
}