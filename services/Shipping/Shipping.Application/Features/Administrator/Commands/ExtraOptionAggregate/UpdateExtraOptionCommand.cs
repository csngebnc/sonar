using MassTransit;
using MediatR;
using Order.Interface.Events.ExtraOptionAggregate;
using Shipping.Domain.Entities.ExtraOptionAggregate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Administrator.Commands.ExtraOptionAggregate
{
    public class UpdateExtraOptionCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public class UpdateExtraOptionCommandHandler : IRequestHandler<UpdateExtraOptionCommand, Unit>
    {
        private readonly IExtraOptionRepository extraOptionRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public UpdateExtraOptionCommandHandler(IExtraOptionRepository extraOptionRepository, IPublishEndpoint publishEndpoint)
        {
            this.extraOptionRepository = extraOptionRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(UpdateExtraOptionCommand request, CancellationToken cancellationToken)
        {
            var option = await extraOptionRepository.SingleAsync(x => x.Id == request.Id);

            option.Name = request.Name;
            option.Price = request.Price;

            await extraOptionRepository.UpdateAsync(option);

            await publishEndpoint.Publish(new OptionUpdatedEvent
            {
                OptionId = request.Id,
                Price = request.Price,
                Name = request.Name
            });

            return Unit.Value;
        }
    }
}
