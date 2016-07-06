using System;
using System.Dynamic;
using Newtonsoft.Json;
using Ruley.Core.Filters;
using Ruley.Core.Outputs;

namespace Ruley.Core
{
    public class Property<T>
    {
        private object _value;
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                _getter = new TemplatedPropertyGetter(value);
            }
        }

        private TemplatedPropertyGetter _getter;

        public Property()
        {
        }

        public Property(object value)
        {
            Value = value;
            _getter = new TemplatedPropertyGetter(value);
        }

        public static implicit operator string(Property<T> d)
        {
            throw new Exception("Implicit cast not allowed, use Get() method");
        }

        public static implicit operator Property<T>(string d)
        {
            return new Property<T>(d);
        }

        public static implicit operator long(Property<T> d)
        {
            throw new Exception("Invalid cast");
        }

        public static implicit operator Property<T>(long d)
        {
            return new Property<T>(d);
        }

        public static implicit operator double(Property<T> d)
        {
            throw new Exception("Invalid cast");
        }

        public static implicit operator Property<T>(double d)
        {
            return new Property<T>(d);
        }

        public static implicit operator bool(Property<T> d)
        {
            return false;
        }

        public static implicit operator Property<T>(bool d)
        {
            return new Property<T>(d);
        }

        public T Get(Event e)
        {
            return _getter.Get<T>(Value, e.Data);
        }
    }
}