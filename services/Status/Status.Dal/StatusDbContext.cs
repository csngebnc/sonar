using Microsoft.EntityFrameworkCore;
using Status.Domain.Entities;
using Status.Interface.Events;

namespace Status.Dal
{
    public class StatusDbContext : DbContext
    {
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<ParcelEvent> ParcelEvents { get; set; }

        public StatusDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
