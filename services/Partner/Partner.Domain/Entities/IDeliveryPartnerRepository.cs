using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Domain.Entities
{
    public interface IDeliveryPartnerRepository
    {
        Task<DeliveryPartner> InsertAsync(DeliveryPartner entity);
        Task<DeliveryPartner> FirstOrDefaultAsync(Expression<Func<DeliveryPartner, bool>> predicate);
        Task<DeliveryPartner> SingleOrDefaultAsync(Expression<Func<DeliveryPartner, bool>> predicate);
        Task<DeliveryPartner> SingleAsync(Expression<Func<DeliveryPartner, bool>> predicate);
        Task<DeliveryPartner> SingleIncludingAsync(Expression<Func<DeliveryPartner, bool>> predicate, params Expression<Func<DeliveryPartner, object>>[] propertySelectors);
        Task<DeliveryPartner> UpdateAsync(DeliveryPartner entity);
        Task DeleteAsync(DeliveryPartner entity);
        IQueryable<DeliveryPartner> GetAllIncluding(params Expression<Func<DeliveryPartner, object>>[] propertySelectors);
        IQueryable<DeliveryPartner> GetAll();
    }
}
