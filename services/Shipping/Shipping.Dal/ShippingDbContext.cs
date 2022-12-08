using Microsoft.EntityFrameworkCore;
using Shipping.Domain.Entities;
using Shipping.Domain.Entities.ExtraOptionAggregate;
using Shipping.Domain.Entities.LockerAggregate;
using Shipping.Domain.Entities.ParcelAggregate;
using System;
using System.Reflection;

namespace Shipping.Dal
{
    public class ShippingDbContext : DbContext
    {
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Locker> Lockers { get; set; }
        public DbSet<ExtraOption> ExtraOptions { get; set; }
        public DbSet<ShippingUser> Users { get; set; }

        public ShippingDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ShippingDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<ShippingUser>().HasData(new ShippingUser
            {
                Id = Guid.Parse("85b4a2a3-aa22-40e4-9b91-c72e96ab8e08"),
                Username = "csngebnc"
            });
        }
    }
}
