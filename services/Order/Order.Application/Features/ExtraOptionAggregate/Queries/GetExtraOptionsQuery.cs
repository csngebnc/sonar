using MediatR;
using Order.Domain.Entities.ExtraOptionAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Features.ExtraOptionAggregate.Queries
{
    public class GetExtraOptionsQuery : IRequest<ExtraOptionsResponse>
    {
    }

    public class ExtraOptionsResponse
    {
        public List<ExtraOptionDto> Options { get; set; }

        public class ExtraOptionDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
        }
    }

    public class GetExtraOptionsQueryHandler : IRequestHandler<GetExtraOptionsQuery, ExtraOptionsResponse>
    {
        private readonly IExtraOptionRepository extraOptionRepository;

        public GetExtraOptionsQueryHandler(IExtraOptionRepository extraOptionRepository)
        {
            this.extraOptionRepository = extraOptionRepository;
        }

        public async Task<ExtraOptionsResponse> Handle(GetExtraOptionsQuery request, CancellationToken cancellationToken)
        {
            var options = await extraOptionRepository.GetAllListAsync();

            return new()
            {
                Options = options.Select(option => new ExtraOptionsResponse.ExtraOptionDto
                {
                    Id = option.Id,
                    Name = option.Name,
                    Price = option.Price
                }).ToList()
            };
        }
    }
}
