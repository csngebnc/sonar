using MediatR;
using Microsoft.EntityFrameworkCore;
using Partner.Application.Interfaces;
using Partner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Partner.Application.Features.Partner
{
    public class ListContactsQuery : IRequest<ListContactsQueryResponse>
    {
        public Guid PartnerId { get; set; }
    }

    public class ListContactsQueryResponse
    {
        public List<ContactDto> Contacts { get; set; }

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
    }

    public class ListContactsQueryHandler : IRequestHandler<ListContactsQuery, ListContactsQueryResponse>
    {
        private readonly IContactRepository contactRepository;
        private readonly IIdentityService identityService;

        public ListContactsQueryHandler(IContactRepository contactRepository, IIdentityService identityService)
        {
            this.contactRepository = contactRepository;
            this.identityService = identityService;
        }

        public async Task<ListContactsQueryResponse> Handle(ListContactsQuery request, CancellationToken cancellationToken)
        {
            var contacts = await contactRepository
                .GetAll()
                .Where(x => x.PartnerId == identityService.GetCurrentUserId())
                .ToListAsync();

            return new()
            {
                Contacts = contacts.Select(contact => new ListContactsQueryResponse.ContactDto
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
                }).ToList()
            };
        }
    }
}
