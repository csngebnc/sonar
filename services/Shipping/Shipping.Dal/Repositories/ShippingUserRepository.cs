using CSONGE.Dal.Repository;
using Shipping.Domain.Entities;
using System;

namespace Shipping.Dal.Repositories
{
    public class ShippingUserRepository : RepositoryBase<ShippingDbContext, ShippingUser, Guid>
    {
        public ShippingUserRepository(ShippingDbContext context) : base(context)
        {
        }
    }
}
