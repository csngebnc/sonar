using CSONGE.Domain.Entities;
using Shipping.Domain.Entities.Data;
using System;

namespace Shipping.Domain.Entities.LockerAggregate
{
    public class Locker : IEntity<Guid>
    {
        public string Name { get; set; }
        public AddressData Address { get; set; }
        public bool IsDeleted { get; set; }
    }
}
