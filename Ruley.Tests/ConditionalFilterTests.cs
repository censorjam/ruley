using System.Dynamic;
using NUnit.Framework;
using Ruley.Core;
using Ruley.Core.Filters;

namespace Ruley.Tests
{
    [TestFixture]
    public class ConditionalFilterTests
    {
        [Test]
        public void Equality_True_With_Direct_Value()
        {
            var filter = new ConditionalFilter
            {
                Field = "sourcefield",
                Level = 10,
                Destination = "destfield",
                Match = "="
            };

            ExpandoObject msg = new ExpandoObject();
            msg.SetValue("sourcefield", 10);
            msg = filter.Apply(msg);
            
            Assert.IsTrue((bool)msg.GetValue("destfield"));
            Assert.IsFalse(msg.HasErrors());
        }

        [Test]
        public void Equality_False_With_Direct_Value()
        {
            var filter = new ConditionalFilter
            {
                Field = "sourcefield",
                Level = 10,
                Destination = "destfield",
                Match = "!="
            };

            ExpandoObject msg = new ExpandoObject();
            msg.SetValue("sourcefield", 10);
            msg = filter.Apply(msg);

            Assert.IsFalse((bool)msg.GetValue("destfield"));
            Assert.IsFalse(msg.HasErrors());
        }

        [Test]
        public void Equality_With_Float_And_Long()
        {
            var filter = new ConditionalFilter
            {
                Field = "sourcefield",
                Level = 10.0f,
                Destination = "destfield",
                Match = "="
            };

            ExpandoObject msg = new ExpandoObject();
            msg.SetValue("sourcefield", 10L);
            msg = filter.Apply(msg);
            
            Assert.IsTrue((bool)msg.GetValue("destfield"));
            Assert.IsFalse(msg.HasErrors());
        }

        [Test]
        public void Dynamic_Match_Field()
        {
            var filter = new ConditionalFilter
            {
                Field = "sourcefield",
                Level = 5.0f,
                Destination = "destfield",
                Match = "{matchfield}"
            };

            ExpandoObject msg = new ExpandoObject();
            msg.SetValue("sourcefield", 10);
            msg.SetValue("matchfield", ">");
            msg = filter.Apply(msg);

            Assert.IsTrue((bool)msg.GetValue("destfield"));

            msg.SetValue("matchfield", "<");
            msg = filter.Apply(msg);
            
            Assert.IsFalse((bool)msg.GetValue("destfield"));
            Assert.IsFalse(msg.HasErrors());
        }
    }
}