using MassTransit;
using Order.Domain.Entities.LockerAggregate;
using Order.Interface.Events.LockerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.EventHandlers.LockerAggregate
{
    public class LockerAddedEventHandler : IConsumer<LockerAddedEvent>
    {
        private readonly ILockerRepository lockerRepository;

        public LockerAddedEventHandler(ILockerRepository lockerRepository)
        {
            this.lockerRepository = lockerRepository;
        }
        public async Task Consume(ConsumeContext<LockerAddedEvent> context)
        {
            var locker = new Locker
            {
                Id = context.Message.LockerId,
                Address = context.Message.Address,
                Name = context.Message.Name,
            };

            await lockerRepository.InsertAsync(locker);
        }
    }
}
