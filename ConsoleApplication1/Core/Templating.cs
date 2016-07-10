using System.Collections.Generic;
using System.Dynamic;
using DotLiquid;
using Ruley.Dynamic;
using SmartFormat;

namespace Ruley.Core
{
    public static class Templater
    {
        public static string ApplyTemplate(string template, DynamicDictionary o)
        {
            //var t = Template.Parse(template);
            //var hash = Hash.FromDictionary(o);
            //var s = t.Render(hash);
            return Smart.Format(template, o);
        }
    }
}