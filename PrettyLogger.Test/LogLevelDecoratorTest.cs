using System;
using NUnit.Framework;
using PrettyLogger.Mock;

namespace PrettyLogger.Test
{
    public class LogLevelDecoratorTest
    {
        LogLevelDecorator  _logLevelDecorator;
        MockImplementation _mockImplementation;
        MockLogFormatter   _mockLogFormatter;
        SimpleTimestamp    _mockTimestamp;


        [SetUp]
        public void SetUp()
        {
            _mockLogFormatter   = new MockLogFormatter();
            _mockTimestamp      = new SimpleTimestamp(DateTime.Now);
            _mockImplementation = new MockImplementation();
            _logLevelDecorator  = new LogLevelDecorator(_mockLogFormatter, LogLevel.Info);
        }

        [Test]
        public void TestInfo_PassWhenLevelIsHigher()
        {
            var log = _logLevelDecorator.Format(new LoggingArgument(LogLevel.Trace, _mockTimestamp, "aiueo"));
            log.WriteTo(_mockImplementation);
            _mockImplementation.AssertLoggedOnlyOnce("aiueo");
        }

        [Test]
        public void TestFormat_PassWhenLevelIsEqual()
        {
            var log = _logLevelDecorator.Format(new LoggingArgument(LogLevel.Info, _mockTimestamp, "aiueo"));
            log.WriteTo(_mockImplementation);
            _mockImplementation.AssertLoggedOnlyOnce("aiueo");
        }

        [Test]
        public void TestInfo_FilterWhenLevelIsLower()
        {
            var log = _logLevelDecorator.Format(new LoggingArgument(LogLevel.Warn, _mockTimestamp, "aiueo"));
            log.WriteTo(_mockImplementation);
            _mockImplementation.AssertNotLogged();
        }
    }
}