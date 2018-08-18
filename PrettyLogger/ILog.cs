namespace PrettyLogger
{
    public interface ILog
    {
        void WriteTo(ILoggerImplementation loggerImplementation);
    }
}