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
    public class DeletePartnerContactCommand : IRequest
    {
        public Guid PartnerId { get; set; }
        public Guid ContactId { get; set; }
    }

    public class DeletePartnerContactCommandHandler : IRequestHandler<DeletePartnerContactCommand, Unit>
    {
        private readonly IDeliveryPartnerRepository deliveryPartnerRepository;
        private readonly IIdentityService identityService;

        public DeletePartnerContactCommandHandler(IDeliveryPartnerRepository deliveryPartnerRepository, IIdentityService identityService)
        {
            this.deliveryPartnerRepository = deliveryPartnerRepository;
            this.identityService = identityService;
        }

        public async Task<Unit> Handle(DeletePartnerContactCommand request, CancellationToken cancellationToken)
        {
            var partner = await deliveryPartnerRepository.SingleIncludingAsync(x => x.Id == identityService.GetCurrentUserId(), x => x.Contacts);

            partner.Contacts = partner.Contacts.Where(c => c.Id != request.ContactId).ToList();

            await deliveryPartnerRepository.UpdateAsync(partner);

            return Unit.Value;
        }
    }

    public class DeletePartnerContactCommandValidator : AbstractValidator<DeletePartnerContactCommand>
    {
        public DeletePartnerContactCommandValidator()
        {
            RuleFor(x => x.ContactId)
                .NotEmpty();

            RuleFor(x => x.PartnerId)
                .NotEmpty();
        }
    }
}
