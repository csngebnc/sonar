using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities.LockerAggregate;

namespace Order.Dal.EntityConfigurations
{
    public class LockerEntityConfiguration : IEntityTypeConfiguration<Locker>
    {
        public void Configure(EntityTypeBuilder<Locker> builder)
        {
            builder.OwnsOne(x => x.Address);
        }
    }
}