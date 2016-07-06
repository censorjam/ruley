using System;
using System.Dynamic;
using NUnit.Framework;
using Ruley.Core;
using Ruley.Core.Filters;

namespace Ruley.Tests
{
    [TestFixture]
    public class CountFilterTests
    {
        [Test]
        public void Simple_Count()
        {
            var filter = new CountFilter
            {
                Destination = "field",
            };

            Event msg = new Event();

            msg = filter.Apply(msg);
            Assert.AreEqual(1, msg.Data.GetValue("field"));

            msg = filter.Apply(msg);
            Assert.AreEqual(2, msg.Data.GetValue("field"));

            //Assert.IsFalse(msg.HasErrors());
        }

    }
}