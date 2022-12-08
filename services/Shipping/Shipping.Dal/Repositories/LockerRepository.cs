using CSONGE.Dal.Repository;
using Shipping.Domain.Entities.LockerAggregate;
using System;

namespace Shipping.Dal.Repositories
{
    public class LockerRepository : RepositoryBase<ShippingDbContext, Locker, Guid>, ILockerRepository
    {
        public LockerRepository(ShippingDbContext context) : base(context)
        {
        }
    }
}
