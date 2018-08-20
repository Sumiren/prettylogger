using PrettyLogger.Abstraction;

namespace PrettyLogger.DefaultImplementation
{
    public class NullLog : ILog
    {
        public void WriteTo(ILoggerImplementation loggerImplementation)
        {
        }
    }
}