using IdentityProvider.Interface.Events.CourierEvents;
using MassTransit;
using MediatR;
using Shipping.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Administrator.Commands.ShippingUserAggregate
{
    public class UpdateCourierCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }

    public class UpdateCourierCommandHandler : IRequestHandler<UpdateCourierCommand, Unit>
    {
        private readonly IShippingUserRepository shippingUserRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public UpdateCourierCommandHandler(IShippingUserRepository shippingUserRepository, IPublishEndpoint publishEndpoint)
        {
            this.shippingUserRepository = shippingUserRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(UpdateCourierCommand request, CancellationToken cancellationToken)
        {
            var courier = await shippingUserRepository.SingleAsync(x => x.Id == request.Id);

            courier.Username = request.Username;

            await shippingUserRepository.UpdateAsync(courier);
            await publishEndpoint.Publish(new CourierUpdatedEvent
            {
                CourierId = courier.Id,
                Username = request.Username,
            });

            return Unit.Value;
        }
    }
}
