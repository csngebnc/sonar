using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities.ExtraOptionAggregate;
using Order.Domain.Entities.LockerAggregate;
using Order.Domain.Entities.ParcelAggregate;

namespace Order.Dal
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Locker> Lockers { get; set; }
        public DbSet<ExtraOption> ExtraOptions { get; set; }

        public OrderDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
