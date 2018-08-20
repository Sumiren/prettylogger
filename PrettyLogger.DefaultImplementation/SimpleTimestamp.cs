using System;
using PrettyLogger.Abstraction;

namespace PrettyLogger.DefaultImplementation
{
    public class SimpleTimestamp : ITimestamp
    {
        public SimpleTimestamp(DateTime dateTime)
        {
            Value = dateTime;
        }

        public DateTime Value { get; }
    }
}