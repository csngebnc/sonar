using IdentityProvider.Interface.Events.AdministratorEvents;
using MassTransit;
using MediatR;
using Shipping.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Administrator.Commands.ShippingUserAggregate
{
    public class UpdateAdminCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }

    public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, Unit>
    {
        private readonly IShippingUserRepository shippingUserRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public UpdateAdminCommandHandler(IShippingUserRepository shippingUserRepository, IPublishEndpoint publishEndpoint)
        {
            this.shippingUserRepository = shippingUserRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
        {
            var Admin = await shippingUserRepository.SingleAsync(x => x.Id == request.Id);

            Admin.Username = request.Username;

            await shippingUserRepository.UpdateAsync(Admin);
            await publishEndpoint.Publish(new AdministratorUpdatedEvent
            {
                AdminId = Admin.Id,
                Username = request.Username,
            });

            return Unit.Value;
        }
    }
}
