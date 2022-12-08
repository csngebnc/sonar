using CSONGE.Messaging.Domain.Messages.Events;
using SharedKernel.Enums;
using System;

namespace Status.Interface.Events
{
    public class ParcelEvent : IntegrationEventBase
    {
        public ParcelState State { get; set; }
    }
}
