using NUnit.Framework;
using PrettyLogger.DefaultImplementation;
using PrettyLogger.Mock;

namespace PrettyLogger.Test
{
    public class NullLogTest
    {
        [Test]
        public void TestWriteTo_DoNothing()
        {
            var mockImplementation = new MockImplementation();
            var nullLog            = new NullLog();
            nullLog.WriteTo(mockImplementation);
            mockImplementation.AssertNotLogged();
        }
    }
}