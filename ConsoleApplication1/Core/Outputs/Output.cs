using System;
using System.Dynamic;

namespace Ruley.Core.Outputs
{
    public class Logger
    {
        public bool IsDebugEnabled { get; internal set; }

        public void Debug(string msg)
        {
            if (IsDebugEnabled)
                Console.WriteLine(msg);
        }

        public void Debug(string msg, params object[] p)
        {
            if (IsDebugEnabled)
                Console.WriteLine(string.Format(msg, p));
        }

        public void Error(Exception e)
        {
            Console.WriteLine(e);
        }

        public void Error(string msg, params object[] p)
        {
            Console.WriteLine(string.Format(msg, p));
        }
    }

    public abstract class Output : Component
    {
        public abstract void Do(ExpandoObject msg);

        public void Execute(ExpandoObject msg)
        {
            CurrentMsg = msg;
            Do(msg);
        }
    }
}