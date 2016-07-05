using System;
using System.Dynamic;

namespace Ruley.Core.Filters
{
    public class AddFieldFilter : InlineFilter
    {
        [Required]
        public Property<string> Destination { get; set; }

        [Required]
        public Property<object> Value { get; set; }

        public override ExpandoObject Apply(ExpandoObject msg)
        {
            var dest = Destination.Get(msg);
            var val = Value.Get(msg);

            Console.WriteLine("dest = {0}, val = {1}", dest, val);

            msg.SetValue(dest, val);
            return msg;
        }
    }
}
