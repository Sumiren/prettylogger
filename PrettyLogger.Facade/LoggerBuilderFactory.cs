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

        public RawLoggerBuilder CreateRawLoggerBuilder()
        {
            return new RawLoggerBuilder();
        }

        public ILoggerBuilderAcceptingLogFormatter CreateLoggerBuilder()
        {
            return new LoggerBuilderWorkflowAdapter();
        }
    }
}