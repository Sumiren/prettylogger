namespace PrettyLogger.Mock
{
    public class MockLogFormatter : ILogFormatter
    {
        public ILog Format(LoggingArgument loggingArgument)
        {
            return new InnerMockLog(loggingArgument);
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