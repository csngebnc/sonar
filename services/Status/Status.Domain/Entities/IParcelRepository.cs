using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Status.Domain.Entities
{
    public interface IParcelRepository
    {
        Task<Parcel> InsertAsync(Parcel entity);
        Task<List<Parcel>> InsertRangeAsync(List<Parcel> entities);
        Task<Parcel> FindByTrackingNumberAsync(string trackingNumber);
        IQueryable<Parcel> GetAll();
    }
}
