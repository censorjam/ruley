using System;
using Ruley.Core;

namespace TestApp
{
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
