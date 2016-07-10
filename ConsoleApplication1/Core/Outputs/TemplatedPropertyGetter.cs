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

            if (!str.Contains("{"))
            {
                _type = PropertyType.Value;
                return;
            }

            if (str.StartsWith("{") && str.EndsWith("}"))
            {
                var split = str.Split('{');
                if (split.Length == 2)
                {
                    _type = PropertyType.Field;
                    _fieldName = split[1].Replace("}", "");
                }
                else
                {
                    _type = PropertyType.Template;
                }
            }
            else
            {
                _type = PropertyType.Template;
            }
        }

        public T Get<T>(object value, DataBag msg)
        {
            //todo what about nulls default(T)?

            if (_type == PropertyType.Value)
            {
                return (T) value;
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