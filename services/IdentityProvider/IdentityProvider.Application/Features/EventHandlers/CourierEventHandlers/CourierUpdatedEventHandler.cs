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

namespace IdentityProvider.Application.Features.EventHandlers.CourierEventHandlers
{
    public class CourierUpdatedEventHandler : IConsumer<CourierUpdatedEvent>
    {
        private readonly UserManager<ServiceUser> userManager;

        public CourierUpdatedEventHandler(UserManager<ServiceUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task Consume(ConsumeContext<CourierUpdatedEvent> context)
        {
            var co = await userManager.FindByIdAsync(context.Message.CourierId.ToString());

            co.UserName = context.Message.Username;
            co.NormalizedUserName = co.UserName.ToUpper();
            co.NormalizedEmail = co.Email.ToUpper();

            await userManager.UpdateAsync(co);
        }
    }
}
