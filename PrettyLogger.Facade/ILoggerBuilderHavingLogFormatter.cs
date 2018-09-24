using PrettyLogger.Abstraction;

namespace PrettyLogger.Facade
{
    public interface ILoggerBuilderHavingLogFormatter
    {
        ILogFormatter StoredLogFormatter { get; }
    }
}