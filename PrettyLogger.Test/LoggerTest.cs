using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using PrettyLogger.Mock;

namespace PrettyLogger.Test
{
    public class LoggerTest
    {
        [Test]
        public void TestInfo()
        {
            var dateTime = new DateTime(2000, 1, 2, 3, 4, 5, 678);

            var implementation = new MockImplementation();
            var formatter      = new MockLogFormatter();
            var gateway        = new MockTimestampGateway(dateTime);
            var logger         = new LoggerTester(formatter, implementation, gateway);

            logger.Info("aiueo");

            Assert.That(logger.WrittenLogs.Count, Is.EqualTo(1));
            var loggedArgument = ((IMockLog) logger.WrittenLogs.First()).LoggingArgument;

            Assert.That(loggedArgument.LogType,         Is.EqualTo(LogType.Info));
            Assert.That(loggedArgument.Timestamp.Value, Is.EqualTo(dateTime));
            Assert.That(loggedArgument.Message,         Is.EqualTo("aiueo"));

            implementation.AssertLoggedOnlyOnce("aiueo");
        }

        class LoggerTester : Logger
        {
            readonly List<ILog> _writtenLogs = new List<ILog>();

            public LoggerTester(ILogFormatter logFormatter, ILoggerImplementation loggerImplementation, ITimestampGateway timestampGateway) : base(
                    logFormatter,
                    loggerImplementation,
                    timestampGateway)
            {
            }

            public ReadOnlyCollection<ILog> WrittenLogs => _writtenLogs.AsReadOnly();

            protected override void Write(ILog log)
            {
                _writtenLogs.Add(log);
                base.Write(log);
            }
        }
    }
}