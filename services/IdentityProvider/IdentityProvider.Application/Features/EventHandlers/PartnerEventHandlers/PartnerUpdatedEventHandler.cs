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
    public class PartnerUpdatedEventHandler : IConsumer<PartnerUpdatedEvent>
    {
        private readonly UserManager<ServiceUser> userManager;

        public PartnerUpdatedEventHandler(UserManager<ServiceUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task Consume(ConsumeContext<PartnerUpdatedEvent> context)
        {
            var partner = await userManager.FindByIdAsync(context.Message.PartnerId.ToString());

            partner.UserName = context.Message.PartnerName;
            partner.NormalizedUserName = partner.UserName.ToUpper();
            partner.Email = context.Message.PartnerEmail;
            partner.NormalizedEmail = partner.Email.ToUpper();

            await userManager.UpdateAsync(partner);
        }
    }
}
