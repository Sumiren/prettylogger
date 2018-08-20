using System;
using PrettyLogger.Abstraction;

namespace PrettyLogger.DefaultImplementation
{
    public class LocalTimestampGateway : ITimestampGateway
    {
        public DateTime PressTimestamp()
        {
            return DateTime.Now;
        }
    }
}