using IdentityProvider.Domain.Entities;
using IdentityProvider.Interface.Events.AdministratorEvents;
using IdentityProvider.Interface.Events.CourierEvents;
using IdentityProvider.Interface.Events.PartnerEvents;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityProvider.Application.Features.EventHandlers.AdministratorEventHandlers
{
    public class AdministratorDeletedEventHandler : IConsumer<AdministratorDeletedEvent>
    {
        private readonly UserManager<ServiceUser> userManager;

        public AdministratorDeletedEventHandler(UserManager<ServiceUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task Consume(ConsumeContext<AdministratorDeletedEvent> context)
        {
            var admin = await userManager.FindByIdAsync(context.Message.AdminId.ToString());

            await userManager.DeleteAsync(admin);
        }
    }
}
