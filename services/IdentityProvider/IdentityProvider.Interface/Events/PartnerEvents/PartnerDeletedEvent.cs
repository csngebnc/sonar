using CSONGE.Messaging.Domain.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityProvider.Interface.Events.PartnerEvents
{
    public class PartnerDeletedEvent : IntegrationEventBase
    {
        public Guid PartnerId { get; set; }
    }
}
