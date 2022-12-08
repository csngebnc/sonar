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
    public class RemoveTrackingNumberFromPartnerCommand : IRequest
    {
        public Guid PartnerId { get; set; }
        public string TrackingNumber { get; set; }
    }

    public class RemoveTrackingNumberFromPartnerCommandHandler : IRequestHandler<RemoveTrackingNumberFromPartnerCommand, Unit>
    {
        private readonly IDeliveryPartnerRepository deliveryPartnerRepository;
        private readonly IIdentityService identityService;

        public RemoveTrackingNumberFromPartnerCommandHandler(IDeliveryPartnerRepository deliveryPartnerRepository, IIdentityService identityService)
        {
            this.deliveryPartnerRepository = deliveryPartnerRepository;
            this.identityService = identityService;
        }

        public async Task<Unit> Handle(RemoveTrackingNumberFromPartnerCommand request, CancellationToken cancellationToken)
        {
            var partner = await deliveryPartnerRepository.SingleOrDefaultAsync(x => x.Id == identityService.GetCurrentUserId());

            partner.TrackingNumbers.Remove(partner.TrackingNumbers.Where(x => x.Id == request.TrackingNumber).Single());

            await deliveryPartnerRepository.UpdateAsync(partner);

            return Unit.Value;
        }
    }

    public class RemoveTrackingNumberFromPartnerCommandValidator : AbstractValidator<RemoveTrackingNumberFromPartnerCommand>
    {
        public RemoveTrackingNumberFromPartnerCommandValidator()
        {
            RuleFor(x => x.TrackingNumber)
                .NotEmpty();

            RuleFor(x => x.PartnerId)
                .NotEmpty();
        }
    }
}
