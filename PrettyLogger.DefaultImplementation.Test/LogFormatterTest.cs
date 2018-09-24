using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PrettyLogger.Abstraction;
using PrettyLogger.Mock;

namespace PrettyLogger.DefaultImplementation.Test
{
    public class LogFormatterTest
    {
        DateTime           _datetime;
        LogFormatter       _logFormatter;
        MockImplementation _mockImplementation;

        [SetUp]
        public void SetUp()
        {
            _mockImplementation = new MockImplementation();
            _logFormatter       = new LogFormatter();
            _datetime           = DateTime.Parse("2018/1/2 01:02:03.456");
        }

        [Test]
        public void TestFormat_Error()
        {
            var log = _logFormatter.Format(new LoggingArgument(LogLevel.Error, new SimpleTimestamp(_datetime), "takanagohan"));
            log.WriteTo(_mockImplementation);

            AssertCorrectlyFormatted("ERROR");
        }

        [Test]
        public void TestFormat_Warn()
        {
            var log = _logFormatter.Format(new LoggingArgument(LogLevel.Warn, new SimpleTimestamp(_datetime), "takanagohan"));
            log.WriteTo(_mockImplementation);

            AssertCorrectlyFormatted("WARN");
        }

        [Test]
        public void TestFormat_Info()
        {
            var log = _logFormatter.Format(new LoggingArgument(LogLevel.Info, new SimpleTimestamp(_datetime), "takanagohan"));
            log.WriteTo(_mockImplementation);

            AssertCorrectlyFormatted("INFO");
        }

        [Test]
        public void TestFormat_Trace()
        {
            var log = _logFormatter.Format(new LoggingArgument(LogLevel.Trace, new SimpleTimestamp(_datetime), "takanagohan"));
            log.WriteTo(_mockImplementation);

            AssertCorrectlyFormatted("TRACE");
        }

        [Test]
        public void TestFormat_InputError()
        {
            var argumentOutOfRangeException =
                    Assert.Throws<ArgumentOutOfRangeException>(
                            () => _logFormatter.Format(new LoggingArgument((LogLevel) (-1), new SimpleTimestamp(_datetime), "takanagohan")));
            Assert.That(argumentOutOfRangeException.ParamName, Is.EqualTo("LogLevel"));
        }

        void AssertCorrectlyFormatted(string logLevelLabel)
        {
            var lengthOfLabel = logLevelLabel.Length;
            Assert.That(_mockImplementation.Logs.Count, Is.EqualTo(1));
            var            lastLogged           = _mockImplementation.LastLogged;
            Func<int, int> addedToLengthOfLabel = i => i + lengthOfLabel;
            var timeStamp = new DateTime(
                    int.Parse(lastLogged.Substring(addedToLengthOfLabel(3),  4)),
                    int.Parse(lastLogged.Substring(addedToLengthOfLabel(8),  2)),
                    int.Parse(lastLogged.Substring(addedToLengthOfLabel(11), 2)),
                    int.Parse(lastLogged.Substring(addedToLengthOfLabel(14), 2)),
                    int.Parse(lastLogged.Substring(addedToLengthOfLabel(17), 2)),
                    int.Parse(lastLogged.Substring(addedToLengthOfLabel(20), 2)),
                    int.Parse(lastLogged.Substring(addedToLengthOfLabel(23), 3)));

            Assert.That(lastLogged.Substring(0,                       addedToLengthOfLabel(2)), Is.EqualTo($"[{logLevelLabel}]"));
            Assert.That(lastLogged.Substring(addedToLengthOfLabel(2), 1),                       Is.EqualTo(" "));
            Assert.That(timeStamp,                                                              Is.EqualTo(_datetime));
            Assert.That(lastLogged.Substring(addedToLengthOfLabel(26), 1),                      Is.EqualTo(" "));
            Assert.That(lastLogged.Substring(addedToLengthOfLabel(27)),                         Is.EqualTo("takanagohan"));
        }

        class LogFormatterTester : LogFormatter
        {
            List<Tuple<ITimestamp, string>>         FormatInfoHistory  { get; } = new List<Tuple<ITimestamp, string>>();
            List<Tuple<ITimestamp, string>>         FormatTraceHistory { get; } = new List<Tuple<ITimestamp, string>>();
            List<Tuple<ITimestamp, string>>         FormatErrorHistory { get; } = new List<Tuple<ITimestamp, string>>();
            List<Tuple<ITimestamp, string>>         FormatWarnHistory  { get; } = new List<Tuple<ITimestamp, string>>();
            List<Tuple<string, ITimestamp, string>> InnerFormatHistory { get; } = new List<Tuple<string, ITimestamp, string>>();

