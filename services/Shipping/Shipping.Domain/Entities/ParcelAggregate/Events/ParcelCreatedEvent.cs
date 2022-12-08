using SharedKernel.Enums;
using Shipping.Domain.Entities.Data;
using Shipping.Domain.Entities.ExtraOptionAggregate;
using Shipping.Domain.Entities.LockerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Domain.Entities.ParcelAggregate.Events
{
    public class ParcelCreatedEvent : ParcelEventBase
    {
        public string TrackingNumber { get; set; }
        public PersonData Sender { get; set; }
        public PersonData Receiver { get; set; }
        public ParcelSize Size { get; set; }
        public ParcelType Type { get; set; }
        public Guid? LockerId { get; set; }
        public AddressData PickupAddress { get; set; }
        public AddressData DeliveryAddress { get; set; }
        public DateTime RegisteredAt { get; set; }

        public ICollection<ExtraOption> ExtraOptions { get; set; }
        public int CashOnDeliveryPrice { get; set; }
    }
}
