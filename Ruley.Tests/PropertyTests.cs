using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Ruley.Core;

namespace Ruley.Tests
{
    [TestFixture]
    public class PropertyTests
    {
        [Test]
        public void Parses_Plain_String()
        {
            var x = (Property<string>)"test_string";
            Assert.AreEqual("test_string", x.Get(null));
        }
    }

    [TestFixture]
    public class ExpandoTests
    {
        [Test]
        public void Test()
        {
            //ExpandoObject target = new ExpandoObject();
            //target["test"] = "123";


        }
    }
}
