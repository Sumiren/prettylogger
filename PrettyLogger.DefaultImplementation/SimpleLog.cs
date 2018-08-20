using PrettyLogger.Abstraction;

namespace PrettyLogger.DefaultImplementation
{
    public class SimpleLog : ILog
    {
        readonly string _message;

        public SimpleLog(string message)
        {
            _message = message;
        }

        public void WriteTo(ILoggerImplementation loggerImplementation)
        {
            loggerImplementation.Log(_message);
        }
    }
}