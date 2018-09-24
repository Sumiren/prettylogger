using PrettyLogger.Abstraction;

namespace PrettyLogger.Facade
{
    public interface ILoggerBuilderAcceptingTimestampGateway : ILoggerBuilderHavingLoggerImplementation
    {
        ICompleteLoggerBuilder SetTimestampGateway(ITimestampGateway timestampGateway);
    }
}