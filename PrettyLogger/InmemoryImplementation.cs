using System.Collections.Generic;

namespace PrettyLogger
{
    public class InmemoryImplementation : ILoggerImplementation
    {
        public List<string> Logs { get; } = new List<string>();

        public void Log(string message)
        {
            Logs.Add(message);
        }
    }
}