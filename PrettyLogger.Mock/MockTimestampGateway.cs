using System;

namespace PrettyLogger.Mock
{
    public class MockTimestampGateway : ITimestampGateway
    {
        readonly DateTime? _specifiedDateTime;

        public MockTimestampGateway(DateTime dateTime)
        {
            _specifiedDateTime = dateTime;
        }


        public MockTimestampGateway()
        {
        }

        public int AccessedTimes { get; private set; }

        public DateTime PressTimestamp()
        {
            AccessedTimes++;
            return _specifiedDateTime ?? DateTime.Now;
        }
    }
}