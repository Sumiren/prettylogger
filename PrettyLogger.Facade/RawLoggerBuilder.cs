using System;
using PrettyLogger.Abstraction;

namespace PrettyLogger.Facade
{
    public class RawLoggerBuilder
    {
        public ILogFormatter         StoredLogFormatter         { get; private set; }
        public ILoggerImplementation StoredLoggerImplementation { get; private set; }
        public ITimestampGateway     StoredTimestampGateway     { get; private set; }


        public RawLoggerBuilder SetLogFormatter(ILogFormatter logFormatter)
        {
            return Clone(builder => builder.StoredLogFormatter = logFormatter);
        }

        public RawLoggerBuilder SetLoggerImplementation(ILoggerImplementation loggerImplementation)
        {
            return Clone(builder => builder.StoredLoggerImplementation = loggerImplementation);
        }

        public RawLoggerBuilder SetTimestampGateway(ITimestampGateway timestampGateway)
        {
            return Clone(builder => builder.StoredTimestampGateway = timestampGateway);
        }

        RawLoggerBuilder Clone(Action<RawLoggerBuilder> tune)
        {
            var loggerBuilder = new RawLoggerBuilder
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