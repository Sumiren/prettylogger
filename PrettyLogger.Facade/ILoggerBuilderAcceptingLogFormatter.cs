using PrettyLogger.Abstraction;

namespace PrettyLogger.Facade
{
    public interface ILoggerBuilderAcceptingLogFormatter
    {
        ILoggerBuilderAcceptingLoggerImplementation SetLogFormatter(ILogFormatter logFormatter);
    }
}