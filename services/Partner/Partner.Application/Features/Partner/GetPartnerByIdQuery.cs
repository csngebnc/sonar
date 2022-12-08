using MediatR;
using Partner.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Partner.Application.Features.Partner
{
    public class GetPartnerByIdQuery : IRequest<PartnerDto>
    {
        public Guid PartnerId { get; set; }
    }

    public class PartnerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string TaxNumber { get; set; }
    }

    public class GetPartnerByIdQueryHandler : IRequestHandler<GetPartnerByIdQuery, PartnerDto>
    {
        private readonly IDeliveryPartnerRepository deliveryPartnerRepository;

        public GetPartnerByIdQueryHandler(IDeliveryPartnerRepository deliveryPartnerRepository)
        {
            this.deliveryPartnerRepository = deliveryPartnerRepository;
        }

        public async Task<PartnerDto> Handle(GetPartnerByIdQuery request, CancellationToken cancellationToken)
        {
            var partner = await deliveryPartnerRepository.SingleAsync(x => x.Id == request.PartnerId);

            return new()
            {
                Id = partner.Id,
                Name = partner.Name,
                ContactPerson = partner.ContactPerson,
                TaxNumber = partner.TaxNumber,
            };
        }
    }
}