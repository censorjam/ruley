using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ruley.Core
{
    //public static class ExpandoHelper
    //{
    //    public static object GetValue(this ExpandoObject msg, string path)
    //    {
    //        try
    //        {
    //            var o = msg;
    //            var split = path.Split('.');

    //            for (var s = 0; s < split.Length; s++)
    //            {
    //                if (s == split.Length - 1)
    //                {
    //                    return ((IDictionary<string, object>) o)[split[s]];
    //                }
    //                else
    //                {
    //                    o = (ExpandoObject) ((IDictionary<string, object>) o)[split[s]];
    //                }
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            throw new Exception(string.Format("GetValue failed with path '{0}'", path));
    //        }

    //        throw new Exception("broken");

    //        //while (path.Contains("{"))
    //        //{
    //        //    path = Smart.Format(path, msg);
    //        //}
    //        //var value = ((IDictionary<string, object>)msg)[path];
    //        //return value;
    //    }

    //    public static ExpandoObject ToExpando(this object obj)
    //    {
    //        var converter = new ExpandoObjectConverter();
    //        return JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(obj), converter);
    //    }

    //    public static void Merge(this ExpandoObject expando, ExpandoObject other)
    //    {
    //        var d = expando.AsDictionary();
    //        foreach (var o in other.AsDictionary())
    //        {
    //            d[o.Key] = o.Value;
    //        }
    //    }

    //    public static dynamic AsDynamic(this ExpandoObject msg)
    //    {
    //        return msg;
    //    }

    //    public static IDictionary<string, object> AsDictionary(this ExpandoObject msg)
    //    {
    //        return msg;
    //    }

    //    public static void SetValue(this ExpandoObject msg, string path, object value)
    //    {
    //        ((IDictionary<string, object>)msg)[path] = value;
    //    }

    //    public static bool HasErrors(this ExpandoObject msg)
    //    {
    //        return msg.HasProperty("$$errors");
    //    }

    //    public static bool HasProperty(this ExpandoObject msg, string path)
    //    {
    //        return ((IDictionary<string, object>) msg).ContainsKey(path);
    //    }

    //    public static void DeleteValue(this ExpandoObject msg, string path)
    //    {
    //        ((IDictionary<string, object>) msg).Remove(path);
    //    }

    //    public static ExpandoObject Clone(this ExpandoObject msg)
    //    {
    //        return JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(msg, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto }));
    //    }

    //    public static void AddError(this ExpandoObject msg, string errorMessage)
    //    {
    //        List<string> errors;
    //        if (msg.HasProperty("$$errors"))
    //        {
    //            errors = (List<string>)msg.GetValue("$$errors");
    //        }
    //        else
    //        {
    //            errors = new List<string>();
    //            msg.SetValue("$$errors", errors);
    //        }
    //        errors.Add(errorMessage);
    //    }
    //}
}