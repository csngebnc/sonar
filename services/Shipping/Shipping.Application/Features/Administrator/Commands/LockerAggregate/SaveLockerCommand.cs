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
    public class SaveLockerCommand : IRequest
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string StreetAndNumber { get; set; }
        public string Other { get; set; }
    }

    public class SaveLockerCommandHandler : IRequestHandler<SaveLockerCommand, Unit>
    {
        private readonly ILockerRepository lockerRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public SaveLockerCommandHandler(ILockerRepository lockerRepository, IPublishEndpoint publishEndpoint)
        {
            this.lockerRepository = lockerRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(SaveLockerCommand request, CancellationToken cancellationToken)
        {
            var locker = new Locker
            {
                Name = request.Name,
                Address = new()
                {
                    City = request.City,
                    PostalCode = request.PostalCode,
                    Country = request.Country,
                    County = request.County,
                    StreetAndNumber = request.StreetAndNumber,
                    Other = request.Other
                }
            };

            await lockerRepository.InsertAsync(locker);

            await publishEndpoint.Publish(new LockerAddedEvent
            {
                LockerId = locker.Id,
                Name = locker.Name,
                Address = new()
                {
                    City = locker.Address.City,
                    Country = locker.Address.Country,
                    County = locker.Address.County,
                    Other = locker.Address.Other,
                    PostalCode = locker.Address.PostalCode,
                    StreetAndNumber = locker.Address.StreetAndNumber
                }
            });

            return Unit.Value;
        }
    }
}
