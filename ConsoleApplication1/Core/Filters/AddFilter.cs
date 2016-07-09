namespace Ruley.Core.Filters
{
    public class AddFieldFilter : InlineFilter
    {
        [Required]
        public Property<string> Destination { get; set; }

        [Required]
        public Property<object> Value { get; set; }

        public override Event Apply(Event e)
        {
            var dest = Destination.Get(e);
            var val = Value.Get(e);

            Logger.Debug("dest = {0}, val = {1}", dest, val);

            e.Data.SetValue(dest, val);
            return e;
        }
    }
}
