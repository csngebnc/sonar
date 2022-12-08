using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shipping.Domain.Entities.LockerAggregate;

namespace Shipping.Dal.EntityConfigurations
{
    public class LockerEntityConfiguration : IEntityTypeConfiguration<Locker>
    {
        public void Configure(EntityTypeBuilder<Locker> builder)
        {
            builder.OwnsOne(x => x.Address);
        }
    }
}