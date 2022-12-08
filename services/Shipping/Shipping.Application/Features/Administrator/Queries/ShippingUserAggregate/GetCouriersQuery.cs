using CSONGE.Application.Extensions;
using CSONGE.Application.Pagination;
using IdentityProvider.Domain;
using MediatR;
using Shipping.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Administrator.Queries.ShippingUserAggregate
{
    public class GetCouriersQuery : IRequest<IPagedList<CourierDto>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class CourierDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }

    public class GetCouriersQueryHandler : IRequestHandler<GetCouriersQuery, IPagedList<CourierDto>>
    {
        private readonly IShippingUserRepository shippingUserRepository;

        public GetCouriersQueryHandler(IShippingUserRepository shippingUserRepository)
        {
            this.shippingUserRepository = shippingUserRepository;
        }

        public async Task<IPagedList<CourierDto>> Handle(GetCouriersQuery request, CancellationToken cancellationToken)
        {
            var couriers = await shippingUserRepository
                .GetAll()
                .OrderBy(x => x.Username)
                .Where(x => x.Type == UserType.Courier)
                .ToPagedListAsync(request.PageIndex, request.PageSize);

            return new PagedList<CourierDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ItemCount = couriers.ItemCount,
                Items = couriers.Items.Select(x => new CourierDto
                {
                    Id = x.Id,
                    Username = x.Username,
                }).ToList(),
            };
        }
    }
}
