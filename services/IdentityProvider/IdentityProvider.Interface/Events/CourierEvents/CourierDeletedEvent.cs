using CSONGE.Messaging.Domain.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityProvider.Interface.Events.CourierEvents
{
    public class CourierDeletedEvent : IntegrationEventBase
    {
        public Guid CourierId { get; set; }
    }
}
