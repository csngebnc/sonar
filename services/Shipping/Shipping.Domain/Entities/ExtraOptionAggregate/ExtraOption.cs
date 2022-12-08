using CSONGE.Domain.Entities;
using Shipping.Domain.Entities.ParcelAggregate;
using System;
using System.Collections.Generic;

namespace Shipping.Domain.Entities.ExtraOptionAggregate
{
    public class ExtraOption : IEntity<Guid>
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Parcel> Parcels { get; set; }
    }
}
