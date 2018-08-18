namespace PrettyLogger
{
    public interface IMockLog : ILog
    {
        LoggingArgument LoggingArgument { get; }
    }
}