using IdentityProvider.Domain.Entities;
using IdentityProvider.Interface.Events.CourierEvents;
using IdentityProvider.Interface.Events.PartnerEvents;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityProvider.Application.Features.EventHandlers.PartnerEventHandlers
{
    public class PartnerDeletedEventHandler : IConsumer<PartnerDeletedEvent>
    {
        private readonly UserManager<ServiceUser> userManager;

        public PartnerDeletedEventHandler(UserManager<ServiceUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task Consume(ConsumeContext<PartnerDeletedEvent> context)
        {
            var partner = await userManager.FindByIdAsync(context.Message.PartnerId.ToString());

            await userManager.DeleteAsync(partner);
        }
    }
}
