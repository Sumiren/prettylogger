using System.Linq;
using NUnit.Framework;

namespace PrettyLogger.DefaultImplementation.Test
{
    public class InmemoryImplementationTest
    {
        [Test]
        public void TestLog()
        {
            var implemmentation = new InmemoryImplementation();
            implemmentation.Log("a");
            implemmentation.Log("i");
            Assert.That(implemmentation.Logs.Count,   Is.EqualTo(2));
            Assert.That(implemmentation.Logs.First(), Is.EqualTo("a"));
            Assert.That(implemmentation.Logs.Last(),  Is.EqualTo("i"));
        }
    }
}