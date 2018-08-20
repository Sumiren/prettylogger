﻿using System;

namespace PrettyLogger
{
    public class LogFormatter : ILogFormatter
    {
        public ILog Format(LoggingArgument loggingArgument)
        {
            var timestamp = loggingArgument.Timestamp;
            var message = loggingArgument.Message;
            
            switch (loggingArgument.LogLevel)
            {
                case LogLevel.Error:
                    return FormatError(timestamp, message);

                case LogLevel.Warn:
                    return FormatWarn(timestamp, message);

                case LogLevel.Info:
                    return FormatInfo(timestamp, message);

                case LogLevel.Trace:
                    return FormatTrace(timestamp, message);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual SimpleLog FormatError(ITimestamp timestamp, string message)
        {
            return InnerFormat("ERROR", timestamp, message);
        }

        protected virtual SimpleLog FormatWarn(ITimestamp timestamp, string message)
        {
            return InnerFormat("WARN", timestamp, message);
        }

        protected virtual SimpleLog FormatInfo(ITimestamp timestamp, string message)
        {
            return InnerFormat("INFO", timestamp, message);
        }

        protected virtual SimpleLog FormatTrace(ITimestamp timestamp, string message)
        {
            return InnerFormat("TRACE", timestamp, message);
        }

        protected virtual SimpleLog InnerFormat(string logTypeLabel, ITimestamp timestamp, string message)
        {
            return new SimpleLog($"[{logTypeLabel}] {timestamp.Value:yyyy-MM-dd HH:mm:ss.fff} {message}");
        }
    }
}