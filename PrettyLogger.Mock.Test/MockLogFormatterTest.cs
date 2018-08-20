using System;
using System.Linq;
using NUnit.Framework;

namespace PrettyLogger.Mock.Test
{
    public class MockLogFormatterTest
    {
        [Test]
        public void TestFormat()
        {
            var implementation = new MockImplementation();
            var formatter      = new MockLogFormatter();
            var log            = formatter.Format(new LoggingArgument(LogLevel.Info, new SimpleTimestamp(new DateTime(2000, 1, 1)), "aiueo"));
            log.WriteTo(implementation);
            Assert.That(log, Is.InstanceOf<IMockLog>());
            var mockLog = (IMockLog) log;
            Assert.That(mockLog.LoggingArgument.LogLevel,        Is.EqualTo(LogLevel.Info));
            Assert.That(mockLog.LoggingArgument.Timestamp.Value, Is.EqualTo(new DateTime(2000, 1, 1)));
            Assert.That(mockLog.LoggingArgument.Message,         Is.EqualTo("aiueo"));
            implementation.AssertLoggedOnlyOnce("aiueo");
        }

        [Test]
        public void TestFormattedLogs()
        {
            var formatter = new MockLogFormatter();
            var first = formatter.Format(new LoggingArgument(LogLevel.Info, new SimpleTimestamp(new DateTime(2000, 1, 1)), "aiueo"));
            var last = formatter.Format(new LoggingArgument(LogLevel.Error, new SimpleTimestamp(new DateTime(2000, 1, 1)), "aiueo"));
            Assert.That(formatter.FormattedLogs.Count, Is.EqualTo(2));
            Assert.That(formatter.FormattedLogs.First(), Is.EqualTo(first));
            Assert.That(formatter.FormattedLogs.Last(), Is.EqualTo(last));
        }
    }
}