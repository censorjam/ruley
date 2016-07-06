﻿using System.Collections.Generic;
using System.Dynamic;

namespace Ruley.Core.Filters
{
    public class ReplayFieldFilter : InlineFilter
    {
        public string Field { get; set; }

        private ExpandoObject _prev;

        public override Event Apply(Event msg)
        {
            var m = (IDictionary<string, object>)msg.Data;
            if (!msg.Data.HasProperty(Field) && _prev != null)
            {
                m[Field] = _prev.GetValue(Field);
            }
            _prev = msg.Data;
            return msg;
        }
    }
}