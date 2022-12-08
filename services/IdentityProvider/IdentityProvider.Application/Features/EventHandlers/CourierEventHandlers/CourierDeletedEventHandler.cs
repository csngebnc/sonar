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
    public class CourierDeletedEventHandler : IConsumer<CourierDeletedEvent>
    {
        private readonly UserManager<ServiceUser> userManager;

        public CourierDeletedEventHandler(UserManager<ServiceUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task Consume(ConsumeContext<CourierDeletedEvent> context)
        {
            var co = await userManager.FindByIdAsync(context.Message.CourierId.ToString());

            await userManager.DeleteAsync(co);
        }
    }
}
