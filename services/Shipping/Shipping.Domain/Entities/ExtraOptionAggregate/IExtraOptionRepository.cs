using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shipping.Domain.Entities.ExtraOptionAggregate
{
    public interface IExtraOptionRepository
    {
        Task<List<ExtraOption>> GetAllListAsync(Expression<Func<ExtraOption, bool>> predicate);
        Task<List<ExtraOption>> GetAllListAsync();
        Task<ExtraOption> SingleAsync(Expression<Func<ExtraOption, bool>> predicate);
        Task<ExtraOption> InsertAsync(ExtraOption entity);
        Task<ExtraOption> UpdateAsync(ExtraOption entity);
        Task DeleteAsync(ExtraOption entity);
        IQueryable<ExtraOption> GetAll();
    }
}
