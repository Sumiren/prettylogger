using System;
using NUnit.Framework;

namespace PrettyLogger
{
    public class LazyTimestampTest
    {
        [Test]
        public void TestValue_UseServiceFirstTime()
        {
            var dateTime             = new DateTime(2000, 1, 2, 3, 4, 5, 678);
            var mockTimestampGateway = new MockTimestampGateway(dateTime);
            var lazyTimestamp        = new LazyTimestamp(mockTimestampGateway);
            Assert.That(mockTimestampGateway.AccessedTimes, Is.EqualTo(0));

            var timestampValue = lazyTimestamp.Value;

            Assert.That(mockTimestampGateway.AccessedTimes, Is.EqualTo(1));
            Assert.That(timestampValue,                     Is.EqualTo(dateTime));
        }

        [Test]
        public void TestValue_CacheResultOfService()
        {
            var dateTime             = new DateTime(2000, 1, 2, 3, 4, 5, 678);
            var mockTimestampGateway = new MockTimestampGateway(dateTime);
            var lazyTimestamp        = new LazyTimestamp(mockTimestampGateway);
            Assert.That(mockTimestampGateway.AccessedTimes, Is.EqualTo(0));

            // ReSharper disable once RedundantAssignment
            var timestampValue = lazyTimestamp.Value;
            timestampValue = lazyTimestamp.Value;

            Assert.That(mockTimestampGateway.AccessedTimes, Is.EqualTo(1));
            Assert.That(timestampValue,                     Is.EqualTo(dateTime));
        }
    }
}