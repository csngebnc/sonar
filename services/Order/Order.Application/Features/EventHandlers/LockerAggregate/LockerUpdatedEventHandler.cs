using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities.LockerAggregate;
using Order.Interface.Events.LockerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.EventHandlers.LockerAggregate
{
    public class LockerUpdatedEventHandler : IConsumer<LockerUpdatedEvent>
    {
        private readonly ILockerRepository lockerRepository;

        public LockerUpdatedEventHandler(ILockerRepository lockerRepository)
        {
            this.lockerRepository = lockerRepository;
        }
        public async Task Consume(ConsumeContext<LockerUpdatedEvent> context)
        {
            var locker = await lockerRepository
                .GetAll()
                .SingleAsync(x => x.Id == context.Message.LockerId);

            locker.Address = context.Message.Address;
            locker.Name = context.Message.Name;

            await lockerRepository.UpdateAsync(locker);
        }
    }
}
