using System;
using Ruley.Core;
using Ruley.Core.Outputs;

namespace Ruley
{
    //todos
    //todo end to end unit test with json file
    //todo logging
    //todo alias'
    //todo sub folders with inherited global.js files
    //todo auto validation with [required]
    //todo [Default()] property attribute?
    //todo add some kind of OnFirst to filters

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
