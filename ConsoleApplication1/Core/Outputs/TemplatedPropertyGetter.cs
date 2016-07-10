using System;
using Ruley.Dynamic;

namespace Ruley.Core.Outputs
{
    public class TemplatedPropertyGetter
    {
        public enum PropertyType
        {
            Value,
            Field,
            Template
        }

        private PropertyType _type;
        private string _fieldName;

        public TemplatedPropertyGetter(object value)
        {
            SetPropertyType(value);
        }

        public void SetPropertyType(object value)
        {
            var str = value as string;
            if (str == null)
            {
                _type = PropertyType.Value;
                return;
            }

            if (str.StartsWith("[") && str.EndsWith("]"))
            {
                _type = PropertyType.Field;
                _fieldName = str.Substring(1, str.Length - 2);
            }

            if (!str.Contains("{"))
            {
                _type = PropertyType.Value;
                return;
            }

            _type = PropertyType.Template;
        }

        public T Get<T>(object value, DynamicDictionary msg)
        {
            //todo what about nulls default(T)?

            if (_type == PropertyType.Value)
            {
                return (T)value;
            }

            if (_type == PropertyType.Field)
            {
                return (T)msg.GetValue(_fieldName);
            }

            if (_type == PropertyType.Template)
            {
                return (T)(object)Templater.ApplyTemplate(value as string, msg);
            }

            throw new Exception("error");
        }
    }
}