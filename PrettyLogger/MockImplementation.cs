using System.Linq;
using NUnit.Framework;

namespace PrettyLogger
{
    public class MockImplementation : InmemoryImplementation
    {
        public string LastLogged => Logs.Last();

        public void AssertLoggedOnlyOnce(string expected)
        {
            Assert.That(Logs.Count,  Is.EqualTo(1));
            Assert.That(Logs.Last(), Is.EqualTo(expected));
        }
    }
}