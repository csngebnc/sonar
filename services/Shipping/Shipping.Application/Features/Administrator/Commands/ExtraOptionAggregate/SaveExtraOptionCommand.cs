using MassTransit;
using MediatR;
using Order.Interface.Events.ExtraOptionAggregate;
using Shipping.Domain.Entities.ExtraOptionAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Shipping.Application.Features.Administrator.Commands.ExtraOptionAggregate
{
    public class SaveExtraOptionCommand : IRequest
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }

    public class SaveExtraOptionCommandHandler : IRequestHandler<SaveExtraOptionCommand, Unit>
    {
        private readonly IExtraOptionRepository extraOptionRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public SaveExtraOptionCommandHandler(IExtraOptionRepository extraOptionRepository, IPublishEndpoint publishEndpoint)
        {
            this.extraOptionRepository = extraOptionRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(SaveExtraOptionCommand request, CancellationToken cancellationToken)
        {
            var option = new ExtraOption
            {
                Name = request.Name,
                Price = request.Price
            };

            await extraOptionRepository.InsertAsync(option);

            await publishEndpoint.Publish(new OptionAddedEvent
            {
                OptionId = option.Id,
                Name = request.Name,
                Price = request.Price
            });

            return Unit.Value;
        }
    }
}
