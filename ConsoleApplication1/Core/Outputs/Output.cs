using System.Dynamic;

namespace Ruley.Core.Outputs
{
    public abstract class Output : Component
    {
        public abstract void Do(ExpandoObject msg);

        public void Execute(ExpandoObject msg)
        {
            Do(msg);
        }
    }
}