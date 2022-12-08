using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Domain.Entities.ParcelAggregate
{
    public interface IParcelRepository
    {
        Task<Parcel> InsertAsync(Parcel entity);
        Task<List<Parcel>> InsertRangeAsync(List<Parcel> entities);
        Task<int> GetCountAsync();
    }
}
