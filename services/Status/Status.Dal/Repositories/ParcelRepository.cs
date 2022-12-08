using CSONGE.Dal.Repository;
using Microsoft.EntityFrameworkCore;
using Status.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Dal.Repositories
{
    public class ParcelRepository : RepositoryBase<StatusDbContext, Parcel, Guid>, IParcelRepository
    {
        public ParcelRepository(StatusDbContext context) : base(context)
        {
        }

        public async Task<Parcel> FindByIdAsync(Guid id)
        {
            return await GetAllIncluding(x => x.Events)
                .SingleAsync(x => x.Id == id);
        }

        public async Task<Parcel> TryFindByIdAsync(Guid id)
        {
            return await GetAllIncluding(x => x.Events)
                .FirstOrDefaultAsync(package => package.Id == id);
        }

        public async Task<Parcel> FindByTrackingNumberAsync(string trackingNumber)
        {
            return await GetAllIncluding(x => x.Events)
                .SingleAsync(package => package.TrackingNumber == trackingNumber);
        }
    }
}
