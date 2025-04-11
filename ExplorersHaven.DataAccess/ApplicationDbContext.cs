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
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Offer> Offers { get; set; }
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
                    Name = "Самолет"
                });
            modelBuilder.Entity<Transport>().HasData(
                new Transport
                {
                    Id = 2,
                    Name = "Влак"
                });
            modelBuilder.Entity<Transport>().HasData(
                new Transport
                {
                    Id = 3,
                    Name = "Ферибот"
                });
            modelBuilder.Entity<Transport>().HasData(
                new Transport
                {
                    Id = 4,
                    Name = "Личен Транспорт"
                });

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 1,
                    Icon = "/Images/parking.svg",
                    Name = "Места за паркиране"
                }
                );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 2,
                    Icon = "/Images/kitchen.svg",
                    Name = "Кухня"
                }
                );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 3,
                    Icon = "/Images/usefortheunabled.svg",
                    Name = "Подходящ за хора с увреждания"
                }
                );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 4,
                    Icon = "/Images/wifi.svg",
                    Name = "Безплатен Wifi"
                }
                );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 5,
                    Icon = "/Images/familyrooms.svg",
                    Name = "Семейни стаи"
                }
                );



            modelBuilder.Entity<Offer>().HasData(
                new Offer {
                    Id = 1,
                    Name ="Egypt",
                    CoverImage= "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243536/Egypt_geyymk.jpg",
                    BackImage= "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741541997/Egypt1_bzftps.avif",
                    Price=759,
                    StayId = 1,
                    Disc = "Пътуване из Египет и круиз по река Нил, обиколка на пирамидите в Гиза.",
                    DurationDays = 5,
                    StartDate = new DateOnly(2025, 4, 1),
                    LastDate = new DateOnly(2025, 4, 8),
                    Discount = 20,
                    MaxPeople = 8,
                    Rating = 3,
                    DefaultRating = 3

                }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel { 
                    Id = 1,
                    Start = "София",
                    Finish = "Кайро",
                    DurationDays = 1,
                    OfferId = 1,
                    TransportId = 1,
                    Arrival = true
                }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel {
                    Id = 2,
                    Start = "Кайро",
                    Finish = "София",
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
                    Price = 500,
                    Stars = 5,
                    Disc= "Този луксозен премиум ултра ол инклузив курорт в Хургада предлага само апартаменти и вили с настаняване на брега на морето с обща площ от 255 000 m2. Разполага с 1 км частен пясъчен плаж, 30 плувни басейна (9 вида), 1 основен ресторант на шведска маса, 7 а-ла-карт ресторанта, 14 бара и безплатен Wi-Fi в целия имот. Този 5-звезден хотел предлага частен плаж и кабинки до басейна при заявка.",
                    Image = "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741542028/EgyptHotel_kc6xak.jpg"
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
            modelBuilder.Entity<StayAmenity>().HasData(
                new StayAmenity
                {
                    Id = 2,
                    StayId = 1,
                    AmenityId = 2
                }
                );
            modelBuilder.Entity<StayAmenity>().HasData(
                new StayAmenity
                {
                    Id = 3,
                    StayId = 1,
                    AmenityId = 3
                }
                );
            modelBuilder.Entity<StayAmenity>().HasData(
                new StayAmenity
                {
                    Id = 4,
                    StayId = 2,
                    AmenityId = 4
                }
                );
            modelBuilder.Entity<StayAmenity>().HasData(
                new StayAmenity
                {
                    Id = 5,
                    StayId = 3,
                    AmenityId = 4
                }
                );
            modelBuilder.Entity<StayAmenity>().HasData(
                new StayAmenity
                {
                    Id = 6,
                    StayId = 4,
                    AmenityId = 4
                }
                );
            modelBuilder.Entity<StayAmenity>().HasData(
                new StayAmenity
                {
                    Id = 7,
                    StayId = 2,
                    AmenityId = 1
                }
                );
            modelBuilder.Entity<StayAmenity>().HasData(
                new StayAmenity
                {
                    Id = 8,
                    StayId = 3,
                    AmenityId = 3
                }
                );
            modelBuilder.Entity<StayAmenity>().HasData(
                new StayAmenity
                {
                    Id = 9,
                    StayId = 3,
                    AmenityId = 2
                }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity { 
                    Id = 1,
                    Name = "Яздене на камила",
                    CoverImage = "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243536/Egypt_geyymk.jpg",
                    OfferId = 1 }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity { Id = 2, Name = "Разглеждане на забележителности",
                    CoverImage = "/Images/sighteg.jpg",
                    OfferId = 1 }
                );

            //offer 2
            modelBuilder.Entity<Offer>().HasData(
                new Offer { 
                    Id = 2,
                    Name = "Полша",
                    CoverImage= "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243527/Poland_heknwf.jpg",
                    BackImage = "/Images/polback.jpg",
                    Price = 300,
                    StayId = 2,
                    Disc = "Полша предлага комбинация от оживени градове, богата история и зашеметяваща природа. Не пропускайте да разгледате Краков и Варшава, опитайте традиционни пироги и използвайте влакове за лесно пътуване между градовете.",
                    DurationDays = 7,
                    StartDate = new DateOnly(2025, 4, 1),
                    LastDate = new DateOnly(2025, 5, 1),
                    Discount = 0,
                    MaxPeople = 12,
                    Rating = 4,
                    DefaultRating = 4
                }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel {
                    Id = 3,
                    Start = "София",
                    Finish = "Варшава",
                    DurationDays = 1,
                    OfferId = 2,
                    TransportId = 1,
                    Arrival = true
                }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel { 
                    Id = 4,
                    Start = "Варшава",
                    Finish = "София",
                    DurationDays = 1,
                    OfferId = 2,
                    TransportId = 1,
                    Arrival = false
                }
                );
            modelBuilder.Entity<Stay>().HasData(
                new Stay { Id = 2,
                    Name = "InterContinental Warsaw Hotel",
                    Price = 120,
                    Stars = 5,
                    Disc = "InterContinental Warszawa е 5-звезден хотел в центъра на Варшава, на 500 метра от централната гара на Варшава. Той разполага с луксозни климатизирани стаи и уелнес център, разположен на 43-ия и 44-ия етаж. Всички стаи в InterContinental са оборудвани с удобства за приготвяне на чай и кафе и минибар.",
                    Image = "/Images/WarsawHotel.jpg"

                }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity { Id = 3,
                    Name = "Разглеждане на забележителности",
                    CoverImage = "/Images/sightpol.jpg",
                    OfferId = 2
                }
                );

            //offer 3
            modelBuilder.Entity<Offer>().HasData(
                new Offer { 
                    Id = 3,
                    Name = "Германия",
                    CoverImage= "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243521/Germany_iifb9a.jpg",
                    BackImage = "/Images/gerback.jpg",
                    Price = 950,
                    StayId = 3,
                    Disc = "Германия съчетава модерна ефективност с дълбока история – изследвайте културата на Берлин, бирените градини на Мюнхен и приказни градове като Ротенбург.",
                    DurationDays = 7,
                    StartDate = new DateOnly(2025, 4, 1),
                    LastDate = new DateOnly(2025, 6, 1),
                    Discount = 15,
                    MaxPeople = 16,
                    Rating = 5,
                    DefaultRating = 5
                }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel {
                    Id = 5,
                    Start = "София", 
                    Finish = "Берлин",
                    OfferId = 3, 
                    TransportId = 1,
                    DurationDays = 1,
                    Arrival = true
                }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel {
                    Id = 6,
                    Start = "Берлин",
                    Finish = "София",
                    OfferId = 3, 
                    TransportId = 1,
                    DurationDays = 1,
                    Arrival = false

                }
                );
            modelBuilder.Entity<Stay>().HasData(
                new Stay {
                    Id = 3,
                    Price = 500,
                    Name = "Charles Hotel",
                    Stars = 5,
                    Disc = "Разположен в зеления квартал Lenbachgärten и близо до историческия Königsplatz, безпроблемният елегантен хотел Charles в Мюнхен е мястото, където съвременният стил среща традиционното баварско гостоприемство.",
                    Image = "/Images/gerHotel.jpg"

                }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity { Id = 4,
                    Name = "Разглеждане на забележителности",
                    OfferId = 3,
                    CoverImage = "/Images/sightger.jpg",
                }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity { Id = 5,
                    Name = "Стрелба с лък",
                    OfferId = 3,
                    CoverImage = "/Images/archery.jpg",
                }
                );
            //test offer 4
            modelBuilder.Entity<Offer>().HasData(
                new Offer
                {
                    Id = 4,
                    Name = "Франция",
                    CoverImage = "/Images/paris.jpg",
                    BackImage = "/Images/frback.jpg",
                    Price = 800,
                    StayId = 4,
                    Disc = "Във Франция всичко е свързано с изкуство, храна и чар – Париж очарова, но не пропускайте винени региони като Бордо или лавандуловите полета на Прованс.",
                    DurationDays = 6,
                    StartDate = new DateOnly(2025, 4, 1),
                    LastDate = new DateOnly(2025, 4, 8),
                    Discount = 35,
                    MaxPeople = 10,
                    Rating = 3,
                    DefaultRating = 3
                }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel
                {
                    Id = 7,
                    Start = "София",
                    Finish = "Париж",
                    OfferId = 4,
                    TransportId = 1,
                    DurationDays = 1,
                    Arrival = true
                }
                );
            modelBuilder.Entity<Travel>().HasData(
                new Travel
                {
                    Id = 8,
                    Start = "Париж",
                    Finish = "София",
                    OfferId = 4,
                    TransportId = 1,
                    DurationDays = 1,
                    Arrival = false

                }
                );
            modelBuilder.Entity<Stay>().HasData(
                new Stay
                {
                    Id = 4,
                    Price = 400,
                    Name = "Paris Gare de Lyon hotel",
                    Stars = 4,
                    Disc = "12-ти район на Париж се нуждаеше само от едно нещо, за да бъде още по-страхотен – умопомрачен разкошен бутиков хотел, покрит със зашеметяващ бар на покрива. И ние не се спираме само на най-добрите гледки към силуета на Париж. Мегаудобни легла, възглавници, подобни на облак, и артистична всекидневна са част от пакета.",
                    Image = "/Images/ParisHotel.jpg"

                }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity
                {
                    Id = 6,
                    Name = "Разглеждане на забележителности",
                    OfferId = 4,
                    CoverImage = "/Images/sightfr.jpg",
                }
                );
            modelBuilder.Entity<Models.Activity>().HasData(
                new Models.Activity
                {
                    Id = 7,
                    Name = "Пътуване до винени региони",
                    OfferId = 4,
                    CoverImage = "/Images/winefield.jpg",
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

                b.HasOne(a => a.User)
                .WithMany(a => a.Activities)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Stay>(b =>
            {
                
                b.HasKey(e => e.Id);

                
                b.Property(e => e.Name)
                    .IsRequired();


                b.HasMany(e => e.Offers)
                 .WithOne(b => b.Stay)
                 .HasForeignKey(e => e.StayId)
                 .OnDelete(DeleteBehavior.ClientCascade);

                b.HasOne(a => a.User)
                .WithMany(a => a.Stays)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

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
                 .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(a => a.User)
                .WithMany(a => a.Travels)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);
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

                var resultA = await userManager.CreateAsync(userA, "Admin123!");

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
                        ProfilePicture = "/Images/def.jpg",
                        Username = "Admin",
                        Email = "admin@admin.com",
                        Password = "Admin123!",
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
                    new Comment() { Stars = 2, OfferId = 2, Content = "Леглото в хотела беше продънено!", UserId = user1.Id },
                    new Comment() { Stars = 5, OfferId = 3, Content = "Много моменти за цял живот!", UserId = user1.Id }
                    );
                }
                await SaveChangesAsync();
            }
        }

    }
}
