using System;

namespace PrettyLogger
{
    public interface ITimestampGateway
    {
        DateTime PressTimestamp();
    }
}