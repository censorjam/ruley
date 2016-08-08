using System;

namespace Ruley.Core.Filters
{
    public class BranchFilter : InlineFilter
    {
        public Property<string> Value { get; set; }
        public InlineFilter Then { get; set; }
        public InlineFilter Else { get; set; }

        public override Event Apply(Event x)
        {
            Then = Then ?? new PassThroughFilter();
            Else = Else ?? new BlockFilter();

            var match = (bool)Value.GetValue(x);
            var next = match ? DoTrue(x) : DoFalse(x);

            return next;
        }

        private Event DoFalse(Event e)
        {
            Logger.Debug("Executing false branch value");
            return Else.Apply(e);
        }

        private Event DoTrue(Event e)
        {
            Logger.Debug("Executing true branch");
            return Then.Apply(e);
        }
    }

    //public class BranchFilter : ConditionalBaseFilter
    //{
    //    public InlineFilter True { get; set; }
    //    public InlineFilter False { get; set; }

    //    public override Event Apply(Event x)
    //    {
    //        True = True ?? new PassThroughFilter();
    //        False = False ?? new BlockFilter();

    //        var match = RunMatch(x);
    //        var next = match ? DoTrue(x) : DoFalse(x);

    //        return next;
    //    }

    //    private Event DoFalse(Event e)
    //    {
    //        Logger.Debug("Executing false branch value");
    //        return False.Apply(e);
    //    }

    //    private Event DoTrue(Event e)
    //    {
    //        Logger.Debug("Executing true branch");
    //        return True.Apply(e);
    //    }
    //}
}