using System;
using System.Linq;
using NUnit.Framework;
using PrettyLogger.Abstraction;
using PrettyLogger.DefaultImplementation;
using PrettyLogger.Mock;

namespace PrettyLogger.Facade.Test
{
    public class LoggerBuilderTest
    {
        [Test]
        public void TestSetFormatStrategy()
        {
            var logFormatter = new MockLogFormatter();
            var builder      = new LoggerBuilder();
            var newBuilder   = builder.SetLogFormatter(logFormatter);
            Assert.That(builder,                       Is.Not.EqualTo(newBuilder));
            Assert.That(newBuilder.StoredLogFormatter, Is.EqualTo(logFormatter));
            Assert.That(builder.StoredLogFormatter,    Is.EqualTo(null));
        }

        [Test]
        public void TestSetLoggerImplementation()
        {
            var loggerImplementation = new MockImplementation();
            var builder              = new LoggerBuilder();
            var newBuilder           = builder.SetLogFormatter(new LogFormatter()).SetLoggerImplementation(loggerImplementation);
            Assert.That(builder,                               Is.Not.EqualTo(newBuilder));
            Assert.That(newBuilder.StoredLoggerImplementation, Is.EqualTo(loggerImplementation));
            Assert.That(newBuilder.StoredLogFormatter,         Is.Not.Null);
            Assert.That(builder.StoredLoggerImplementation,    Is.EqualTo(null));
        }

        [Test]
        public void TestSetTimestampGateway()
        {
            var mockTimestampGateway = new MockTimestampGateway();
            var builder              = new LoggerBuilder();
            var newBuilder           = builder.SetLoggerImplementation(new MockImplementation()).SetTimestampGateway(mockTimestampGateway);
            Assert.That(builder,                               Is.Not.EqualTo(newBuilder));
            Assert.That(newBuilder.StoredTimestampGateway,     Is.EqualTo(mockTimestampGateway));
            Assert.That(newBuilder.StoredLoggerImplementation, Is.Not.Null);
            Assert.That(builder.StoredLoggerImplementation,    Is.EqualTo(null));
        }

        [Test]
        public void TestBuildUp()
        {
            var loggerBuilder = new LoggerBuilder();
            TestSimpleBuilder(loggerBuilder);
        }

        public static void TestSimpleBuilder(LoggerBuilder loggerBuilder)
        {
            var dateTime             = new DateTime(2000, 1, 1);
            var mockLogFormatter     = new MockLogFormatter();
            var mockImplementation   = new MockImplementation();
            var mockTimestampGateway = new MockTimestampGateway(dateTime);
            var logger = loggerBuilder.SetLogFormatter(mockLogFormatter)
                                      .SetLoggerImplementation(mockImplementation)
                                      .SetTimestampGateway(mockTimestampGateway)
                                      .BuildUp();

            logger.Info("aiueo");

            Assert.That(mockLogFormatter.FormattedLogs.Count,                                   Is.EqualTo(1));
            Assert.That(mockLogFormatter.FormattedLogs.First().LoggingArgument.Message,         Is.EqualTo("aiueo"));
            Assert.That(mockLogFormatter.FormattedLogs.First().LoggingArgument.Timestamp.Value, Is.EqualTo(dateTime));
            Assert.That(mockLogFormatter.FormattedLogs.First().LoggingArgument.LogLevel,        Is.EqualTo(LogLevel.Info));

            mockImplementation.AssertLoggedOnlyOnce("aiueo");

            Assert.That(mockTimestampGateway.AccessedTimes, Is.EqualTo(1));
        }
    }
}