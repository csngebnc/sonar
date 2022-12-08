using IdentityProvider.Interface.Events.CourierEvents;
using MassTransit;
using MediatR;
using Shipping.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Administrator.Commands.ShippingUserAggregate
{
    public class DeleteCourierCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteCourierCommandHandler : IRequestHandler<DeleteCourierCommand, Unit>
    {
        private readonly IShippingUserRepository shippingUserRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public DeleteCourierCommandHandler(IShippingUserRepository shippingUserRepository, IPublishEndpoint publishEndpoint)
        {
            this.shippingUserRepository = shippingUserRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(DeleteCourierCommand request, CancellationToken cancellationToken)
        {
            var courier = await shippingUserRepository.SingleAsync(x => x.Id == request.Id);

            await shippingUserRepository.DeleteAsync(courier);
            await publishEndpoint.Publish(new CourierDeletedEvent
            {
                CourierId = request.Id
            });

            return Unit.Value;
        }
    }
}
