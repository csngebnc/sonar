using CSONGE.Dal.Repository;
using Partner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partner.Dal.Repositories
{
    public class ContactRepository : RepositoryBase<PartnerDbContext, Contact, Guid>, IContactRepository
    {
        public ContactRepository(PartnerDbContext context) : base(context)
        {
        }
    }
}
