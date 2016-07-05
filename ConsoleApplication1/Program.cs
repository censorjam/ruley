using System;
using Ruley.Core;

namespace Ruley
{
    public interface IRuleyLogger
    {
        void Info(string message);
        void Debug(string message);
        void Exception(Exception e);
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var rm = new RuleManager();
            rm.Start();

            Console.ReadLine();
        }
    }
}
