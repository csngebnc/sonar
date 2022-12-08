using IdentityProvider.Interface.Events.AdministratorEvents;
using MassTransit;
using MediatR;
using Shipping.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Administrator.Commands.ShippingUserAggregate
{
    public class DeleteAdminCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, Unit>
    {
        private readonly IShippingUserRepository shippingUserRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public DeleteAdminCommandHandler(IShippingUserRepository shippingUserRepository, IPublishEndpoint publishEndpoint)
        {
            this.shippingUserRepository = shippingUserRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            var Admin = await shippingUserRepository.SingleAsync(x => x.Id == request.Id);

            await shippingUserRepository.DeleteAsync(Admin);
            await publishEndpoint.Publish(new AdministratorDeletedEvent
            {
                AdminId = request.Id
            });

            return Unit.Value;
        }
    }
}
