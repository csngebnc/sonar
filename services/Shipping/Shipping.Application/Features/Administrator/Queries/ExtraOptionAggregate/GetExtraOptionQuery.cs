using CSONGE.Application.Extensions;
using CSONGE.Application.Pagination;
using MediatR;
using Shipping.Domain.Entities.ExtraOptionAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Administrator.Queries.ExtraOptionAggregate
{
    public class GetExtraOptionQuery : IRequest<IPagedList<OptionDto>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class OptionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public class GetExtraOptionQueryHandler : IRequestHandler<GetExtraOptionQuery, IPagedList<OptionDto>>
    {
        private readonly IExtraOptionRepository extraOptionRepository;

        public GetExtraOptionQueryHandler(IExtraOptionRepository extraOptionRepository)
        {
            this.extraOptionRepository = extraOptionRepository;
        }

        public async Task<IPagedList<OptionDto>> Handle(GetExtraOptionQuery request, CancellationToken cancellationToken)
        {
            var options = await extraOptionRepository
                .GetAll()
                .OrderBy(x => x.Name)
                .ToPagedListAsync(request.PageIndex, request.PageSize);

            return new PagedList<OptionDto>
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                ItemCount = options.ItemCount,
                Items = options.Items.Select(x => new OptionDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                }).ToList()
            };
        }
    }


}
