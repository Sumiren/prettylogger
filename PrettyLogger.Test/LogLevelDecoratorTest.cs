using System;
using NUnit.Framework;
using PrettyLogger.Mock;

namespace PrettyLogger.Test
{
    public class LogLevelDecoratorTest
    {
        MockLogFormatter   _mockLogFormatter;
        SimpleTimestamp    _mockTimestamp;
        MockImplementation _mockImplementation;
        LogLevelDecorator  _logLevelDecorator;


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
            var log = _logLevelDecorator.Format(new LoggingArgument(LogLevel.Warning, _mockTimestamp, "aiueo"));
            log.WriteTo(_mockImplementation);
            _mockImplementation.AssertNotLogged();
        }
    }
}