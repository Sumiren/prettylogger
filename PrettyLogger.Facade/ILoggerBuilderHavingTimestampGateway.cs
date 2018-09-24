using PrettyLogger.Abstraction;

namespace PrettyLogger.Facade
{
    public interface ILoggerBuilderHavingTimestampGateway : ILoggerBuilderHavingLoggerImplementation
    {
        ITimestampGateway StoredTimestampGateway { get; }
    }
}