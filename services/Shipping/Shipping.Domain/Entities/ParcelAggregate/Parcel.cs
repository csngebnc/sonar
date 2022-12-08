using CSONGE.Domain.Entities;
using SharedKernel.Enums;
using Shipping.Domain.Entities.Data;
using Shipping.Domain.Entities.ExtraOptionAggregate;
using Shipping.Domain.Entities.LockerAggregate;
using Shipping.Domain.Entities.ParcelAggregate.Events;
using System;
using System.Collections.Generic;

namespace Shipping.Domain.Entities.ParcelAggregate
{
    public class Parcel : IEntity<Guid>
    {
        public ParcelState State { get; set; }
        public string TrackingNumber { get; set; }
        public PersonData Sender { get; set; }
        public PersonData Receiver { get; set; }
        public ParcelSize Size { get; set; }
        public ParcelType Type { get; set; }
        public Guid? LockerId { get; set; }
        public Locker Locker { get; set; }
        public AddressData PickupAddress { get; set; }
        public AddressData DeliveryAddress { get; set; }
        public DateTime RegisteredAt { get; set; }

        public ICollection<ExtraOption> ExtraOptions { get; set; }
        public int CashOnDeliveryPrice { get; set; }

        public List<ParcelEventBase> Events { get; set; } = new List<ParcelEventBase>();


        public void Apply(ParcelCreatedEvent @event)
        {
            TrackingNumber = @event.TrackingNumber;
            Sender = @event.Sender;
            Receiver = @event.Receiver;
            Size = @event.Size;
            Type = @event.Type;
            LockerId = @event.LockerId;
            PickupAddress = @event.PickupAddress;
            DeliveryAddress = @event.DeliveryAddress;
            RegisteredAt= @event.RegisteredAt;
            ExtraOptions= @event.ExtraOptions;
            CashOnDeliveryPrice = @event.CashOnDeliveryPrice;

            Events.Add(@event);
        }

        public void Apply(ParcelUpdatedEvent @event)
        {
            State = @event.State;
            Events.Add(@event);
        }
    }

}
