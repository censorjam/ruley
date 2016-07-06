using System;
using System.Dynamic;

namespace Ruley.Core
{
    public class GlobalScope
    {
        private readonly string _guid = System.Guid.NewGuid().ToString();
        public string Guid
        {
            get
            {
                return _guid;
            }
        }
    }

    public class RuleScope
    {
        private string _ruleName = "testymctestface";
        public string RuleName
        {
            get
            {
                return _ruleName;
            }
        }
    }

    public class Event
    {
        public DateTime? Created { get; set; }
        public DateTime? Processed { get; set; }
        public ExpandoObject Data { get; set; }

        public Event()
        {
            Data = new ExpandoObject();
            Global = RuleManager.GlobalVars;
        }

        public Event(ExpandoObject data)
        {
            Data = data;
            Global = RuleManager.GlobalVars;
        }

        public GlobalScope Global { get; set; }
        public RuleScope Rule { get; set; }
    }
}