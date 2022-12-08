using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities.ParcelAggregate;

namespace Order.Dal.EntityConfigurations
{
    public class ParcelEntityConfiguration : IEntityTypeConfiguration<Parcel>
    {
        public void Configure(EntityTypeBuilder<Parcel> builder)
        {
            builder.OwnsOne(x => x.Sender);
            builder.OwnsOne(x => x.Receiver);
            builder.OwnsOne(x => x.Size);

            builder.HasOne(x => x.Locker)
                .WithMany()
                .HasForeignKey(x => x.LockerId)
                .IsRequired(false);

            builder.OwnsOne(x => x.PickupAddress);
            builder.OwnsOne(x => x.DeliveryAddress);

            builder.HasMany(x => x.ExtraOptions)
                .WithMany(x => x.Parcels);
        }
    }
}