using System;

namespace PrettyLogger.Abstraction
{
    public interface ITimestamp
    {
        DateTime Value { get; }
    }
}