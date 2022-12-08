using MassTransit;
using MediatR;
using Order.Interface.Events.ExtraOptionAggregate;
using Shipping.Domain.Entities.ExtraOptionAggregate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Administrator.Commands.ExtraOptionAggregate
{
    public class DeleteExtraOptionCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteExtraOptionCommandHandler : IRequestHandler<DeleteExtraOptionCommand, Unit>
    {
        private readonly IExtraOptionRepository extraOptionRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public DeleteExtraOptionCommandHandler(IExtraOptionRepository extraOptionRepository, IPublishEndpoint publishEndpoint)
        {
            this.extraOptionRepository = extraOptionRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(DeleteExtraOptionCommand request, CancellationToken cancellationToken)
        {
            var option = await extraOptionRepository.SingleAsync(x => x.Id == request.Id);

            option.IsDeleted = true;

            await extraOptionRepository.UpdateAsync(option);

            await publishEndpoint.Publish(new OptionDeletedEvent
            {
                OptionId = request.Id,
            });

            return Unit.Value;
        }
    }
}
