namespace PrettyLogger.Abstraction
{
    public interface ILogFormatter
    {
        ILog Format(LoggingArgument loggingArgument);
    }
}