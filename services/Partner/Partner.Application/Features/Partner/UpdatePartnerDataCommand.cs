using FluentValidation;
using IdentityProvider.Interface.Events.PartnerEvents;
using MassTransit;
using MediatR;
using Partner.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Partner.Application.Features.Partner
{
    public class UpdatePartnerDataCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string TaxNumber { get; set; }
        public string Email { get; set; }

    }

    public class UpdatePartnerDataCommandHandler : IRequestHandler<UpdatePartnerDataCommand, Unit>
    {
        private readonly IDeliveryPartnerRepository deliveryPartnerRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public UpdatePartnerDataCommandHandler(IDeliveryPartnerRepository deliveryPartnerRepository, IPublishEndpoint publishEndpoint)
        {
            this.deliveryPartnerRepository = deliveryPartnerRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(UpdatePartnerDataCommand request, CancellationToken cancellationToken)
        {
            var partner = await deliveryPartnerRepository.SingleOrDefaultAsync(x => x.Id == request.Id);

            partner.ContactPerson = request.ContactPerson;
            partner.Name = request.Name;
            partner.TaxNumber = request.TaxNumber;

            await deliveryPartnerRepository.UpdateAsync(partner);

            await publishEndpoint.Publish(new PartnerUpdatedEvent
            {
                PartnerId = partner.Id,
                PartnerName = request.Name,
                PartnerEmail = request.Email,
            });

            return Unit.Value;
        }
    }

    public class UpdatePartnerDataCommandValidator : AbstractValidator<UpdatePartnerDataCommand>
    {
        public UpdatePartnerDataCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull();

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.ContactPerson)
                .NotEmpty();

            RuleFor(x => x.TaxNumber)
                .NotEmpty();
        }
    }
}
