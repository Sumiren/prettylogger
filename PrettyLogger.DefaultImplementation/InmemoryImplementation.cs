using System.Collections.Generic;
using PrettyLogger.Abstraction;

namespace PrettyLogger.DefaultImplementation
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