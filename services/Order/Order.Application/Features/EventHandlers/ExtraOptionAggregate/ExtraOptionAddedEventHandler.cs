using MassTransit;
using Order.Domain.Entities.ExtraOptionAggregate;
using Order.Domain.Entities.LockerAggregate;
using Order.Interface.Events.ExtraOptionAggregate;
using System.Threading.Tasks;

namespace Order.Application.Features.EventHandlers.ExtraOptionAggregate
{
    public class ExtraOptionAddedEventHandler : IConsumer<OptionAddedEvent>
    {
        private readonly IExtraOptionRepository extraOptionRepository;

        public ExtraOptionAddedEventHandler(IExtraOptionRepository extraOptionRepository)
        {
            this.extraOptionRepository = extraOptionRepository;
        }
        public async Task Consume(ConsumeContext<OptionAddedEvent> context)
        {
            var option = new ExtraOption
            {
                Id = context.Message.OptionId,
                Name= context.Message.Name,
                Price= context.Message.Price,
            };

            await extraOptionRepository.InsertAsync(option);
        }
    }
}
