using System.Collections.Generic;
using System.Dynamic;
using NUnit.Framework;
using Ruley.Core;
using Ruley.Core.Filters;

namespace Ruley.Tests
{
    [TestFixture]
    public class MapTests
    {
        [Test]
        public void Single_Value_String_Map()
        {
            var filter = new MapFilter()
            {
                Field = "source",
                Mapping = new List<object[]>() { new object[] { "a1", "b1" } },
                Destination = "field"
            };

            ExpandoObject msg = new ExpandoObject();
            msg.SetValue("source", "a1");

            msg = filter.Apply(msg);
            Assert.AreEqual("b1", msg.GetValue("field"));
            Assert.IsFalse(msg.HasErrors());
        }

        [Test]
        public void Multi_Value_String_Map()
        {
            var filter = new MapFilter()
            {
                Field = "source",
                Mapping = new List<object[]>()
                    {
                        new object[] {"a1", "b1"},
                        new object[] {"a2", "b2"},
                        new object[] {"a3", "b3"}
                    },
                Destination = "field"
            };

            ExpandoObject msg = new ExpandoObject();
            msg.SetValue("source", "a3");

            msg = filter.Apply(msg);
            Assert.AreEqual("b3", msg.GetValue("field"));
            Assert.IsFalse(msg.HasErrors());
        }

        [Test]
        public void Integer_Map()
        {
            var filter = new MapFilter()
            {
                Field = "source",
                Mapping = new List<object[]>() 
                    { 
                        new object[] { 1, "one" }, 
                        new object[] { 2, "two" }, 
                        new object[] { 3, "three" } 
                    },
                Destination = "field"
            };

            ExpandoObject msg = new ExpandoObject();
            msg.SetValue("source", 2);

            msg = filter.Apply(msg);
            Assert.AreEqual("two", msg.GetValue("field"));
            Assert.IsFalse(msg.HasErrors());
        }

        [Test]
        public void Boolean_Map()
        {
            var filter = new MapFilter()
            {
                Field = "source",
                Mapping = new List<object[]>() 
                    { 
                        new object[] { true, "yes" }, 
                        new object[] { false, "no" }, 
                    },
                Destination = "field"
            };

            ExpandoObject msg = new ExpandoObject();

            msg.SetValue("source", true);
            msg = filter.Apply(msg);
            Assert.AreEqual("yes", msg.GetValue("field"));

            msg.SetValue("source", false);
            msg = filter.Apply(msg);
            Assert.AreEqual("no", msg.GetValue("field"));
            Assert.IsFalse(msg.HasErrors());
        }

        [Test]
        public void Default_Value()
        {
            var filter = new MapFilter()
            {
                Field = "source",
                Mapping = new List<object[]>()
                {
                    new object[] {"a1", "b1"},
                    new object[] {"a2", "b2"},
                },
                Destination = "field",
                DefaultValue = "default"
            };

            ExpandoObject msg = new ExpandoObject();
            msg.SetValue("source", "ax");

            msg = filter.Apply(msg);
            Assert.AreEqual("default", msg.GetValue("field"));
            Assert.IsFalse(msg.HasErrors());
        }

        [Test]
        public void Null_Value()
        {
            var filter = new MapFilter()
            {
                Field = "source",
                Mapping = new List<object[]>()
                    {
                        new object[] {"a1", "b1"},
                        new object[] {"a2", "b2"},
                    },
                Destination = "field"
            };

            ExpandoObject msg = new ExpandoObject();
            msg.SetValue("source", "ax");

            msg = filter.Apply(msg);
            Assert.AreEqual("default", msg.GetValue("field"));
            Assert.IsFalse(msg.HasErrors());
        }

    }
}