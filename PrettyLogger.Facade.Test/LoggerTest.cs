using System;
using System.Linq;
using NUnit.Framework;
using PrettyLogger.Abstraction;
using PrettyLogger.Mock;

namespace PrettyLogger.Facade.Test
{
    public class LoggerTest
    {
        DateTime             _datetime;
        Logger               _logger;
        LoggerTester         _loggerTester;
        MockImplementation   _mockImplementation;
        MockLogFormatter     _mockLogFormatter;
        MockTimestampGateway _mockTimestampGateway;

        [SetUp]
        public void SetUp()
        {
            _datetime             = new DateTime(2000, 1, 2, 3, 4, 5, 678);
            _mockImplementation   = new MockImplementation();
            _mockLogFormatter     = new MockLogFormatter();
            _mockTimestampGateway = new MockTimestampGateway(_datetime);
            _logger               = new Logger(_mockLogFormatter, _mockImplementation, _mockTimestampGateway);
            _loggerTester         = new LoggerTester(_mockLogFormatter, _mockImplementation, _mockTimestampGateway);
        }

        [Test]
        public void TestError()
        {
            _logger.Error("aiueo");

            AssertInteractingWithImplementationCorrectly();
            AssertInteractingWithFormatterCorrectly(LogLevel.Error);
        }

        [Test]
        public void TestWarning()
        {
            _logger.Warn("aiueo");

            AssertInteractingWithImplementationCorrectly();
            AssertInteractingWithFormatterCorrectly(LogLevel.Warn);
        }

        [Test]
        public void TestInfo()
        {
            _logger.Info("aiueo");

            AssertInteractingWithImplementationCorrectly();
            AssertInteractingWithFormatterCorrectly(LogLevel.Info);
        }

        [Test]
        public void TestTrace()
        {
            _logger.Trace("aiueo");

            AssertInteractingWithImplementationCorrectly();
            AssertInteractingWithFormatterCorrectly(LogLevel.Trace);
        }

        [Test]
        public void TestError_CallFormatAndWrite()
        {
            _loggerTester.Error("aiueo");

            Assert.That(_loggerTester.FormatAndWriteCount, Is.EqualTo(1));
            // decided not to test arguments...it's clear if they were wrong the preceding tests would have failed.
        }

        [Test]
        public void TestWarn_CallFormatAndWrite()
        {
            _loggerTester.Warn("aiueo");

            Assert.That(_loggerTester.FormatAndWriteCount, Is.EqualTo(1));
        }

        [Test]
        public void TestInfo_CallFormatAndWrite()
        {
            _loggerTester.Info("aiueo");

            Assert.That(_loggerTester.FormatAndWriteCount, Is.EqualTo(1));
        }

        [Test]
        public void TestTrace_CallFormatAndWrite()
        {
            _loggerTester.Trace("aiueo");

            Assert.That(_loggerTester.FormatAndWriteCount, Is.EqualTo(1));
        }

        [Test]
        public void TestFormatAndWrite_CallFormatAndCallWrite()
        {
            _loggerTester.CallFormatAndWrite();

            Assert.That(_loggerTester.FormatCount, Is.EqualTo(1));
            Assert.That(_loggerTester.WriteCount,  Is.EqualTo(1));
            // decided not to test arguments...it's clear if they were wrong the preceding tests would have failed.
        }

        void AssertInteractingWithFormatterCorrectly(LogLevel expectedLogLevel)
        {
            Assert.That(_mockLogFormatter.FormattedLogs.Count, Is.EqualTo(1));
            var passedLoggingArgument = _mockLogFormatter.FormattedLogs.First().LoggingArgument;

            Assert.That(passedLoggingArgument.LogLevel,        Is.EqualTo(expectedLogLevel));
            Assert.That(passedLoggingArgument.Timestamp.Value, Is.EqualTo(_datetime));
            Assert.That(passedLoggingArgument.Message,         Is.EqualTo("aiueo"));
        }

        void AssertInteractingWithImplementationCorrectly()
        {
            _mockImplementation.AssertLoggedOnlyOnce("aiueo");
        }

        class LoggerTester : Logger
        {
            public LoggerTester(ILogFormatter logFormatter, ILoggerImplementation loggerImplementation, ITimestampGateway timestampGateway) : base(
                    logFormatter,
                    loggerImplementation,
                    timestampGateway)
            {
            }

            public int FormatAndWriteCount { get; private set; }

            public int WriteCount { get; private set; }

            public int FormatCount { get; private set; }

            public void CallFormatAndWrite()
            {
                FormatAndWrite("aiueo", LogLevel.Info);
            }

            protected override void FormatAndWrite(string message, LogLevel logLevel)
            {
                FormatAndWriteCount++;
                base.FormatAndWrite(message, logLevel);
            }

            protected override ILog Format(LogLevel logLevel, string message)
            {
                FormatCount++;
                return base.Format(logLevel, message);
            }

            protected override void Write(ILog log)
            {
                WriteCount++;
                base.Write(log);
            }
        }
    }
}