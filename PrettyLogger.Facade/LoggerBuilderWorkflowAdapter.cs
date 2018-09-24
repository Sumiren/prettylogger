using PrettyLogger.Abstraction;

namespace PrettyLogger.Facade
{
    public class LoggerBuilderWorkflowAdapter : ILoggerBuilderAcceptingLogFormatter, ILoggerBuilderAcceptingLoggerImplementation,
                                                ILoggerBuilderAcceptingTimestampGateway, ICompleteLoggerBuilder
    {
        readonly RawLoggerBuilder _innerRawLoggerBuilder;

        public LoggerBuilderWorkflowAdapter()
        {
            _innerRawLoggerBuilder = new RawLoggerBuilder();
        }

        LoggerBuilderWorkflowAdapter(RawLoggerBuilder innerRawLoggerBuilder)
        {
            _innerRawLoggerBuilder = innerRawLoggerBuilder;
        }

        public ITimestampGateway StoredTimestampGateway => _innerRawLoggerBuilder.StoredTimestampGateway;

        public ILogger BuildUp()
        {
            return _innerRawLoggerBuilder.BuildUp();
        }

        public ILoggerBuilderAcceptingLoggerImplementation SetLogFormatter(ILogFormatter logFormatter)
        {
            return new LoggerBuilderWorkflowAdapter(_innerRawLoggerBuilder.SetLogFormatter(logFormatter));
        }

        public ILoggerBuilderAcceptingTimestampGateway SetLoggerImplementation(ILoggerImplementation mockImplementation)
        {
            return new LoggerBuilderWorkflowAdapter(_innerRawLoggerBuilder.SetLoggerImplementation(mockImplementation));
        }

        public ILogFormatter         StoredLogFormatter         => _innerRawLoggerBuilder.StoredLogFormatter;
        public ILoggerImplementation StoredLoggerImplementation => _innerRawLoggerBuilder.StoredLoggerImplementation;

        public ICompleteLoggerBuilder SetTimestampGateway(ITimestampGateway timestampGateway)
        {
            return new LoggerBuilderWorkflowAdapter(_innerRawLoggerBuilder.SetTimestampGateway(timestampGateway));
        }
    }
}