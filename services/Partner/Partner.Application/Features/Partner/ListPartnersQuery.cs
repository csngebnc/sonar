using MediatR;
using Microsoft.EntityFrameworkCore;
using Partner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Partner.Application.Features.Partner
{
    public class ListPartnersQuery : IRequest<ListPartnerQueryResponse>
    {
    }

    public class ListPartnerQueryResponse
    {
        public List<PartnerDto> Partners { get; set; }

        public class PartnerDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string ContactPerson { get; set; }
            public string TaxNumber { get; set; }
        }
    }

    public class ListPartnersQueryHandler : IRequestHandler<ListPartnersQuery, ListPartnerQueryResponse>
    {
        private readonly IDeliveryPartnerRepository deliveryPartnerRepository;

        public ListPartnersQueryHandler(IDeliveryPartnerRepository deliveryPartnerRepository)
        {
            this.deliveryPartnerRepository = deliveryPartnerRepository;
        }

        public async Task<ListPartnerQueryResponse> Handle(ListPartnersQuery request, CancellationToken cancellationToken)
        {
            var partners = await deliveryPartnerRepository.GetAll().ToListAsync();

            return new()
            {
                Partners = partners.Select(partner => new ListPartnerQueryResponse.PartnerDto
                {
                    Id = partner.Id,
                    Name = partner.Name,
                    ContactPerson = partner.ContactPerson,
                    TaxNumber = partner.TaxNumber,
                }).ToList()
            };
        }
    }
}
