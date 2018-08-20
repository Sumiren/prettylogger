namespace PrettyLogger.Facade
{
    public interface ILogger
    {
        void Error(string message);

        void Warn(string message);

        void Info(string message);

        void Trace(string message);
    }
}