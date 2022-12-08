using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Domain.Entities
{
    public interface IShippingUserRepository
    {
        Task<ShippingUser> SingleAsync(Expression<Func<ShippingUser, bool>> predicate);
        Task<List<ShippingUser>> GetAllListAsync();
        Task<ShippingUser> InsertAsync(ShippingUser entity);
        Task<ShippingUser> UpdateAsync(ShippingUser entity);
        Task DeleteAsync(ShippingUser entity);
        IQueryable<ShippingUser> GetAll();
    }
}
