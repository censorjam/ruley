using System.Collections.Generic;
using System.Dynamic;
using DotLiquid;
using SmartFormat;

namespace Ruley.Core
{
    public class DynamicDictionary : DynamicObject
    {
        // The inner dictionary.
        Dictionary<string, object> dictionary = new Dictionary<string, object>();

        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string name = binder.Name.ToLower();
            return dictionary.TryGetValue(name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            dictionary[binder.Name.ToLower()] = value;
            return true;
        }
    }



    public static class Templater
    {
        public static string ApplyTemplate(string template, ExpandoObject o)
        {
            var t = Template.Parse(template);
            var hash = Hash.FromDictionary(o);
            var s = t.Render(hash);
            return s;//Smart.Format(template, msg);
        }
    }
}