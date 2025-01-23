using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity;
using ExplorersHaven.Models;

namespace Explorers_Haven.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //     ApplicationDbContext
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Travelogue> Travelogues { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Travel> Travels { get; set; }
        public DbSet<Stay> Stays { get; set; }
        public DbSet<Activity> Activites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne()
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
