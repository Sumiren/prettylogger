using NUnit.Framework;
using PrettyLogger.Abstraction;

namespace PrettyLogger.DefaultImplementation.Test
{
    public class LogFormatterDecoratorTest
    {
        [Test]
        public void TestDecorated()
        {
            var decorated                   = new SomeLogFormatter();
            var logFormatterDecoratorTester = new LogFormatterDecoratorTester(decorated);
            Assert.That(logFormatterDecoratorTester.Decorated, Is.EqualTo(decorated));
        }

        [Test]
        public void InnerTest_TestersAreSane()
        {
            // I wanted to avoid being confused that coverage is 99%
            Assert.That(new SomeLogFormatter().Format(null), Is.Null);
            Assert.That(new LogFormatterDecoratorTester(new SomeLogFormatter()).Format(null), Is.Null);
        }

        class SomeLogFormatter : ILogFormatter
        {
            public ILog Format(LoggingArgument loggingArgument)
            {
                return null;
            }
        }

        class LogFormatterDecoratorTester : LogFormatterDecorator
        {
            public LogFormatterDecoratorTester(ILogFormatter decorated) : base(decorated)
            {
            }

            public override ILog Format(LoggingArgument loggingArgument)
            {
                return null;
            }
        }
    }
}