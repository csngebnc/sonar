using MediatR;
using Microsoft.EntityFrameworkCore;
using Partner.Application.Interfaces;
using Partner.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Partner.Application.Features.Partner
{
    public class GetContactById : IRequest<ContactDto>
    {
        public Guid ContactId { get; set; }
    }


    public class ContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AddressDataDto Address { get; set; }
        public DateTime CreationDate { get; set; }

        public class AddressDataDto
        {
            public string Country { get; set; }
            public string County { get; set; }
            public string City { get; set; }
            public string PostalCode { get; set; }
            public string StreetAndNumber { get; set; }
            public string Other { get; set; }
        }
    }

    public class GetContactByIdHandler : IRequestHandler<GetContactById, ContactDto>
    {
        private readonly IContactRepository contactRepository;
        private readonly IIdentityService identityService;

        public GetContactByIdHandler(IContactRepository contactRepository, IIdentityService identityService)
        {
            this.contactRepository = contactRepository;
            this.identityService = identityService;
        }

        public async Task<ContactDto> Handle(GetContactById request, CancellationToken cancellationToken)
        {
            var contact = await contactRepository
                .GetAll()
                .Where(x => x.PartnerId == identityService.GetCurrentUserId())
                .SingleAsync(c => c.Id == request.ContactId);

            return new()
            {
                Id = contact.Id,
                Name = contact.Name,
                Address = new()
                {
                    City = contact.Address.City,
                    Country = contact.Address.Country,
                    County = contact.Address.County,
                    Other = contact.Address.Other,
                    PostalCode = contact.Address.PostalCode,
                    StreetAndNumber = contact.Address.StreetAndNumber
                },
                CreationDate = contact.CreationDate,
            };
        }
    }
}
