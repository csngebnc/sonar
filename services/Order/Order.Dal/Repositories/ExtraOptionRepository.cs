using CSONGE.Dal.Repository;
using Order.Domain.Entities.ExtraOptionAggregate;
using Order.Domain.Entities.LockerAggregate;
using System;

namespace Order.Dal.Repositories
{
    public class ExtraOptionRepository : RepositoryBase<OrderDbContext, ExtraOption, Guid>, IExtraOptionRepository
    {
        public ExtraOptionRepository(OrderDbContext context) : base(context)
        {
        }
    }
}
