using CSONGE.Dal.Repository;
using Order.Domain.Entities.ParcelAggregate;
using System;

namespace Order.Dal.Repositories
{
    public class ParcelRepository : RepositoryBase<OrderDbContext, Parcel, Guid>, IParcelRepository
    {
        public ParcelRepository(OrderDbContext context) : base(context)
        {
        }
    }
}
