using CSONGE.Dal.Repository;
using Shipping.Dal;
using Shipping.Domain.Entities.ParcelAggregate;
using System;

namespace Order.Dal.Repositories
{
    public class ParcelRepository : RepositoryBase<ShippingDbContext, Parcel, Guid>, IParcelRepository
    {
        public ParcelRepository(ShippingDbContext context) : base(context)
        {
        }
    }
}
