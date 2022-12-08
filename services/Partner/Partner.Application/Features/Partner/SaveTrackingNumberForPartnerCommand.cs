using FluentValidation;
using MediatR;
using Partner.Application.Interfaces;
using Partner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Partner.Application.Features.Partner
{
    public class SaveTrackingNumberForPartnerCommand : IRequest
    {
        public Guid PartnerId { get; set; }
        public string TrackingNumber { get; set; }
    }

    public class SaveTrackingNumberForPartnerCommandHandler : IRequestHandler<SaveTrackingNumberForPartnerCommand, Unit>
    {
        private readonly IDeliveryPartnerRepository deliveryPartnerRepository;
        private readonly IIdentityService identityService;

        public SaveTrackingNumberForPartnerCommandHandler(IDeliveryPartnerRepository deliveryPartnerRepository, IIdentityService identityService)
        {
            this.deliveryPartnerRepository = deliveryPartnerRepository;
            this.identityService = identityService;
        }

        public async Task<Unit> Handle(SaveTrackingNumberForPartnerCommand request, CancellationToken cancellationToken)
        {
            var partner = await deliveryPartnerRepository.SingleOrDefaultAsync(x => x.Id == identityService.GetCurrentUserId());

            partner.TrackingNumbers.Add(new()
            {
                Id = request.TrackingNumber,
            });

            await deliveryPartnerRepository.UpdateAsync(partner);

            return Unit.Value;
        }
    }

    public class SaveTrackingNumberForPartnerCommandValidator : AbstractValidator<SaveTrackingNumberForPartnerCommand>
    {
        public SaveTrackingNumberForPartnerCommandValidator()
        {
            RuleFor(x => x.TrackingNumber)
                .NotEmpty();

            RuleFor(x => x.PartnerId)
                .NotEmpty();
        }
    }
}
