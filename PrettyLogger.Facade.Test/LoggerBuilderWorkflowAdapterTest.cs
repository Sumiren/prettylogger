using System;
using System.Linq;
using NUnit.Framework;
using PrettyLogger.Abstraction;
using PrettyLogger.Mock;

namespace PrettyLogger.Facade.Test
{
    public class LoggerBuilderWorkflowAdapterTest
    {
        [Test]
        public void TestSetLogFormatter()
        {
            var mockLogFormatter = new MockLogFormatter();

            var emptyLoggerBuilder  = new LoggerBuilderWorkflowAdapter();
            var resultLoggerBuilder = emptyLoggerBuilder.SetLogFormatter(mockLogFormatter);
            Assert.That(resultLoggerBuilder.StoredLogFormatter, Is.EqualTo(mockLogFormatter));
        }

        [Test]
        public void TestSetLoggerImplementation()
        {
            var mockLogFormatter   = new MockLogFormatter();
            var mockImplementation = new MockImplementation();

            var emptyLoggerBuilder                        = new LoggerBuilderWorkflowAdapter();
            var loggerBuilderProvidedLogFormatter         = emptyLoggerBuilder.SetLogFormatter(mockLogFormatter);
            var loggerBuilderProvidedLoggerImplementation = loggerBuilderProvidedLogFormatter.SetLoggerImplementation(mockImplementation);
            Assert.That(loggerBuilderProvidedLoggerImplementation.StoredLogFormatter,         Is.EqualTo(mockLogFormatter));
            Assert.That(loggerBuilderProvidedLoggerImplementation.StoredLoggerImplementation, Is.EqualTo(mockImplementation));
        }

        [Test]
        public void TestSetTimestampGateway()
        {
            var mockLogFormatter     = new MockLogFormatter();
            var mockImplementation   = new MockImplementation();
            var mockTimestampGateway = new MockTimestampGateway();

            var emptyLoggerBuilder                        = new LoggerBuilderWorkflowAdapter();
            var loggerBuilderProvidedLogFormatter         = emptyLoggerBuilder.SetLogFormatter(mockLogFormatter);
            var loggerBuilderProvidedLoggerImplementation = loggerBuilderProvidedLogFormatter.SetLoggerImplementation(mockImplementation);
            var completeLoggerBuilder                     = loggerBuilderProvidedLoggerImplementation.SetTimestampGateway(mockTimestampGateway);
            Assert.That(completeLoggerBuilder.StoredLogFormatter,         Is.EqualTo(mockLogFormatter));
            Assert.That(completeLoggerBuilder.StoredLoggerImplementation, Is.EqualTo(mockImplementation));
            Assert.That(completeLoggerBuilder.StoredTimestampGateway,     Is.EqualTo(mockTimestampGateway));
        }

        [Test]
        public void TestBuildUp()
        {
            var emptyLoggerBuilder = new LoggerBuilderWorkflowAdapter();

            AssertCanBuildUpCorrectly(emptyLoggerBuilder);
        }

        public static void AssertCanBuildUpCorrectly(ILoggerBuilderAcceptingLogFormatter emptyLoggerBuilder)
        {
            var dateTime = new DateTime(2000, 1, 1);

            var mockLogFormatter     = new MockLogFormatter();
            var mockImplementation   = new MockImplementation();
            var mockTimestampGateway = new MockTimestampGateway(dateTime);

            var loggerBuilderProvidedLogFormatter         = emptyLoggerBuilder.SetLogFormatter(mockLogFormatter);
            var loggerBuilderProvidedLoggerImplementation = loggerBuilderProvidedLogFormatter.SetLoggerImplementation(mockImplementation);
            var completeLoggerBuilder                     = loggerBuilderProvidedLoggerImplementation.SetTimestampGateway(mockTimestampGateway);
            var logger                                    = completeLoggerBuilder.BuildUp();

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