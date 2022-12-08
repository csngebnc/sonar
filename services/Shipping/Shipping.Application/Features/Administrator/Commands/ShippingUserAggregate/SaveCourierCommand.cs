using IdentityProvider.Domain;
using IdentityProvider.Interface.Events.CourierEvents;
using MassTransit;
using MediatR;
using Shipping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Administrator.Commands.ShippingUserAggregate
{
    public class SaveCourierCommand : IRequest
    {
        public string Username { get; set; }
    }

    public class SaveCourierCommandHandler : IRequestHandler<SaveCourierCommand, Unit>
    {
        private readonly IShippingUserRepository shippingUserRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public SaveCourierCommandHandler(IShippingUserRepository shippingUserRepository, IPublishEndpoint publishEndpoint)
        {
            this.shippingUserRepository = shippingUserRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(SaveCourierCommand request, CancellationToken cancellationToken)
        {
            var courier = new ShippingUser
            {
                Username = request.Username,
                Type = UserType.Courier
            };

            await shippingUserRepository.InsertAsync(courier);
            await publishEndpoint.Publish(new CourierAddedEvent
            {
                CourierId = courier.Id,
                Username = request.Username,
            });

            return Unit.Value;
        }
    }
}
