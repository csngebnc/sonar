using CSONGE.Messaging.Domain.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityProvider.Interface.Events.AdministratorEvents
{
    public class AdministratorDeletedEvent : IntegrationEventBase
    {
        public Guid AdminId { get; set; }
    }
}
