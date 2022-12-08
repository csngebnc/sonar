using FluentValidation;
using MediatR;
using Partner.Application.Interfaces;
using Partner.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Partner.Application.Features.Partner
{
    public class SavePartnerContactCommand : IRequest
    {
        public Guid PartnerId { get; set; }
        public string Name { get; set; }
        public AddressData Address { get; set; }
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

    public class SavePartnerContactCommandHandler : IRequestHandler<SavePartnerContactCommand, Unit>
    {
        private readonly IDeliveryPartnerRepository deliveryPartnerRepository;
        private readonly IIdentityService identityService;

        public SavePartnerContactCommandHandler(IDeliveryPartnerRepository deliveryPartnerRepository, IIdentityService identityService)
        {
            this.deliveryPartnerRepository = deliveryPartnerRepository;
            this.identityService = identityService;
        }

        public async Task<Unit> Handle(SavePartnerContactCommand request, CancellationToken cancellationToken)
        {
            var partner = await deliveryPartnerRepository.SingleIncludingAsync(x => x.Id == identityService.GetCurrentUserId(), x => x.Contacts);

            var newContact = new Contact
            {
                Name = request.Name,
                PartnerId = request.PartnerId,
                CreationDate = DateTime.Now,
                Address = new()
                {
                    City = request.Address.City,
                    PostalCode = request.Address.PostalCode,
                    Country = request.Address.Country,
                    County = request.Address.County,
                    Other = request.Address.Other,
                    StreetAndNumber = request.Address.StreetAndNumber
                }
            };

            partner.Contacts.Add(newContact);

            await deliveryPartnerRepository.UpdateAsync(partner);

            return Unit.Value;
        }
    }

    public class SavePartnerContactCommandValidator : AbstractValidator<SavePartnerContactCommand>
    {
        public SavePartnerContactCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.CreationDate)
                .NotEmpty();

            RuleFor(x => x.Address.Country)
                .NotEmpty();

            RuleFor(x => x.Address.County)
                .NotEmpty();

            RuleFor(x => x.Address.PostalCode)
                .NotEmpty();

            RuleFor(x => x.Address.City)
                .NotEmpty();

            RuleFor(x => x.Address.StreetAndNumber)
                .NotEmpty();
        }
    }
}
