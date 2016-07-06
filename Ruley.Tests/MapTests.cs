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

            var msg = new Event();
            msg.Data.SetValue("source", "a1");

            msg = filter.Apply(msg);
            Assert.AreEqual("b1", msg.Data.GetValue("field"));
            //Assert.IsFalse(msg.HasErrors());
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

            Event msg = new Event();
            msg.Data.SetValue("source", "a3");

            msg = filter.Apply(msg);
            Assert.AreEqual("b3", msg.Data.GetValue("field"));
            //Assert.IsFalse(msg.HasErrors());
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

            Event msg = new Event();
            msg.Data.SetValue("source", 2);

            msg = filter.Apply(msg);
            Assert.AreEqual("two", msg.Data.GetValue("field"));
            //Assert.IsFalse(msg.HasErrors());
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

            Event msg = new Event();

            msg.Data.SetValue("source", true);
            msg = filter.Apply(msg);
            Assert.AreEqual("yes", msg.Data.GetValue("field"));

            msg.Data.SetValue("source", false);
            msg = filter.Apply(msg);
            Assert.AreEqual("no", msg.Data.GetValue("field"));
            //Assert.IsFalse(msg.HasErrors());
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

            Event msg = new Event();
            msg.Data.SetValue("source", "ax");

            msg = filter.Apply(msg);
            Assert.AreEqual("default", msg.Data.GetValue("field"));
            //Assert.IsFalse(msg.HasErrors());
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

            Event ev = new Event();
            ev.Data.SetValue("source", "ax");

            ev = filter.Apply(ev);
            Assert.AreEqual("default", ev.Data.GetValue("field"));
            //Assert.IsFalse(msg.HasErrors());
        }

    }
}