using System;

namespace PrettyLogger
{
    public class LocalTimestampGateway : ITimestampGateway
    {
        public DateTime PressTimestamp()
        {
            return DateTime.Now;
        }
    }
}