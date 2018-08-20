using System;

namespace PrettyLogger.Abstraction
{
    public interface ITimestampGateway
    {
        DateTime PressTimestamp();
    }
}