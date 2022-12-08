using CSONGE.Dal.Repository;
using Shipping.Domain.Entities.ExtraOptionAggregate;
using System;

namespace Shipping.Dal.Repositories
{
    public class ExtraOptionRepository : RepositoryBase<ShippingDbContext, ExtraOption, Guid>, IExtraOptionRepository
    {
        public ExtraOptionRepository(ShippingDbContext context) : base(context)
        {
        }
    }
}
