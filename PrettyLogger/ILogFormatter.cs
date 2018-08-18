namespace PrettyLogger
{
    public interface ILogFormatter
    {
        ILog Format(LoggingArgument loggingArgument);
    }
}