            [SetUp]
            public void SetUp()
            {
                FormatErrorHistory.Clear();
                FormatWarnHistory.Clear();
                FormatInfoHistory.Clear();
                FormatTraceHistory.Clear();
                InnerFormatHistory.Clear();
            }

            [Test]
            public void TestFormat_CallFormatErrorWhenError()
            {
                AssertFormatCallsCorrespondingHook(LogLevel.Error, FormatErrorHistory);
            }

            [Test]
            public void TestFormatError_CallInnerFormat()
            {
                AssertHookCallsInnerFormat(FormatError, "ERROR");
            }

            [Test]
            public void TestFormat_CallFormatWarnWhenWarn()
            {
                AssertFormatCallsCorrespondingHook(LogLevel.Warn, FormatWarnHistory);
            }

            [Test]
            public void TestFormatWarn_CallInnerFormat()
            {
                AssertHookCallsInnerFormat(FormatWarn, "WARN");
            }

            [Test]
            public void TestFormat_CallFormatInfoWhenInfo()
            {
                AssertFormatCallsCorrespondingHook(LogLevel.Info, FormatInfoHistory);
            }

            [Test]
            public void TestFormatInfo_CallInnerFormat()
            {
                AssertHookCallsInnerFormat(FormatInfo, "INFO");
            }

            [Test]
            public void TestFormat_CallFormatTraceWhenTrace()
            {
                AssertFormatCallsCorrespondingHook(LogLevel.Trace, FormatTraceHistory);
            }

            [Test]
            public void TestFormatTrace_CallInnerFormat()
            {
                AssertHookCallsInnerFormat(FormatTrace, "TRACE");
            }


            void AssertHookCallsInnerFormat(Func<ITimestamp, string, SimpleLog> formatInfo, string expected)
            {
                formatInfo(new SimpleTimestamp(new DateTime(2000, 1, 1)), "aiueo");
                Assert.That(InnerFormatHistory.Count,               Is.EqualTo(1));
                Assert.That(InnerFormatHistory.First().Item1,       Is.EqualTo(expected));
                Assert.That(InnerFormatHistory.First().Item2.Value, Is.EqualTo(new DateTime(2000, 1, 1)));
                Assert.That(InnerFormatHistory.First().Item3,       Is.EqualTo("aiueo"));
            }

            void AssertFormatCallsCorrespondingHook(LogLevel logLevel, List<Tuple<ITimestamp, string>> operationHistory)
            {
                Format(new LoggingArgument(logLevel, new SimpleTimestamp(new DateTime(2000, 1, 1)), "aiueo"));
                Assert.That(operationHistory.Count,               Is.EqualTo(1));
                Assert.That(operationHistory.First().Item1.Value, Is.EqualTo(new DateTime(2000, 1, 1)));
                Assert.That(operationHistory.First().Item2,       Is.EqualTo("aiueo"));
            }

            protected override SimpleLog FormatError(ITimestamp timestamp, string message)
            {
                FormatErrorHistory.Add(Tuple.Create(timestamp, message));
                return base.FormatError(timestamp, message);
            }

            protected override SimpleLog FormatWarn(ITimestamp timestamp, string message)
            {
                FormatWarnHistory.Add(Tuple.Create(timestamp, message));
                return base.FormatWarn(timestamp, message);
            }

            protected override SimpleLog FormatInfo(ITimestamp timestamp, string message)
            {
                FormatInfoHistory.Add(Tuple.Create(timestamp, message));
                return base.FormatInfo(timestamp, message);
            }

            protected override SimpleLog FormatTrace(ITimestamp timestamp, string message)
            {
                FormatTraceHistory.Add(Tuple.Create(timestamp, message));
                return base.FormatTrace(timestamp, message);
            }

            protected override SimpleLog InnerFormat(string logTypeLabel, ITimestamp timestamp, string message)
            {
                InnerFormatHistory.Add(Tuple.Create(logTypeLabel, timestamp, message));
                return base.InnerFormat(logTypeLabel, timestamp, message);
            }
        }
    }
}