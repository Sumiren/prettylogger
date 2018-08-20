namespace PrettyLogger
{
    public class Logger : ILogger
    {
        readonly ILogFormatter         _logFormatter;
        readonly ILoggerImplementation _loggerImplementation;
        readonly ITimestampGateway     _timestampGateway;

        public Logger(ILogFormatter logFormatter, ILoggerImplementation loggerImplementation, ITimestampGateway timestampGateway)
        {
            _logFormatter         = logFormatter;
            _loggerImplementation = loggerImplementation;
            _timestampGateway     = timestampGateway;
        }

        public void Error(string message)
        {
            FormatAndWrite(message, LogLevel.Error);
        }

        public void Warn(string message)
        {
            FormatAndWrite(message, LogLevel.Warn);
        }

        public virtual void Info(string message)
        {
            FormatAndWrite(message, LogLevel.Info);
        }

        public void Trace(string message)
        {
            FormatAndWrite(message, LogLevel.Trace);
        }

        protected virtual void FormatAndWrite(string message, LogLevel logLevel)
        {
            var log = Format(logLevel, message);
            Write(log);
        }

        protected virtual ILog Format(LogLevel logLevel, string message)
        {
            return _logFormatter.Format(new LoggingArgument(logLevel, new LazyTimestamp(_timestampGateway), message));
        }

        protected virtual void Write(ILog log)
        {
            log.WriteTo(_loggerImplementation);
        }
    }
}