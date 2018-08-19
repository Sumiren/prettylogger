namespace PrettyLogger
{
    public class NullLog : ILog
    {
        public void WriteTo(ILoggerImplementation loggerImplementation)
        {
        }
    }
}