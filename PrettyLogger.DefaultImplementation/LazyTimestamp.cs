using System;
using PrettyLogger.Abstraction;

namespace PrettyLogger.DefaultImplementation
{
    public class LazyTimestamp : ITimestamp
    {
        readonly ITimestampGateway _timestampGateway;
        DateTime?                  _value;

        public LazyTimestamp(ITimestampGateway timestampGateway)
        {
            _timestampGateway = timestampGateway;
        }

        public DateTime Value
        {
            get
            {
                if (_value == null)
                    _value = _timestampGateway.PressTimestamp();

                return _value.Value;
            }
        }
    }
}