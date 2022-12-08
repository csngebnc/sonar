using System;

namespace CSONGE.Messaging.Configuration
{
    public class MessagingConfiguration
    {
        public ConsumerErrorHandlingConfiguration ConsumerErrorHandling { get; set; }

        public class ConsumerErrorHandlingConfiguration
        {
            public int RetryCount { get; set; }
            public TimeSpan MinInterval { get; set; }
            public TimeSpan MaxInterval { get; set; }
            public TimeSpan ExponentialBaseIntervalDelta { get; set; }
        }
    }
}
