using CSONGE.Application.Extensions;
using CSONGE.Application.Pagination;
using MediatR;
using Shipping.Domain.Entities.LockerAggregate;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Administrator.Queries.LockerAggregate
{
    public class GetLockersQuery : IRequest<IPagedList<LockerDto>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

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

    public class GetLockersQueryHandler : IRequestHandler<GetLockersQuery, IPagedList<LockerDto>>
    {
        private readonly ILockerRepository lockerRepository;

        public GetLockersQueryHandler(ILockerRepository lockerRepository)
        {
            this.lockerRepository = lockerRepository;
        }

        public async Task<IPagedList<LockerDto>> Handle(GetLockersQuery request, CancellationToken cancellationToken)
        {
            var lockers = await lockerRepository
                .GetAll()
                .OrderBy(x => x.Name)
                .ToPagedListAsync(request.PageIndex, request.PageSize);

            return new PagedList<LockerDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ItemCount = lockers.ItemCount,
                Items = lockers.Items.Select(x => new LockerDto
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
