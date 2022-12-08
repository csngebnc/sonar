using FluentValidation;
using MediatR;
using Partner.Application.Interfaces;
using Partner.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Partner.Application.Features.Partner
{
    public class UpdatePartnerContactCommand : IRequest
    {
        public Guid PartnerId { get; set; }
        public Guid ContactId { get; set; }
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

    public class UpdatePartnerContactCommandHandler : IRequestHandler<UpdatePartnerContactCommand, Unit>
    {
        private readonly IDeliveryPartnerRepository deliveryPartnerRepository;
        private readonly IIdentityService identityService;

        public UpdatePartnerContactCommandHandler(IDeliveryPartnerRepository deliveryPartnerRepository, IIdentityService identityService)
        {
            this.deliveryPartnerRepository = deliveryPartnerRepository;
            this.identityService = identityService;
        }

        public async Task<Unit> Handle(UpdatePartnerContactCommand request, CancellationToken cancellationToken)
        {
            var partner = await deliveryPartnerRepository.SingleIncludingAsync(x => x.Id == identityService.GetCurrentUserId(), x => x.Contacts);

            var contact = partner.Contacts.Where(c => c.Id == request.ContactId).Single();

            contact.Name = request.Name;
            contact.PartnerId = request.PartnerId;
            contact.CreationDate = DateTime.Now;
            contact.Address = new()
            {
                City = request.Address.City,
                PostalCode = request.Address.PostalCode,
                Country = request.Address.Country,
                County = request.Address.County,
                Other = request.Address.Other,
                StreetAndNumber = request.Address.StreetAndNumber
            };

            await deliveryPartnerRepository.UpdateAsync(partner);

            return Unit.Value;
        }
    }

    public class UpdatePartnerContactCommandValidator : AbstractValidator<UpdatePartnerContactCommand>
    {
        public UpdatePartnerContactCommandValidator()
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
