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