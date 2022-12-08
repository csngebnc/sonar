using IdentityProvider.Interface.Events.PartnerEvents;
using MassTransit;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Partner.Application.Features.Partner
{
    public class RemovePartnerCommand : IRequest
    {
        public Guid PartnerId { get; set; }
    }

    public class RemovePartnerCommandHandler : IRequestHandler<RemovePartnerCommand>
    {
        private readonly IPublishEndpoint publishEndpoint;

        public RemovePartnerCommandHandler(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(RemovePartnerCommand request, CancellationToken cancellationToken)
        { 
            await publishEndpoint.Publish(new PartnerDeletedEvent
            {
                PartnerId = request.PartnerId,
            });

            return Unit.Value;
        }
    }
}
