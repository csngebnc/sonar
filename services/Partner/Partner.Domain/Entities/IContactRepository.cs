using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Domain.Entities
{
    public interface IContactRepository
    {
        Task<Contact> InsertAsync(Contact entity);
        Task<Contact> FirstOrDefaultAsync(Expression<Func<Contact, bool>> predicate);
        Task<Contact> SingleOrDefaultAsync(Expression<Func<Contact, bool>> predicate);
        Task<Contact> SingleAsync(Expression<Func<Contact, bool>> predicate);
        Task<Contact> SingleIncludingAsync(Expression<Func<Contact, bool>> predicate, params Expression<Func<Contact, object>>[] propertySelectors);
        Task<Contact> UpdateAsync(Contact entity);
        Task DeleteAsync(Contact entity);
        IQueryable<Contact> GetAllIncluding(params Expression<Func<Contact, object>>[] propertySelectors);
        IQueryable<Contact> GetAll();
        Task<List<Contact>> GetAllListAsync(Expression<Func<Contact, bool>> predicate);
    }
}
