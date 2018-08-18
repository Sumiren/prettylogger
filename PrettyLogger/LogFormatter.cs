namespace PrettyLogger
{
    public class LogFormatter : ILogFormatter
    {
        public ILog Format(LoggingArgument loggingArgument)
        {
            var message   = loggingArgument.Message;
            var timestamp = loggingArgument.Timestamp;
            return FormatInfo(timestamp, message);
        }

        protected virtual SimpleLog FormatInfo(ITimestamp timestamp, string message)
        {
            return InnerFormat("INFO", timestamp, message);
        }

        protected virtual SimpleLog InnerFormat(string logTypeLabel, ITimestamp timestamp, string message)
        {
            return new SimpleLog($"[{logTypeLabel}] {timestamp.Value:yyyy-MM-dd HH:mm:ss.fff} {message}");
        }
    }
}