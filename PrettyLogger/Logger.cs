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

        public virtual void Info(string message)
        {
            var log = _logFormatter.Format(new LoggingArgument(LogType.Info, new LazyTimestamp(_timestampGateway), message));
            Write(log);
        }

        protected virtual void Write(ILog log)
        {
            log.WriteTo(_loggerImplementation);
        }
    }
}