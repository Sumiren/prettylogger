using System;
using NUnit.Framework;

namespace PrettyLogger
{
    public class MockTimestampGatewayTest
    {
        [Test]
        public void TestInitializeByDefault()
        {
            var mockTimestampGateway = new MockTimestampGateway();
            var before               = DateTime.Now;
            var timestamp            = mockTimestampGateway.PressTimestamp();
            var after                = DateTime.Now;
            Assert.That(timestamp, Is.GreaterThanOrEqualTo(before));
            Assert.That(timestamp, Is.LessThanOrEqualTo(after));
        }

        [Test]
        public void TestInitializeWithValue()
        {
            var dateTime             = new DateTime(2000, 1, 2, 3, 4, 5, 678);
            var mockTimestampGateway = new MockTimestampGateway(dateTime);
            var timestamp            = mockTimestampGateway.PressTimestamp();
            Assert.That(timestamp, Is.EqualTo(dateTime));
        }

        [Test]
        public void TestAccessedTime()
        {
            var mockTimestampGateway = new MockTimestampGateway();
            mockTimestampGateway.PressTimestamp();
            mockTimestampGateway.PressTimestamp();
            Assert.That(mockTimestampGateway.AccessedTimes, Is.EqualTo(2));
        }
    }
}