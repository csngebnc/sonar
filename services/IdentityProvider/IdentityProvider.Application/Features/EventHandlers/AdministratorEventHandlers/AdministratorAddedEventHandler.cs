using IdentityProvider.Domain;
using IdentityProvider.Domain.Entities;
using IdentityProvider.Interface.Events.AdministratorEvents;
using IdentityProvider.Interface.Events.CourierEvents;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace IdentityProvider.Application.Features.EventHandlers.AdministratorEventHandlers
{
    public class AdministratorAddedEventHandler : IConsumer<AdministratorAddedEvent>
    {
        private readonly UserManager<ServiceUser> userManager;

        public AdministratorAddedEventHandler(UserManager<ServiceUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task Consume(ConsumeContext<AdministratorAddedEvent> context)
        {
            PasswordHasher<ServiceUser> passwordHasher = new PasswordHasher<ServiceUser>();

            var admin = new ServiceUser
            {
                Id = context.Message.AdminId,
                UserName = context.Message.Username,
                EmailConfirmed = true,
                NormalizedUserName = context.Message.Username.ToUpper(),
                Type = UserType.Administrator
            };

            admin.PasswordHash = passwordHasher.HashPassword(admin, "Aa1234.");

            var result = await userManager.CreateAsync(admin);

            Console.WriteLine(result);
        }
    }
}
