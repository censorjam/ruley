using System.Collections.Generic;
using System.Dynamic;
using NUnit.Framework;
using Ruley.Core;
using Ruley.Core.Filters;
using Ruley.Dynamic;

namespace Ruley.Tests
{
    [TestFixture]
    public class MapTests
    {
        //add tests for regex
        //add test for ignorecase

        [Test]
        public void Single_Value_String_Map()
        {
            var filter = new MapFilter()
            {
                Value = "[source]",
                Map = new List<Mapping>()
                    { 
                        new Mapping() { Key = "a1", Value = DynamicDictionary.Create("{'field':'b1'}") }
                    }
            };

            var msg = new Event();
            msg.Data.SetValue("source", "a1");

            msg = filter.Apply(msg);
            Assert.AreEqual("b1", msg.Data["field"]);
        }

        [Test]
        public void Multi_Value_String_Map()
        {
            var filter = new MapFilter()
            {
                Value = "[source]",
                //Mapping = new List<object[]>()
                //    {
                //        new object[] {"a1", "b1"},
                //        new object[] {"a2", "b2"},
                //        new object[] {"a3", "b3"}
                //    },
                //Destination = "field"
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
                Value = "[source]",
                Map = new List<Mapping>()
                    { 
                        new Mapping() { Key = 1, Value = DynamicDictionary.Create("{'x':'one'}") },
                        new Mapping() { Key = 2, Value = DynamicDictionary.Create("{'x':'two'}") },
                        new Mapping() { Key = 3, Value = DynamicDictionary.Create("{'x':'three'}") }
                    }
            };

            Event msg = new Event();
            msg.Data.SetValue("source", 2);

            msg = filter.Apply(msg);
            Assert.AreEqual("two", msg.Data["x"]);
        }

        [Test]
        public void Boolean_Map()
        {
            var filter = new MapFilter()
            {
                Value = "[source]",
                Map = new List<Mapping>()
                    { 
                        new Mapping() { Key = true, Value = DynamicDictionary.Create("{'field':'yes'}") },
                        new Mapping() { Key = false, Value = DynamicDictionary.Create("{'field':'no'}") },
                    }
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
                Value = "[source]",
                //Mapping = new List<object[]>()
                //{
                //    new object[] {"a1", "b1"},
                //    new object[] {"a2", "b2"},
                //},
                //Destination = "field",
                //Default = "default"
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
                Value = "[source]",
                //Mapping = new List<object[]>()
                //    {
                //        new object[] {"a1", "b1"},
                //        new object[] {"a2", "b2"},
                //    },
                //Destination = "field"
            };

            Event ev = new Event();
            ev.Data.SetValue("source", "ax");

            ev = filter.Apply(ev);
            Assert.AreEqual("default", ev.Data.GetValue("field"));
            //Assert.IsFalse(msg.HasErrors());
        }

    }
}