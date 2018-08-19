using NUnit.Framework;

namespace PrettyLogger.Mock.Test
{
    public class MockImplementationTest
    {
        [Test]
        public void TestLastLogged()
        {
            var implementation = new MockImplementation();
            implementation.Log("aiueo");
            implementation.Log("kakikukeko");
            Assert.That(implementation.LastLogged, Is.EqualTo("kakikukeko"));
        }
    }
}