using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PrettyLogger.Mock;

namespace PrettyLogger.Test
{
    public class LogFormatterTest
    {
        [Test]
        public void TestFormat_Info()
        {
            var implementation = new MockImplementation();
            var format         = new LogFormatter();

            var dateTime = DateTime.Parse("2018/1/2 01:02:03.456");
            var log      = format.Format(new LoggingArgument(LogLevel.Info, new SimpleTimestamp(dateTime), "takanagohan"));
            log.WriteTo(implementation);

            var lastLogged = implementation.LastLogged;
            var timeStamp = new DateTime(
                    int.Parse(lastLogged.Substring(7,  4)),
                    int.Parse(lastLogged.Substring(12, 2)),
                    int.Parse(lastLogged.Substring(15, 2)),
                    int.Parse(lastLogged.Substring(18, 2)),
                    int.Parse(lastLogged.Substring(21, 2)),
                    int.Parse(lastLogged.Substring(24, 2)),
                    int.Parse(lastLogged.Substring(27, 3)));

            Assert.That(lastLogged.Substring(0, 6),  Is.EqualTo("[INFO]"));
            Assert.That(lastLogged.Substring(6, 1),  Is.EqualTo(" "));
            Assert.That(timeStamp,                   Is.EqualTo(dateTime));
            Assert.That(lastLogged.Substring(30, 1), Is.EqualTo(" "));
            Assert.That(lastLogged.Substring(31),    Is.EqualTo("takanagohan"));
        }

        class LogFormatterTester : LogFormatter
        {
            List<Tuple<ITimestamp, string>> FormatInfoHistory { get; } = new List<Tuple<ITimestamp, string>>();

            List<Tuple<string, ITimestamp, string>> InnerFormatHistory { get; } = new List<Tuple<string, ITimestamp, string>>();

            [SetUp]
            public void SetUp()
            {
                FormatInfoHistory.Clear();
                InnerFormatHistory.Clear();
            }

            [Test]
            public void TestFormatInfo_CallInnerFormat()
            {
                FormatInfo(new SimpleTimestamp(new DateTime(2000, 1, 1)), "aiueo");
                Assert.That(InnerFormatHistory.Count,               Is.EqualTo(1));
                Assert.That(InnerFormatHistory.First().Item1,       Is.EqualTo("INFO"));
                Assert.That(InnerFormatHistory.First().Item2.Value, Is.EqualTo(new DateTime(2000, 1, 1)));
                Assert.That(InnerFormatHistory.First().Item3,       Is.EqualTo("aiueo"));
            }

            [Test]
            public void TestFormat_CallFormatInfoWhenInfo()
            {
                Format(new LoggingArgument(LogLevel.Info, new SimpleTimestamp(new DateTime(2000, 1, 1)), "aiueo"));
                Assert.That(FormatInfoHistory.Count,               Is.EqualTo(1));
                Assert.That(FormatInfoHistory.First().Item1.Value, Is.EqualTo(new DateTime(2000, 1, 1)));
                Assert.That(FormatInfoHistory.First().Item2,       Is.EqualTo("aiueo"));
            }

            protected override SimpleLog FormatInfo(ITimestamp timestamp, string message)
            {
                FormatInfoHistory.Add(Tuple.Create(timestamp, message));
                return base.FormatInfo(timestamp, message);
            }

            protected override SimpleLog InnerFormat(string logTypeLabel, ITimestamp timestamp, string message)
            {
                InnerFormatHistory.Add(Tuple.Create(logTypeLabel, timestamp, message));
                return base.InnerFormat(logTypeLabel, timestamp, message);
            }
        }
    }
}