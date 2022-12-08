using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities.LockerAggregate;
using Order.Interface.Events.LockerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Interface.Events.ExtraOptionAggregate;
using Order.Domain.Entities.ExtraOptionAggregate;

namespace Order.Application.Features.EventHandlers.ExtraOptionAggregate
{
    public class ExtraOptionDeletedEventHandler : IConsumer<OptionDeletedEvent>
    {
        private readonly IExtraOptionRepository extraOptionRepository;

        public ExtraOptionDeletedEventHandler(IExtraOptionRepository extraOptionRepository)
        {
            this.extraOptionRepository = extraOptionRepository;
        }

        public async Task Consume(ConsumeContext<OptionDeletedEvent> context)
        {
            var locker = await extraOptionRepository
                .GetAll()
                .SingleAsync(x => x.Id == context.Message.OptionId);

            await extraOptionRepository.DeleteAsync(locker);
        }
    }
}
