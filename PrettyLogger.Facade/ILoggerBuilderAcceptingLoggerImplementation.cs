using PrettyLogger.Abstraction;

namespace PrettyLogger.Facade
{
    public interface ILoggerBuilderAcceptingLoggerImplementation : ILoggerBuilderHavingLogFormatter
    {
        ILoggerBuilderAcceptingTimestampGateway SetLoggerImplementation(ILoggerImplementation mockImplementation);
    }
}