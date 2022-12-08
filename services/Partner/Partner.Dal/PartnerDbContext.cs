using Microsoft.EntityFrameworkCore;
using Partner.Domain.Entities;

namespace Partner.Dal
{
    public class PartnerDbContext : DbContext
    {
        public DbSet<DeliveryPartner> Partners { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public PartnerDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>().OwnsOne(x => x.Address);
        }
    }
}