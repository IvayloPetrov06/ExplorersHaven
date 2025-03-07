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
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        //     ApplicationDbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Offer> Offers { get; set; }
        //public DbSet<Trip> Trips { get; set; }
        public DbSet<Travel> Travels { get; set; }
        public DbSet<Stay> Stays { get; set; }
        public DbSet<Activity> Activites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //offer 1
           modelBuilder.Entity<Offer>().HasData(
                new Offer { Id = 1, Name ="Egypt",CoverImage= "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243536/Egypt_geyymk.jpg", Price=100,  }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel { Id = 1, Start = "Sofia", Finish = "Cairo", Transport = "Plane", OfferId = 1 }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel { Id = 2, Start = "Cairo", Finish = "Sofia", Transport = "Plane", OfferId = 1 }
                );
            modelBuilder.Entity<Stay>().HasData(
                new Stay { Id = 1, Name = "Megawish Hotel", OfferId = 1 }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity { Id = 1, Name = "Camel riding", OfferId = 1 }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity { Id = 2, Name = "Sightseeing", OfferId = 1 }
                );

            //offer 2
            modelBuilder.Entity<Offer>().HasData(
                new Offer { Id = 2, Name = "Poland",CoverImage= "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243527/Poland_heknwf.jpg", Price = 200 }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel { Id = 3, Start = "Sofia", Finish = "Warsaw", Transport = "Plane", OfferId = 2 }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel { Id = 4, Start = "Warsaw", Finish = "Sofia", Transport = "Plane", OfferId = 2 }
                );
            modelBuilder.Entity<Stay>().HasData(
                new Stay { Id = 2, Name = "InterContinental Warsaw Hotel", OfferId = 2 }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity { Id = 3, Name = "Sightseeing", OfferId = 2 }
                );

            //offer 3
            modelBuilder.Entity<Offer>().HasData(
                new Offer { Id = 3, Name = "Germany",CoverImage= "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243521/Germany_iifb9a.jpg", Price = 500 }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel { Id = 5, Start = "Sofia", Finish = "Berlin", Transport = "Plane", OfferId = 3 }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel { Id = 6, Start = "Berlin", Finish = "Sofia", Transport = "Plane", OfferId = 3 }
                );
            modelBuilder.Entity<Stay>().HasData(
                new Stay { Id = 3, Name = "Mitte Hotel", OfferId = 3 }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity { Id = 4, Name = "Sightseeing", OfferId = 3 }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity { Id = 5, Name = "Archery", OfferId = 3 }
                );
            modelBuilder.Entity<User>(b =>
            {
                modelBuilder.Entity<User>()
                .HasOne(x => x.UserIdentity)
                .WithOne()
                .HasForeignKey<User>(x => x.UserIdentityId)
                .OnDelete(DeleteBehavior.Cascade);
                /* // Each User can have many UserClaims
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
                     .OnDelete(DeleteBehavior.Cascade);*/
            });

         
            
                
                
        
        modelBuilder.Entity<Activity>(b =>
            {
                b.HasKey(e => e.Id);

                b.Property(e => e.Name)
                   .IsRequired();

                b.HasOne(a => a.Offer)
                .WithMany(t => t.Activities)
                .HasForeignKey(e => e.OfferId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Stay>(b =>
            {
                
                b.HasKey(e => e.Id);

                
                b.Property(e => e.Name)
                    .IsRequired();


                b.HasOne(e => e.Offer)
                 .WithOne(b=>b.Stay)
                 .HasForeignKey<Stay>(e => e.OfferId)
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


                b.HasOne(e => e.Offer)
                 .WithMany(t => t.Travels)
                .HasForeignKey(e => e.OfferId)
                .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<Offer>(b =>
            {
                b.HasKey(e => e.Id);


                b.Property(e => e.Name)
                 .IsRequired();

                b.Property(e => e.CoverImage);

                b.Property(e => e.Price);

                b.HasMany(a => a.Activities)
                .WithOne(b => b.Offer)
                .HasForeignKey(e => e.OfferId)
                .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(e => e.Stay)
                 .WithOne(b => b.Offer)
                 .HasForeignKey<Stay>(e => e.OfferId)
                  .OnDelete(DeleteBehavior.Cascade);

                b.HasMany(e => e.Travels)
                 .WithOne(b => b.Offer)
                 .HasForeignKey(e => e.OfferId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            


            base.OnModelCreating(modelBuilder);
        }
    }
}
