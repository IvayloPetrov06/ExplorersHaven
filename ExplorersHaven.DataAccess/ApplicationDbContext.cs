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
using Explorers_Haven.Models;
using System.Runtime.ConstrainedExecution;

namespace Explorers_Haven.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //     ApplicationDbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Travelogue> Travelogues { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Travel> Travels { get; set; }
        public DbSet<Stay> Stays { get; set; }
        public DbSet<Activity> Activites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Travelogue>().HasData(
                new Travelogue { Id = 1, Name ="Egipet Patepis" }
                );
            //trip 1
            modelBuilder.Entity<Trip>().HasData(
                new Trip { Id = 1, Name = "KazanlakPlovdiv", TravelogueId = 1 }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel { Id = 1, Start = "Kazanlak", Finish = "Plovdiv",Transport="Car", TripId = 1 }
                );
            //trip 2
            modelBuilder.Entity<Trip>().HasData(
                new Trip { Id = 2, Name = "PlovdivKairo", TravelogueId = 1 }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel { Id = 2, Start = "Plovdiv", Finish = "Kairo", Transport = "Plane", TripId = 2 }
                );
            modelBuilder.Entity<Stay>().HasData(
                new Stay { Id = 1, Name = "ZlatniPqsuci", TripId = 2 }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity { Id = 1, Name = "Qzdene na kamili", TripId = 2 }
                );
            //trip 3
            modelBuilder.Entity<Trip>().HasData(
                new Trip { Id = 3, Name = "KairoKazanluk", TravelogueId = 1 }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel { Id = 3, Start = "Kairo", Finish = "Kazanlak", Transport = "Plane", TripId = 3 }
                );
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne()
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

         
            
                
                
        
        modelBuilder.Entity<Activity>(b =>
            {
                b.HasKey(e => e.Id);

                b.Property(e => e.Name)
                   .IsRequired();

                b.HasOne(a => a.Trip)
                .WithMany(t => t.Activities)
                .HasForeignKey(e => e.TripId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Stay>(b =>
            {
                
                b.HasKey(e => e.Id);

                
                b.Property(e => e.Name)
                    .IsRequired();


                b.HasOne(e => e.Trip)
                 .WithOne(b=>b.Stay)
                 .HasForeignKey<Stay>(e => e.TripId)
                 .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<Travel>(b =>
            {

                b.HasKey(e => e.Id);


                b.Property(e => e.Start)
                 .IsRequired();

                b.Property(e => e.Finish)
                 .IsRequired();

                b.Property(e => e.Transport)
                 .IsRequired();


                b.HasOne(e => e.Trip)
                 .WithOne(b=>b.Travel)
                 .HasForeignKey<Travel>(e => e.TripId)
                  .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Travelogue>(b =>
            {
                b.HasKey(e => e.Id);


                b.Property(e => e.Name)
                 .IsRequired();

                b.HasMany(a => a.Trips)
                .WithOne(b=>b.Travelogue)
                .HasForeignKey(a =>a.TravelogueId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Trip>(b =>
            {
                b.HasKey(e => e.Id);


                b.Property(e => e.Name)
                 .IsRequired();

                b.HasOne(a => a.Travelogue)
                 .WithMany(b=>b.Trips)
                 .HasForeignKey(e => e.TravelogueId)
                  .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(a => a.Activities)
                .WithOne(b=>b.Trip)
                .HasForeignKey(e => e.TripId)
                .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(e => e.Stay)
                 .WithOne(b => b.Trip)
                 .HasForeignKey<Stay>(e => e.TripId)
                  .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(e => e.Travel)
                 .WithOne(b => b.Trip)
                 .HasForeignKey<Travel>(e => e.TripId)
                 .OnDelete(DeleteBehavior.Cascade);
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
