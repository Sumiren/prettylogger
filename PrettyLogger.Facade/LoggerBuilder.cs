using System;
using PrettyLogger.Abstraction;

namespace PrettyLogger.Facade
{
    public class LoggerBuilder
    {
        public ILogFormatter         StoredLogFormatter         { get; private set; }
        public ILoggerImplementation StoredLoggerImplementation { get; private set; }
        public ITimestampGateway     StoredTimestampGateway     { get; private set; }


        public LoggerBuilder SetLogFormatter(ILogFormatter logFormatter)
        {
            return Clone(builder => builder.StoredLogFormatter = logFormatter);
        }

        public LoggerBuilder SetLoggerImplementation(ILoggerImplementation loggerImplementation)
        {
            return Clone(builder => builder.StoredLoggerImplementation = loggerImplementation);
        }

        public LoggerBuilder SetTimestampGateway(ITimestampGateway timestampGateway)
        {
            return Clone(builder => builder.StoredTimestampGateway = timestampGateway);
        }

        LoggerBuilder Clone(Action<LoggerBuilder> tune)
        {
            var loggerBuilder = new LoggerBuilder
            {
                    StoredLogFormatter         = StoredLogFormatter,
                    StoredLoggerImplementation = StoredLoggerImplementation,
                    StoredTimestampGateway     = StoredTimestampGateway
            };
            tune(loggerBuilder);
            return loggerBuilder;
        }

        public ILogger BuildUp()
        {
            return new Logger(StoredLogFormatter, StoredLoggerImplementation, StoredTimestampGateway);
        }
    }
}