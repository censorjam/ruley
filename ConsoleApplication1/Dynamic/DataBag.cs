using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;

namespace Ruley.Dynamic
{
    public class DataBag : DynamicObject, IDictionary<string, object>
    {
        private readonly IDictionary<string, object> _dictionary = new Dictionary<string, object>();

        public DataBag()
        {
        }

        public DataBag(IDictionary<string, object> source)
        {
            _dictionary = source;
        }

        public void Add(KeyValuePair<string, object> item)
        {
            _dictionary.Add(item);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return _dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return _dictionary.Remove(item);
        }

        public int Count
        {
            get
            {
                return _dictionary.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return _dictionary.IsReadOnly; }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _dictionary.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _dictionary[binder.Name] = value;
            return true;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }

        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void Add(string key, object value)
        {
            _dictionary.Add(key, value);
        }

        public bool Remove(string key)
        {
            return _dictionary.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public object this[string key]
        {
            get { return _dictionary[key]; }
            set { _dictionary[key] = value; }
        }

        public ICollection<string> Keys
        {
            get { return _dictionary.Keys; }
        }

        public ICollection<object> Values
        {
            get { return _dictionary.Values; }
        }

        public object GetValue(string path)
        {
            try
            {
                IDictionary<string, object> o = this;
                var split = path.Split('.');

                for (var s = 0; s < split.Length; s++)
                {
                    if (s == split.Length - 1)
                    {
                        return o[split[s]];
                    }
                    else
                    {
                        o = (IDictionary<string, object>)o[split[s]];
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("GetValue failed with path '{0}'", path));
            }

            throw new Exception("broken");
        }

        public object SetValue(string path, object value)
        {
            return _dictionary[path] = value;
        }

        public DataBag Clone()
        {
            return (DataBag)JsonHelper.Deserialize(JsonConvert.SerializeObject(this));
        }

        public void Merge(DataBag other)
        {
            foreach (var o in other)
            {
                this[o.Key] = o.Value;
            }
        }

        public bool HasProperty(string property)
        {
            return _dictionary.ContainsKey(property);
        }

        public static DataBag FromObject(object o)
        {
            return (DataBag)JsonHelper.Deserialize(JsonConvert.SerializeObject(o));
        }

        public static DataBag FromJson(string json)
        {
            return (DataBag) JsonHelper.Deserialize(json);
        }
    }
}
