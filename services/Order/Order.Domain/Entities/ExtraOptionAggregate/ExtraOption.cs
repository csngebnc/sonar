using CSONGE.Domain.Entities;
using Order.Domain.Entities.ParcelAggregate;
using System;
using System.Collections.Generic;

namespace Order.Domain.Entities.ExtraOptionAggregate
{
    public class ExtraOption : IEntity<Guid>
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public ICollection<Parcel> Parcels { get; set; }
    }
}
