using CSONGE.Domain.Entities;
using Order.Domain.Entities.Data;
using Order.Domain.Entities.ExtraOptionAggregate;
using Order.Domain.Entities.LockerAggregate;
using SharedKernel.Enums;
using System;
using System.Collections.Generic;

namespace Order.Domain.Entities.ParcelAggregate
{
    public class Parcel : IEntity<Guid>
    {
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
    }
}
