using MassTransit;
using Order.Domain.Entities.ExtraOptionAggregate;
using Order.Interface.Events.ExtraOptionAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.EventHandlers.ExtraOptionAggregate
{
    public class ExtraOptionUpdatedEventHandler : IConsumer<OptionUpdatedEvent>
    {
        private readonly IExtraOptionRepository extraOptionRepository;

        public ExtraOptionUpdatedEventHandler(IExtraOptionRepository extraOptionRepository)
        {
            this.extraOptionRepository = extraOptionRepository;
        }
        public async Task Consume(ConsumeContext<OptionUpdatedEvent> context)
        {
            var option = await extraOptionRepository.SingleAsync(x => x.Id == context.Message.OptionId);

            option.Name = context.Message.Name;
            option.Price = context.Message.Price;

            await extraOptionRepository.UpdateAsync(option);
        }
    }
}
