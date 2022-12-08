using SharedKernel.Enums;
using System;

namespace Shipping.Domain.Entities.ParcelAggregate.Events
{
    public class ParcelEventBase
    {
        public Guid Id { get; set; }
        public Guid ParcelId { get; set; }
        public ParcelState State { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
