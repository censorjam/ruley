using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
