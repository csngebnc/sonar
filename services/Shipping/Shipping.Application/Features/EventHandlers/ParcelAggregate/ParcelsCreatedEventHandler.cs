using MassTransit;
using SharedKernel.Enums;
using Shipping.Domain.Entities.ParcelAggregate;
using Shipping.Interface.Events;
using System.Linq;
using System.Threading.Tasks;

namespace Shipping.Application.Features.EventHandlers.ParcelAggregate
{
    public class ParcelsCreatedEventHandler : IConsumer<ParcelsCreatedEvent>
    {
        private readonly IParcelRepository parcelRepository;

        public ParcelsCreatedEventHandler(IParcelRepository parcelRepository)
        {
            this.parcelRepository = parcelRepository;
        }

        public async Task Consume(ConsumeContext<ParcelsCreatedEvent> context)
        {
            var parcels = context.Message.Parcels.Select(x => new Parcel
            {
                Id = x.ParcelId,
                Type = x.Type,
                CashOnDeliveryPrice = x.CashOnDeliveryPrice,
                DeliveryAddress = x.DeliveryAddress,
                Events = new()
                {
                    new()
                    {
                        State = ParcelState.NEW,
                        CreatedAt = context.Message.CreationDate,
                    }
                },
                ExtraOptions = x.ExtraOptions,
                LockerId = x.LockerId,
                PickupAddress = x.PickupAddress,
                Receiver = x.Receiver,
                RegisteredAt = x.RegisteredAt,
                Sender = x.Sender,
                Size = x.Size,
                State = ParcelState.NEW,
                TrackingNumber = x.TrackingNumber,
            }).ToList();

            await parcelRepository.InsertRangeAsync(parcels);
        }
    }
}
