using FluentValidation;
using IdentityProvider.Interface.Events.PartnerEvents;
using MassTransit;
using MediatR;
using Partner.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Partner.Application.Features.Partner
{
    public class SavePartnerCommand : IRequest
    {
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string TaxNumber { get; set; }
        public string Email { get; set; }
    }

    public class SavePartnerCommandHandler : IRequestHandler<SavePartnerCommand, Unit>
    {
        private readonly IDeliveryPartnerRepository deliveryPartnerRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public SavePartnerCommandHandler(IDeliveryPartnerRepository deliveryPartnerRepository, IPublishEndpoint publishEndpoint)
        {
            this.deliveryPartnerRepository = deliveryPartnerRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(SavePartnerCommand request, CancellationToken cancellationToken)
        {
            var partner = new DeliveryPartner
            {
                Name = request.Name,
                ContactPerson = request.ContactPerson,
                TaxNumber = request.TaxNumber,
            };

            await deliveryPartnerRepository.InsertAsync(partner);

            await publishEndpoint.Publish(new PartnerAddedEvent
            {
                PartnerId = partner.Id,
                PartnerName = request.Name,
                PartnerEmail = request.Email,
            });

            return Unit.Value;
        }
    }

    public class SavePartnerCommandValidator : AbstractValidator<SavePartnerCommand>
    {
        public SavePartnerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.ContactPerson)
                .NotEmpty();

            RuleFor(x => x.TaxNumber)
                .NotEmpty();
        }
    }
}
