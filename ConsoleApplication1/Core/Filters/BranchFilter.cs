using System;

namespace Ruley.Core.Filters
{
    public class BranchFilter : ConditionalBaseFilter
    {
        public InlineFilter True { get; set; }
        public InlineFilter False { get; set; }

        public override Event Apply(Event x)
        {
            True = True ?? new BlockFilter();
            False = False ?? new BlockFilter();

            var match = RunMatch(x);
            var next = match ? DoTrue(x) : DoFalse(x);

            return next;
        }

        private Event DoFalse(Event e)
        {
            Console.WriteLine("Executing false branch value");
            return False.Apply(e);
        }

        private Event DoTrue(Event e)
        {
            Console.WriteLine("Executing true branch");
            return True.Apply(e);
        }
    }
}