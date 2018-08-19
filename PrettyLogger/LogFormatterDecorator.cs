namespace PrettyLogger
{
    public abstract class LogFormatterDecorator : ILogFormatter
    {
        public ILogFormatter Decorated { get; }

        public LogFormatterDecorator(ILogFormatter decorated)
        {
            Decorated = decorated;
        }

        public abstract ILog Format(LoggingArgument loggingArgument);
    }
}