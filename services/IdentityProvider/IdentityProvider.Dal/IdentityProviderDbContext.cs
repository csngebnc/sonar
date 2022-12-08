using IdentityProvider.Domain;
using IdentityProvider.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace IdentityProvider.Dal
{
    public class IdentityProviderDbContext : IdentityDbContext<ServiceUser, IdentityRole<Guid>, Guid>
    {
        public IdentityProviderDbContext(DbContextOptions options) : base(options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            PasswordHasher<ServiceUser> passwordHasher = new PasswordHasher<ServiceUser>();
            var user = new ServiceUser
            {
                Id = Guid.Parse("85b4a2a3-aa22-40e4-9b91-c72e96ab8e08"),
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "csngebnc",
                NormalizedUserName = "CSNGEBNC",
                Email = "i@i.i",
                NormalizedEmail = "I@I.I",
                EmailConfirmed = true,
                Type = UserType.Administrator
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "Aa1234.");

            modelBuilder.Entity<ServiceUser>().HasData(user);
        }
    }
}
