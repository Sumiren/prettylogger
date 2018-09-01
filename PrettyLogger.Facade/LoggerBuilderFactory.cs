namespace PrettyLogger.Facade
{
    public class LoggerBuilderFactory
    {
        static readonly object Locker = new object();

        static LoggerBuilderFactory _instance;
        public static LoggerBuilderFactory Instance
        {
            get
            {
                lock (Locker)
                {
                    if (_instance == null)
                        _instance = new LoggerBuilderFactory();
                }

                return _instance;
            }
        }

        public LoggerBuilder CreateLoggerBuilder()
        {
            return new LoggerBuilder();
        }
    }
}