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

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
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

            modelBuilder.Entity<Activity>(b =>
            {
                b.HasKey(e => e.Id);

                b.Property(e => e.Name)
                   .IsRequired();

                b.HasOne(a => a.Trip)
                .WithMany()
                .HasForeignKey(e => e.TripId);
            });

            modelBuilder.Entity<Stay>(b =>
            {
                
                b.HasKey(e => e.Id);

                
                b.Property(e => e.Name)
                    .IsRequired();


                b.HasOne(e => e.Trip)
                 .WithOne()
                 .HasForeignKey<Stay>(e => e.TripId);

            });

            modelBuilder.Entity<Travel>(b =>
            {

                b.HasKey(e => e.Id);


                b.Property(e => e.Start)
                 .IsRequired();

                b.Property(e => e.End)
                 .IsRequired();

                b.Property(e => e.Transport)
                 .IsRequired();


                b.HasOne(e => e.Trip)
                 .WithOne()
                 .HasForeignKey<Travel>(e => e.TripId);

            });

            modelBuilder.Entity<Travelogue>(b =>
            {
                b.HasKey(e => e.Id);


                b.Property(e => e.Name)
                 .IsRequired();

                b.HasMany(a => a.Trips)
                .WithOne()
                .HasForeignKey(a =>a.TravelogueId);
            });

            modelBuilder.Entity<Trip>(b =>
            {
                b.HasKey(e => e.Id);


                b.Property(e => e.Name)
                 .IsRequired();

                b.HasOne(a => a.Travelogue)
                 .WithMany()
                 .HasForeignKey(e => e.TravelogueId);

                b.HasMany(a => a.Activities)
                .WithOne()
                .HasForeignKey(e => e.TripId);

                b.HasOne(e => e.Stay)
                 .WithOne()
                 .HasForeignKey<Stay>(e => e.TripId);

                b.HasOne(e => e.Travel)
                 .WithOne()
                 .HasForeignKey<Travel>(e => e.TripId);
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
