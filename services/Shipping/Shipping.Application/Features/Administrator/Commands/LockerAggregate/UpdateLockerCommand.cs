using MassTransit;
using MediatR;
using Order.Interface.Events.LockerAggregate;
using Shipping.Domain.Entities.LockerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Administrator.Commands.LockerAggregate
{
    public class UpdateLockerCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetAndNumber { get; set; }
        public string Other { get; set; }
    }

    public class UpdateLockerCommandHandler : IRequestHandler<UpdateLockerCommand, Unit>
    {
        private readonly ILockerRepository lockerRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public UpdateLockerCommandHandler(ILockerRepository lockerRepository, IPublishEndpoint publishEndpoint)
        {
            this.lockerRepository = lockerRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(UpdateLockerCommand request, CancellationToken cancellationToken)
        {
            var locker = await lockerRepository.SingleAsync(x => x.Id == request.Id);

            locker.Name = request.Name;
            locker.Address.City = request.City;
            locker.Address.PostalCode = request.PostalCode;
            locker.Address.StreetAndNumber = request.StreetAndNumber;
            locker.Address.Other = request.Other;
            locker.Address.Country = request.Country;
            locker.Address.County = request.County;

            await lockerRepository.UpdateAsync(locker);

            await publishEndpoint.Publish(new LockerUpdatedEvent
            {
                LockerId = locker.Id,
                Name = locker.Name,
                Address = new()
                {
                    StreetAndNumber = locker.Address.StreetAndNumber,
                    City = locker.Address.City,
                    Country = locker.Address.Country,
                    County = locker.Address.County,
                    Other = locker.Address.Other,
                    PostalCode = locker.Address.PostalCode,
                }
            });

            return Unit.Value;
        }
    }
}
