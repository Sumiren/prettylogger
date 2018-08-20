namespace PrettyLogger
{
    public class LoggingArgument
    {
        public LoggingArgument(LogLevel logLevel, ITimestamp timestamp, string message)
        {
            LogLevel  = logLevel;
            Timestamp = timestamp;
            Message   = message;
        }

        public LogLevel   LogLevel  { get; }
        public ITimestamp Timestamp { get; }
        public string     Message   { get; }
    }
}