using CSONGE.Domain.Entities;
using Order.Domain.Entities.Data;
using System;

namespace Order.Domain.Entities.LockerAggregate
{
    public class Locker : IEntity<Guid>
    {
        public string Name { get; set; }
        public AddressData Address { get; set; }
    }
}
