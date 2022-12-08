using IdentityProvider.Domain;
using IdentityProvider.Domain.Entities;
using IdentityProvider.Interface.Events.CourierEvents;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace IdentityProvider.Application.Features.EventHandlers.CourierEventHandlers
{
    public class CourierAddedEventHandler : IConsumer<CourierAddedEvent>
    {
        private readonly UserManager<ServiceUser> userManager;

        public CourierAddedEventHandler(UserManager<ServiceUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task Consume(ConsumeContext<CourierAddedEvent> context)
        {
            PasswordHasher<ServiceUser> passwordHasher = new PasswordHasher<ServiceUser>();

            var co = new ServiceUser
            {
                Id = context.Message.CourierId,
                UserName = context.Message.Username,
                EmailConfirmed = true,
                NormalizedUserName = context.Message.Username.ToUpper(),
                Type = UserType.Courier
            };

            co.PasswordHash = passwordHasher.HashPassword(co, "Aa1234.");

            var result = await userManager.CreateAsync(co);

            Console.WriteLine(result);
        }
    }
}
