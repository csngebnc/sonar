using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shipping.Domain.Entities.ParcelAggregate
{
    public interface IParcelRepository
    {
        Task<Parcel> InsertAsync(Parcel entity);
        Task<List<Parcel>> InsertRangeAsync(List<Parcel> entities);
        Task<int> GetCountAsync();
        Task<Parcel> SingleAsync(Expression<Func<Parcel, bool>> predicate);
        Task<Parcel> UpdateAsync(Parcel entity);
        IQueryable<Parcel> GetAll();
        Task<List<Parcel>> GetAllListAsync(Expression<Func<Parcel, bool>> predicate);
        Task<List<Parcel>> GetAllListAsync();
    }
}
