using System;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using SmartFormat;

namespace Ruley.Core.Outputs
{
    public abstract class Component : IDisposable
    {
        public Component()
        {
            Logger = new Logger();
        }

        protected ExpandoObject CurrentMsg;

        private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public bool Debug { get; set; }

        public Logger Logger { get; set; }

        public virtual void Dispose()
        {
        }

        public virtual void Validate()
        {
        }

        protected T Get<T>(object value)
        {
            var getter = new TemplatedPropertyGetter(value);
            return getter.Get<T>(value, CurrentMsg);
        }
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }


    }
}