using System.Collections.Generic;
using PrettyLogger.Abstraction;

namespace PrettyLogger.Mock
{
    public class MockLogFormatter : ILogFormatter
    {
        public List<IMockLog> FormattedLogs { get; } = new List<IMockLog>();

        public ILog Format(LoggingArgument loggingArgument)
        {
            var mockLog = new InnerMockLog(loggingArgument);
            FormattedLogs.Add(mockLog);
            return mockLog;
        }

        sealed class InnerMockLog : IMockLog
        {
            public InnerMockLog(LoggingArgument loggingArgument)
            {
                LoggingArgument = loggingArgument;
            }

            public LoggingArgument LoggingArgument { get; }

            public void WriteTo(ILoggerImplementation loggerImplementation)
            {
                loggerImplementation.Log(LoggingArgument.Message);
            }
        }
    }
}