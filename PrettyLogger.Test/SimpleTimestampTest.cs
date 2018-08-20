using System;
using NUnit.Framework;
using PrettyLogger.DefaultImplementation;

namespace PrettyLogger.Test
{
    public class SimpleTimestampTest
    {
        [Test]
        public void TestValue()
        {
            var dateTime        = new DateTime(2000, 1, 1);
            var simpleTimestamp = new SimpleTimestamp(dateTime);
            Assert.That(simpleTimestamp.Value, Is.EqualTo(dateTime));
        }
    }
}