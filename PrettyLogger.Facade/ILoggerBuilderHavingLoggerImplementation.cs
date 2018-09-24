using PrettyLogger.Abstraction;

namespace PrettyLogger.Facade
{
    public interface ILoggerBuilderHavingLoggerImplementation : ILoggerBuilderHavingLogFormatter
    {
        ILoggerImplementation StoredLoggerImplementation { get; }
    }
}