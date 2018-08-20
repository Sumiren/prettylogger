using NUnit.Framework;
using PrettyLogger.Mock;

namespace PrettyLogger.DefaultImplementation.Test
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