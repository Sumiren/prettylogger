namespace PrettyLogger.Facade
{
    public interface ICompleteLoggerBuilder : ILoggerBuilderHavingTimestampGateway
    {
        ILogger BuildUp();
    }
}