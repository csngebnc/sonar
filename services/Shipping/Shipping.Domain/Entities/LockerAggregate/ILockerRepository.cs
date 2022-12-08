using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shipping.Domain.Entities.LockerAggregate
{
    public interface ILockerRepository
    {
        Task<Locker> SingleAsync(Expression<Func<Locker, bool>> predicate);
        Task<List<Locker>> GetAllListAsync();
        Task<Locker> InsertAsync(Locker entity);
        Task<Locker> UpdateAsync(Locker entity);
        Task DeleteAsync(Locker entity);
        IQueryable<Locker> GetAll();
    }
}
