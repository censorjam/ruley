using NUnit.Framework;
using Ruley.Dynamic;

namespace Ruley.Tests
{
    [TestFixture]
    public class DataBagTests
    {
        class TestObject
        {
            public string Prop1 { get; set; }
            public long Prop2 { get; set; }
        }

        class NestedTestObject
        {
            public string Prop1 { get; set; }
            public long Prop2 { get; set; }

            public TestObject Prop3 { get; set; }
        }

        [Test]
        public void Flat_From_Object()
        {
            dynamic db = DataBag.FromObject(new TestObject() {Prop1 = "abc", Prop2 = 100});

            Assert.AreEqual("abc", db["Prop1"]);
            Assert.AreEqual(100, db["Prop2"]);
            Assert.AreEqual("abc", db.Prop1);
            Assert.AreEqual(100, db.Prop2);
        }

        [Test]
        public void Nested_From_Object()
        {
            //dynamic db = DataBag.FromObject(new TestObject() { Prop1 = "abc", Prop2 = 100 });

            //Assert.AreEqual("abc", db["Prop1"]);
            //Assert.AreEqual(100, db["Prop2"]);
            //Assert.AreEqual("abc", db.Prop1);
            //Assert.AreEqual(100, db.Prop2);
        }
    }
}
