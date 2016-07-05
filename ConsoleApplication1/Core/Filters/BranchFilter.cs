using System;
using System.Dynamic;

namespace Ruley.Core.Filters
{
    public class BranchFilter : ConditionalBaseFilter
    {
        public InlineFilter True { get; set; }
        public InlineFilter False { get; set; }

        public override ExpandoObject Apply(ExpandoObject x)
        {
            True = True ?? new BlockFilter();
            False = False ?? new BlockFilter();

            var match = RunMatch(x);
            var next = match ? DoTrue(x) : DoFalse(x);

            return next;
        }

        private ExpandoObject DoFalse(ExpandoObject m)
        {
            Console.WriteLine("Executing false branch value");
            return False.Apply(m);
        }

        private ExpandoObject DoTrue(ExpandoObject m)
        {
            Console.WriteLine("Executing true branch");
            return True.Apply(m);
        }
    }
}