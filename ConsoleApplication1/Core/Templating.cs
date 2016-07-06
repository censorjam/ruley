using System.Dynamic;
using SmartFormat;

namespace Ruley.Core
{
    public static class Templater
    {
        public static string ApplyTemplate(string template, Event msg)
        {
            return Smart.Format(template, msg);
        }
    }
}