using CSONGE.Messaging.Domain.Messages.Events;
using Order.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Interface.Events.LockerAggregate
{
    public class LockerAddedEvent : IntegrationEventBase
    {
        public Guid LockerId { get; set; }
        public string Name { get; set; }
        public AddressData Address { get; set; }
    }
}
