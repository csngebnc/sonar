using MassTransit;
using SharedKernel.Enums;
using Status.Domain.Entities;
using Status.Interface.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Status.Application.Features.EventConsumers
{
    public class ParcelsCreatedEventConsumer : IConsumer<ParcelsCreatedEvent>
    {
        private readonly IParcelRepository repository;

        public ParcelsCreatedEventConsumer(IParcelRepository repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<ParcelsCreatedEvent> context)
        {
            var parcels = context.Message.Parcels
                .Select(@event => new Parcel(new ParcelCreatedEvent
                {
                    Id = Guid.NewGuid(),
                    ParcelId = @event.ParcelId,
                    TrackingNumber = @event.TrackingNumber,
                    Type = @event.Type,
                    State = ParcelState.NEW
                    
                }))
                .ToList();

            await repository.InsertRangeAsync(parcels);
        }
    }
}