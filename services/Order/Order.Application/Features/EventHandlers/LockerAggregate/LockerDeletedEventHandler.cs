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
    public class LockerDeletedEventHandler : IConsumer<LockerDeletedEvent>
    {
        private readonly ILockerRepository lockerRepository;

        public LockerDeletedEventHandler(ILockerRepository lockerRepository)
        {
            this.lockerRepository = lockerRepository;
        }

        public async Task Consume(ConsumeContext<LockerDeletedEvent> context)
        {
            var locker = await lockerRepository
                .GetAll()
                .SingleAsync(x => x.Id == context.Message.LockerId);

            await lockerRepository.DeleteAsync(locker);
        }
    }
}
