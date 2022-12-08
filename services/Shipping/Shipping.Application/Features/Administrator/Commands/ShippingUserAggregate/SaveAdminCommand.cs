using IdentityProvider.Domain;
using IdentityProvider.Interface.Events.AdministratorEvents;
using MassTransit;
using MediatR;
using Shipping.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Administrator.Commands.ShippingUserAggregate
{
    public class SaveAdminCommand : IRequest
    {
        public string Username { get; set; }
    }

    public class SaveAdminCommandHandler : IRequestHandler<SaveAdminCommand, Unit>
    {
        private readonly IShippingUserRepository shippingUserRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public SaveAdminCommandHandler(IShippingUserRepository shippingUserRepository, IPublishEndpoint publishEndpoint)
        {
            this.shippingUserRepository = shippingUserRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(SaveAdminCommand request, CancellationToken cancellationToken)
        {
            var Admin = new ShippingUser
            {
                Username = request.Username,
                Type = UserType.Administrator
            };

            await shippingUserRepository.InsertAsync(Admin);
            await publishEndpoint.Publish(new AdministratorAddedEvent
            {
                AdminId = Admin.Id,
                Username = request.Username,
            });

            return Unit.Value;
        }
    }
}
