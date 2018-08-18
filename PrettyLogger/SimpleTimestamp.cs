using System;

namespace PrettyLogger
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