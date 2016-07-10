using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace Ruley.Dynamic
{
    public class DataBag : DynamicObject, IDictionary<string, object>
    {
        public DataBag()
        {
        }

        public DataBag(IDictionary<string, object> source)
        {
            dictionary = source;
        }

        // The inner dictionary.
        IDictionary<string, object> dictionary = new Dictionary<string, object>();
        public void Add(KeyValuePair<string, object> item)
        {
            dictionary.Add(item);
        }

        public void Clear()
        {
            dictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            dictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return dictionary.Remove(item);
        }

        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return dictionary.IsReadOnly; }
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

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)dictionary).GetEnumerator();
        }

        public bool ContainsKey(string key)
        {
            return dictionary.ContainsKey(key);
        }

        public void Add(string key, object value)
        {
            dictionary.Add(key, value);
        }

        public bool Remove(string key)
        {
            return dictionary.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        public object this[string key]
        {
            get { return dictionary[key]; }
            set { dictionary[key] = value; }
        }

        public ICollection<string> Keys
        {
            get { return dictionary.Keys; }
        }

        public ICollection<object> Values
        {
            get { return dictionary.Values; }
        }
    }
}
