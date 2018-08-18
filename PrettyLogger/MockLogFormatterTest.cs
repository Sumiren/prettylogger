﻿using System;
using NUnit.Framework;

namespace PrettyLogger
{
    public class MockLogFormatterTest
    {
        [Test]
        public void TestFormat()
        {
            var implementation = new MockImplementation();
            var formatter      = new MockLogFormatter();
            var log            = formatter.Format(new LoggingArgument(LogType.Info, new SimpleTimestamp(new DateTime(2000, 1, 1)), "aiueo"));
            log.WriteTo(implementation);
            Assert.That(log, Is.InstanceOf<IMockLog>());
            var mockLog = (IMockLog) log;
            Assert.That(mockLog.LoggingArgument.LogType,         Is.EqualTo(LogType.Info));
            Assert.That(mockLog.LoggingArgument.Timestamp.Value, Is.EqualTo(new DateTime(2000, 1, 1)));
            Assert.That(mockLog.LoggingArgument.Message,         Is.EqualTo("aiueo"));
            implementation.AssertLoggedOnlyOnce("aiueo");
        }
    }
}