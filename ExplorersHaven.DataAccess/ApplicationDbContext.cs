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
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Explorers_Haven.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        IServiceProvider serviceProvider;

        public ApplicationDbContext(DbContextOptions options, IServiceProvider _serviceProvider) : base(options)
        {
            serviceProvider = _serviceProvider;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Offer> Offers { get; set; }
        //public DbSet<Trip> Trips { get; set; }
        public DbSet<Travel> Travels { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Stay> Stays { get; set; }
        public DbSet<Models.Activity> Activites { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        
        protected async override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Transport>().HasData(
                new Transport
                {
                    Id = 1,
                    Name = "Plane"
                });
            modelBuilder.Entity<Transport>().HasData(
                new Transport
                {
                    Id = 2,
                    Name = "Train"
                });
            modelBuilder.Entity<Transport>().HasData(
                new Transport
                {
                    Id = 3,
                    Name = "Boat"
                });
            modelBuilder.Entity<Transport>().HasData(
                new Transport
                {
                    Id = 4,
                    Name = "Custom"
                });
            
            modelBuilder.Entity<Offer>().HasData(
                new Offer {
                    Id = 1,
                    Name ="Egypt",
                    CoverImage= "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243536/Egypt_geyymk.jpg",
                    BackImage= "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741541997/Egypt1_bzftps.avif",
                    Price=100,
                    StayId = 1,
                    Disc = "Travel across Egypt and cruise down the Nile River, tour the pyramids of Giza.",
                    DurationDays = 4,
                    StartDate = new DateOnly(2025, 4, 1),
                    LastDate = new DateOnly(2025, 4, 8),
                    Discount = 20,
                    MaxPeople = 8,
                    Rating = 3

                }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel { Id = 1,
                    Start = "Sofia",
                    Finish = "Cairo",
                    DurationDays = 1,
                    OfferId = 1,
                    TransportId = 1,
                    Arrival = true
                }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel {
                    Id = 2,
                    Start = "Cairo",
                    Finish = "Sofia",
                    DurationDays = 1,
                    OfferId = 1,
                    TransportId = 1,
                    Arrival = false
                }
                );
            modelBuilder.Entity<Stay>().HasData(
                new Stay {
                    Id = 1,
                    Name = "Megawish Hotel",
                    Price = 100,
                    Stars = 5,
                    Disc= "This Luxurious Premium Ultra all-inclusive resort in Hurghada offers only suites and villas with beachfront accommodation with total landscape area of 255.000 m2. It features 1km private sandy beach, 30 Swimming pools (9 types), 1 main buffet restaurant, 7 a-la-carte restaurants, 14 bars and free Wi-Fi in the entire property. This 5-star hotel offers private beach and pool cabanas upon request.",
                    Image = "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741542028/EgyptHotel_kc6xak.jpg"
                }
                );
            modelBuilder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 1,
                    Icon = "/Images/parking.svg",
                    Name = "Parking places"
                }
                );
            modelBuilder.Entity<StayAmenity>().HasData(
                new StayAmenity
                {
                    Id = 1,
                    StayId=1,
                    AmenityId=1
                }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity { 
                    Id = 1,
                    Name = "Camel riding",
                    CoverImage = "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243536/Egypt_geyymk.jpg",
                    OfferId = 1 }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity { Id = 2, Name = "Sightseeing",
                    CoverImage = "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243536/Egypt_geyymk.jpg", OfferId = 1 }
                );

            //offer 2
            modelBuilder.Entity<Offer>().HasData(
                new Offer { Id = 2, Name = "Poland",CoverImage= "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243527/Poland_heknwf.jpg", Price = 20 , StayId = 2}
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel { Id = 3, Start = "Sofia", Finish = "Warsaw", OfferId = 2, TransportId =1  }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel { Id = 4, Start = "Warsaw", Finish = "Sofia", OfferId = 2, TransportId =1  }
                );
            modelBuilder.Entity<Stay>().HasData(
                new Stay { Id = 2, Name = "InterContinental Warsaw Hotel"}
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity { Id = 3, Name = "Sightseeing", OfferId = 2 }
                );

            //offer 3
            modelBuilder.Entity<Offer>().HasData(
                new Offer { Id = 3, Name = "Germany",CoverImage= "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243521/Germany_iifb9a.jpg", Price = 500,StayId=3 }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel { Id = 5, Start = "Sofia", Finish = "Berlin", OfferId = 3, TransportId = 1 }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel { Id = 6, Start = "Berlin", Finish = "Sofia", OfferId = 3, TransportId = 1 }
                );
            modelBuilder.Entity<Stay>().HasData(
                new Stay { Id = 3, Name = "Mitte Hotel" }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity { Id = 4, Name = "Sightseeing", OfferId = 3 }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity { Id = 5, Name = "Archery", OfferId = 3 }
                );
            modelBuilder.Entity<Offer>().HasData(
                new Offer
                {
                    Id = 4,
                    Name = "Test",
                    CoverImage = "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243536/Egypt_geyymk.jpg",
                    BackImage = "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741541997/Egypt1_bzftps.avif",
                    Price = 100,
                    StayId = 1,
                    Disc = "Travel across Egypt and cruise down the Nile River, tour the pyramids of Giza.",
                    DurationDays = 4,
                    StartDate = new DateOnly(2025, 4, 1),
                    LastDate = new DateOnly(2025, 4, 8),
                    Discount = 20,
                    MaxPeople = 8,
                    Rating = 3

                }
                );
            modelBuilder.Entity<User>(b =>
            {
                modelBuilder.Entity<User>()
                .HasOne(x => x.UserIdentity)
                .WithOne()
                .HasForeignKey<User>(x => x.UserIdentityId)
                .OnDelete(DeleteBehavior.Cascade);
                
            });

        
        modelBuilder.Entity<Models.Activity>(b =>
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


                b.HasMany(e => e.Offers)
                 .WithOne(b => b.Stay)
                 .HasForeignKey(e => e.StayId)
                 .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Travel>(b =>
            {

                b.HasOne(e => e.Offer)
                 .WithMany(t => t.Travels)
                .HasForeignKey(e => e.OfferId)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.Transport)  
                 .WithMany(t => t.Travels)
                 .HasForeignKey(e => e.TransportId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Transport>()
                .HasMany(e => e.Travels)
                .WithOne(b => b.Transport)
                .HasForeignKey(e => e.TransportId)
                .OnDelete(DeleteBehavior.Cascade);



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
                 .WithMany(t => t.Offers)
                .HasForeignKey(e => e.StayId)
                .OnDelete(DeleteBehavior.Restrict);

                b.HasMany(e => e.Travels)
                 .WithOne(b => b.Offer)
                 .HasForeignKey(e => e.OfferId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Booking>()
                .HasOne(x => x.Offer)
                .WithMany(l => l.Bookings)
                .HasForeignKey(x => x.OfferId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
              .HasOne(x => x.User)
              .WithMany(l => l.Bookings)
              .HasForeignKey(x => x.UserId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rating>()
                .HasOne(x => x.Offer)
                .WithMany(l => l.Ratings)
                .HasForeignKey(x => x.OfferId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rating>()
              .HasOne(x => x.User)
              .WithMany(l => l.Ratings)
              .HasForeignKey(x => x.UserId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(x => x.Offer)
                .WithMany(l => l.Comments)
                .HasForeignKey(x => x.OfferId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
              .HasOne(x => x.User)
              .WithMany(l => l.Comments)
              .HasForeignKey(x => x.UserId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Favorite>()
                .HasOne(x => x.Offer)
                .WithMany(l => l.Favorites)
                .HasForeignKey(x => x.OfferId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
              .HasOne(x => x.User)
              .WithMany(l => l.Favorites)
              .HasForeignKey(x => x.UserId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StayAmenity>()
                .HasOne(x => x.Stay)
                .WithMany(l => l.StayAmenities)
                .HasForeignKey(x => x.StayId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StayAmenity>()
              .HasOne(x => x.Amenity)
              .WithMany(l => l.StayAmenities)
              .HasForeignKey(x => x.AmenityId)
              .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
        public async Task Seed()
        {

            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var adminEmail = "admin@admin.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var userA = new IdentityUser { UserName = "admin@admin.com", Email = adminEmail };

                var resultA = await userManager.CreateAsync(userA, "admin1234!");

                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                if (!await roleManager.RoleExistsAsync("User"))
                {
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }

                if (resultA.Succeeded)
                {

                    await userManager.AddToRoleAsync(userA, "Admin");
                    User user1 = new User()
                    {
                        Username = "Admin",
                        Email = "admin@admin.com",
                        Password = "admin1234!",
                        UserIdentityId = userA.Id,
                        UserIdentity = userA
                    };

                    Users.Add(user1);
                    await SaveChangesAsync();
                }

            }

            var user = new IdentityUser { UserName = "ivan@abv.bg", Email = "ivan@abv.bg" };
            var result = await userManager.CreateAsync(user, "Ivan123!");


            var user2 = new IdentityUser { UserName = "elica@abv.bg", Email = "elica@abv.bg" };
            var result2 = await userManager.CreateAsync(user2, "Elica123!");


            var user3 = new IdentityUser { UserName = "teo@abv.bg", Email = "teo@abv.bg" };
            var result3 = await userManager.CreateAsync(user3, "Teo123!");


            if (Users.Count() <= 1)
            {

                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync("User"))
                {
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }

                if (result.Succeeded)
                {

                    await userManager.AddToRoleAsync(user, "User");
                    User user1 = new User()
                    {
                        Username = "Ivan Vulkov",
                        Email = "ivan@abv.bg",
                        Password = "Ivan123!",
                        UserIdentityId = user.Id,
                        UserIdentity = user,
                        ProfilePicture = "/Images/Ivan.jpg"
                    };

                    Users.Add(user1);
                    await SaveChangesAsync();
                    Comments.AddRange(
                    new Comment() { Stars = 4, OfferId = 1, Content = "Чудесно преживяване!", UserId = user1.Id },
                    new Comment() { Stars = 2, OfferId = 2, Content = "Можеше и по-добра организация!", UserId = user1.Id },
                    new Comment() { Stars = 5, OfferId = 3, Content = "Разбиха очакванията!", UserId = user1.Id }
                    );
                }


                if (!await roleManager.RoleExistsAsync("User"))
                {
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }

                if (result2.Succeeded)
                {

                    await userManager.AddToRoleAsync(user, "User");
                    User user1 = new User()
                    {
                        Username = "ЕlicaBG99",
                        Email = "elica@abv.bg",
                        Password = "elica123!",
                        UserIdentityId = user2.Id,
                        UserIdentity = user2,
                        ProfilePicture = "/Images/dog.jpg"
                    };
                    Users.Add(user1);

                    await SaveChangesAsync();

                    Comments.AddRange(
                    new Comment() { Stars = 3, OfferId = 1, Content = "Беше интересно, но 5 часа в пустинята никога отново!", UserId = user1.Id },
                    new Comment() { Stars = 1, OfferId = 2, Content = "Ужас!", UserId = user1.Id },
                    new Comment() { Stars = 4, OfferId = 3, Content = "Препоръчвам!", UserId = user1.Id }
                    );
                }

                if (result3.Succeeded)
                {

                    await userManager.AddToRoleAsync(user3, "User");
                    User user1 = new User()
                    {
                        Username = "Теодор Узунов",
                        Email = "teo@abv.bg",
                        Password = "Teo123!",
                        UserIdentityId = user3.Id,
                        UserIdentity = user3,
                        ProfilePicture = "/Images/ken.jpg"

                    };

                    Users.Add(user1);
                    await SaveChangesAsync();

                    Comments.AddRange(
                    new Comment() { Stars = 1, OfferId = 1, Content = "Откраднаха ми портфейла!", UserId = user1.Id },
                    new Comment() { Stars = 2, OfferId = 2, Content = "Леглото в хотела беше продунено!", UserId = user1.Id },
                    new Comment() { Stars = 5, OfferId = 3, Content = "Много моменти за цял живот!", UserId = user1.Id }
                    );
                }
                await SaveChangesAsync();
            }

        }

    }
}
