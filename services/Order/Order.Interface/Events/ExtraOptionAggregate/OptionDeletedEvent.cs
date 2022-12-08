using CSONGE.Messaging.Domain.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Interface.Events.ExtraOptionAggregate
{
    public class OptionDeletedEvent : IntegrationEventBase
    {
        public Guid OptionId { get; set; }
    }
}
