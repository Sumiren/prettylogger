namespace PrettyLogger.Abstraction
{
    public interface ILog
    {
        void WriteTo(ILoggerImplementation loggerImplementation);
    }
}