using MediatR;
using Order.Application.Features.ExtraOptionAggregate.Queries;
using Order.Domain.Entities.LockerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Features.LockerAggregate.Queries
{
    public class GetLockersQuery : IRequest<LockerResponse>
    {
    }

    public class LockerResponse
    {
        public List<LockerDto> Lockers { get; set; }

        public class LockerDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public AddressDto Address { get; set; }

            public class AddressDto
            {
                public string Country { get; set; }
                public string County { get; set; }
                public string City { get; set; }
                public string PostalCode { get; set; }
                public string StreetAndNumber { get; set; }
                public string Other { get; set; }
            }
        }
    }

    public class GetLockersQueryHandler : IRequestHandler<GetLockersQuery, LockerResponse>
    {
        private readonly ILockerRepository lockerRepository;

        public GetLockersQueryHandler(ILockerRepository lockerRepository)
        {
            this.lockerRepository = lockerRepository;
        }

        public async Task<LockerResponse> Handle(GetLockersQuery request, CancellationToken cancellationToken)
        {
            var lockers = await lockerRepository.GetAllListAsync();

            return new()
            {
                Lockers = lockers.Select(x => new LockerResponse.LockerDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = new()
                    {
                        City = x.Address.City,
                        Country = x.Address.Country,
                        County = x.Address.County,
                        Other = x.Address.Other,
                        PostalCode = x.Address.PostalCode,
                        StreetAndNumber = x.Address.StreetAndNumber,
                    }
                }).ToList()
            };
        }
    }
}
