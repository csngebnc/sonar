using CSONGE.Application.Extensions;
using CSONGE.Application.Pagination;
using IdentityProvider.Domain;
using MediatR;
using Shipping.Application.Interfaces;
using Shipping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Administrator.Queries.ShippingUserAggregate
{
    public class GetAdministratorsQuery : IRequest<IPagedList<AdminDto>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class AdminDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }

    public class GetAdministratorsQueryHandler : IRequestHandler<GetAdministratorsQuery, IPagedList<AdminDto>>
    {
        private readonly IShippingUserRepository shippingUserRepository;
        private readonly IIdentityService identityService;

        public GetAdministratorsQueryHandler(IShippingUserRepository shippingUserRepository, IIdentityService identityService)
        {
            this.shippingUserRepository = shippingUserRepository;
            this.identityService = identityService;
        }

        public async Task<IPagedList<AdminDto>> Handle(GetAdministratorsQuery request, CancellationToken cancellationToken)
        {
            var couriers = await shippingUserRepository
                .GetAll()
                .OrderBy(x => x.Username)
                .Where(x => x.Type == UserType.Administrator && x.Id != identityService.GetCurrentUserId())
                .ToPagedListAsync(request.PageIndex, request.PageSize);

            return new PagedList<AdminDto>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                ItemCount = couriers.ItemCount,
                Items = couriers.Items.Select(x => new AdminDto
                {
                    Id = x.Id,
                    Username = x.Username,
                }).ToList(),
            };
        }
    }
}
