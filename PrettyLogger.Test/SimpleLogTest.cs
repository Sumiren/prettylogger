using NUnit.Framework;
using PrettyLogger.Mock;

namespace PrettyLogger.Test
{
    public class SimpleLogTest
    {
        [Test]
        public void TestWriteTo()
        {
            var implementation = new MockImplementation();
            var simpleLog      = new SimpleLog("aiueo");
            simpleLog.WriteTo(implementation);
            implementation.AssertLoggedOnlyOnce("aiueo");
        }
    }
}