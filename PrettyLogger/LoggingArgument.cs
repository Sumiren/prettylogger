namespace PrettyLogger
{
    public class LoggingArgument
    {
        public LoggingArgument(LogType logType, ITimestamp timestamp, string message)
        {
            LogType   = logType;
            Timestamp = timestamp;
            Message   = message;
        }

        public LogType    LogType   { get; }
        public ITimestamp Timestamp { get; }
        public string     Message   { get; }
    }
}