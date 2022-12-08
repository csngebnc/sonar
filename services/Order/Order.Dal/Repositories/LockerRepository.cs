using CSONGE.Dal.Repository;
using Order.Domain.Entities.LockerAggregate;
using System;

namespace Order.Dal.Repositories
{
    public class LockerRepository : RepositoryBase<OrderDbContext, Locker, Guid>, ILockerRepository
    {
        public LockerRepository(OrderDbContext context) : base(context)
        {
        }
    }
}
