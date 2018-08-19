namespace PrettyLogger.Mock
{
    public interface IMockLog : ILog
    {
        LoggingArgument LoggingArgument { get; }
    }
}