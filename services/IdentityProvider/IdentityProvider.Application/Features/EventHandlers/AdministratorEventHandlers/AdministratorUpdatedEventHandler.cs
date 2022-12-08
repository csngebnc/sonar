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
    public class AdministratorUpdatedEventHandler : IConsumer<AdministratorUpdatedEvent>
    {
        private readonly UserManager<ServiceUser> userManager;

        public AdministratorUpdatedEventHandler(UserManager<ServiceUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task Consume(ConsumeContext<AdministratorUpdatedEvent> context)
        {
            var admin = await userManager.FindByIdAsync(context.Message.AdminId.ToString());

            admin.UserName = context.Message.Username;
            admin.NormalizedUserName = admin.UserName.ToUpper();
            admin.NormalizedEmail = admin.Email.ToUpper();

            await userManager.UpdateAsync(admin);
        }
    }
}
