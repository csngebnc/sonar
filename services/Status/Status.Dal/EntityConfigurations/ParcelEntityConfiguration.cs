using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Status.Domain.Entities;

namespace Status.Dal.EntityConfigurations
{
    public class ParcelEntityConfiguration : IEntityTypeConfiguration<Parcel>
    {
        public void Configure(EntityTypeBuilder<Parcel> builder)
        {
            builder.HasKey(ent => ent.Id);
            builder.Property(ent => ent.Id)
                .ValueGeneratedNever();
        }
    }
}
