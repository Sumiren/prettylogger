using PrettyLogger.Abstraction;

namespace PrettyLogger.DefaultImplementation
{
    public abstract class LogFormatterDecorator : ILogFormatter
    {
        public LogFormatterDecorator(ILogFormatter decorated)
        {
            Decorated = decorated;
        }

        public ILogFormatter Decorated { get; }

        public abstract ILog Format(LoggingArgument loggingArgument);
    }
}