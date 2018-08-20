﻿using System;
using NUnit.Framework;
using PrettyLogger.DefaultImplementation;

namespace PrettyLogger.Test
{
    public class LocalTimestampGatewayTest
    {
        [Test]
        public void TestPressTimestamp()
        {
            var localTimestampGateway = new LocalTimestampGateway();
            var before                = DateTime.Now;
            var timestamp             = localTimestampGateway.PressTimestamp();
            var after                 = DateTime.Now;
            Assert.That(timestamp, Is.GreaterThanOrEqualTo(before));
            Assert.That(timestamp, Is.LessThanOrEqualTo(after));
        }
    }
}