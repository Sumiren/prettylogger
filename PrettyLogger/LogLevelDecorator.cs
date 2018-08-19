namespace PrettyLogger
{
    public class LogLevelDecorator : LogFormatterDecorator
    {
        readonly LogLevel _maxLogLevelPassed;

        public LogLevelDecorator(ILogFormatter decorated, LogLevel maxLogLevelPassed) : base(decorated)
        {
            _maxLogLevelPassed = maxLogLevelPassed;
        }

        public override ILog Format(LoggingArgument loggingArgument)
        {
            return loggingArgument.LogLevel < _maxLogLevelPassed ? new NullLog() : Decorated.Format(loggingArgument);
        }
    }
}