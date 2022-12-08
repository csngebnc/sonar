using CSONGE.Messaging.Domain.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityProvider.Interface.Events.PartnerEvents
{
    public class PartnerUpdatedEvent : IntegrationEventBase
    {
        public Guid PartnerId { get; set; }
        public string PartnerName { get; set; }
        public string PartnerEmail { get; set; }
    }
}
