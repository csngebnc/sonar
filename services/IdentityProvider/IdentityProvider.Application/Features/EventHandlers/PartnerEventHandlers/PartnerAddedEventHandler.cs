using IdentityProvider.Dal;
using IdentityProvider.Domain;
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
    public class PartnerAddedEventHandler : IConsumer<PartnerAddedEvent>
    {
        private readonly UserManager<ServiceUser> userManager;

        public PartnerAddedEventHandler(UserManager<ServiceUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task Consume(ConsumeContext<PartnerAddedEvent> context)
        {
            PasswordHasher<ServiceUser> passwordHasher = new PasswordHasher<ServiceUser>();

            var partner = new ServiceUser
            {
                Id = context.Message.PartnerId,
                UserName = context.Message.PartnerName,
                Email = context.Message.PartnerEmail,
                EmailConfirmed = true,
                NormalizedEmail = context.Message.PartnerEmail.ToUpper(),
                NormalizedUserName = context.Message.PartnerName.ToUpper(),
                Type = UserType.Partner
            };

            partner.PasswordHash = passwordHasher.HashPassword(partner, "Aa1234.");

            var result = await userManager.CreateAsync(partner);

            Console.WriteLine(result);
        }
    }
}
