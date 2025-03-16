﻿// <auto-generated />
using System;
using Explorers_Haven.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Explorers_Haven.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250315225757_updateValues4")]
    partial class updateValues4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Explorers_Haven.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CoverImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.ToTable("Activites");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CoverImage = "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243536/Egypt_geyymk.jpg",
                            Name = "Camel riding",
                            OfferId = 1
                        },
                        new
                        {
                            Id = 2,
                            CoverImage = "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243536/Egypt_geyymk.jpg",
                            Name = "Sightseeing",
                            OfferId = 1
                        },
                        new
                        {
                            Id = 3,
                            Name = "Sightseeing",
                            OfferId = 2
                        },
                        new
                        {
                            Id = 4,
                            Name = "Sightseeing",
                            OfferId = 3
                        },
                        new
                        {
                            Id = 5,
                            Name = "Archery",
                            OfferId = 3
                        });
                });

            modelBuilder.Entity("Explorers_Haven.Models.Amenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Amenities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Parking places"
                        });
                });

            modelBuilder.Entity("Explorers_Haven.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("OfferId")
                        .HasColumnType("int");

                    b.Property<string>("OfferName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PeopleCount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly?>("StartDate")
                        .HasColumnType("date");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<decimal?>("YoungOldPeopleCount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("Explorers_Haven.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("OfferId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Explorers_Haven.Models.Favorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("OfferId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("Explorers_Haven.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BackImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Clicks")
                        .HasColumnType("int");

                    b.Property<string>("CoverImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Disc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("DurationDays")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("LastDate")
                        .HasColumnType("date");

                    b.Property<int?>("MaxPeople")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("Rating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly?>("StartDate")
                        .HasColumnType("date");

                    b.Property<int?>("StayId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StayId");

                    b.HasIndex("UserId");

                    b.ToTable("Offers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BackImage = "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741541997/Egypt1_bzftps.avif",
                            CoverImage = "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243536/Egypt_geyymk.jpg",
                            Disc = "Travel across Egypt and cruise down the Nile River, tour the pyramids of Giza.",
                            DurationDays = 4,
                            LastDate = new DateOnly(2025, 4, 8),
                            MaxPeople = 20,
                            Name = "Egypt",
                            Price = 100m,
                            StartDate = new DateOnly(2025, 4, 1),
                            StayId = 1
                        },
                        new
                        {
                            Id = 2,
                            CoverImage = "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243527/Poland_heknwf.jpg",
                            Name = "Poland",
                            Price = 20m,
                            StayId = 2
                        },
                        new
                        {
                            Id = 3,
                            CoverImage = "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243521/Germany_iifb9a.jpg",
                            Name = "Germany",
                            Price = 500m,
                            StayId = 3
                        });
                });

            modelBuilder.Entity("Explorers_Haven.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("OfferId")
                        .HasColumnType("int");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Explorers_Haven.Models.Stay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Disc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Stars")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Stays");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Disc = "This Luxurious Premium Ultra all-inclusive resort in Hurghada offers only suites and villas with beachfront accommodation with total landscape area of 255.000 m2. It features 1km private sandy beach, 30 Swimming pools (9 types), 1 main buffet restaurant, 7 a-la-carte restaurants, 14 bars and free Wi-Fi in the entire property. This 5-star hotel offers private beach and pool cabanas upon request.",
                            Image = "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741542028/EgyptHotel_kc6xak.jpg",
                            Name = "Megawish Hotel",
                            Price = 100m,
                            Stars = 5
                        },
                        new
                        {
                            Id = 2,
                            Name = "InterContinental Warsaw Hotel"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Mitte Hotel"
                        });
                });

            modelBuilder.Entity("Explorers_Haven.Models.StayAmenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AmenityId")
                        .HasColumnType("int");

                    b.Property<int?>("StayId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AmenityId");

                    b.HasIndex("StayId");

                    b.ToTable("StayAmenity");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AmenityId = 1,
                            StayId = 1
                        });
                });

            modelBuilder.Entity("Explorers_Haven.Models.Transport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Transports");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Plane"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Train"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Boat"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Custom"
                        });
                });

            modelBuilder.Entity("Explorers_Haven.Models.Travel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateFinish")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Finish")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.Property<string>("Start")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransportId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.HasIndex("TransportId");

                    b.ToTable("Travels");

                    b.HasData(
                        new
                        {
                            Id = 7,
                            DateFinish = new DateTime(2025, 3, 10, 8, 30, 0, 0, DateTimeKind.Unspecified),
                            DateStart = new DateTime(2025, 3, 9, 6, 30, 0, 0, DateTimeKind.Unspecified),
                            Finish = "Cairo",
                            OfferId = 1,
                            Start = "Sofia",
                            TransportId = 2
                        },
                        new
                        {
                            Id = 1,
                            DateFinish = new DateTime(2025, 3, 10, 8, 30, 0, 0, DateTimeKind.Unspecified),
                            DateStart = new DateTime(2025, 3, 9, 6, 30, 0, 0, DateTimeKind.Unspecified),
                            Finish = "Cairo",
                            OfferId = 1,
                            Start = "Sofia",
                            TransportId = 3
                        },
                        new
                        {
                            Id = 2,
                            DateFinish = new DateTime(2025, 3, 16, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            DateStart = new DateTime(2025, 3, 15, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Finish = "Sofia",
                            OfferId = 1,
                            Start = "Cairo",
                            TransportId = 4
                        },
                        new
                        {
                            Id = 3,
                            Finish = "Warsaw",
                            OfferId = 2,
                            Start = "Sofia",
                            TransportId = 1
                        },
                        new
                        {
                            Id = 4,
                            Finish = "Sofia",
                            OfferId = 2,
                            Start = "Warsaw",
                            TransportId = 1
                        },
                        new
                        {
                            Id = 5,
                            Finish = "Berlin",
                            OfferId = 3,
                            Start = "Sofia",
                            TransportId = 1
                        },
                        new
                        {
                            Id = 6,
                            Finish = "Sofia",
                            OfferId = 3,
                            Start = "Berlin",
                            TransportId = 1
                        });
                });

            modelBuilder.Entity("Explorers_Haven.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserIdentityId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("UserIdentityId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Explorers_Haven.Models.Activity", b =>
                {
                    b.HasOne("Explorers_Haven.Models.Offer", "Offer")
                        .WithMany("Activities")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("Explorers_Haven.Models.Booking", b =>
                {
                    b.HasOne("Explorers_Haven.Models.Offer", "Offer")
                        .WithMany("Bookings")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Explorers_Haven.Models.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Offer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Explorers_Haven.Models.Comment", b =>
                {
                    b.HasOne("Explorers_Haven.Models.Offer", "Offer")
                        .WithMany("Comments")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Explorers_Haven.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Offer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Explorers_Haven.Models.Favorite", b =>
                {
                    b.HasOne("Explorers_Haven.Models.Offer", "Offer")
                        .WithMany("Favorites")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Explorers_Haven.Models.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Offer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Explorers_Haven.Models.Offer", b =>
                {
                    b.HasOne("Explorers_Haven.Models.Stay", "Stay")
                        .WithMany("Offers")
                        .HasForeignKey("StayId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Explorers_Haven.Models.User", "User")
                        .WithMany("Offers")
                        .HasForeignKey("UserId");

                    b.Navigation("Stay");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Explorers_Haven.Models.Rating", b =>
                {
                    b.HasOne("Explorers_Haven.Models.Offer", "Offer")
                        .WithMany("Ratings")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Explorers_Haven.Models.User", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Offer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Explorers_Haven.Models.StayAmenity", b =>
                {
                    b.HasOne("Explorers_Haven.Models.Amenity", "Amenity")
                        .WithMany("StayAmenities")
                        .HasForeignKey("AmenityId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Explorers_Haven.Models.Stay", "Stay")
                        .WithMany("StayAmenities")
                        .HasForeignKey("StayId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Amenity");

                    b.Navigation("Stay");
                });

            modelBuilder.Entity("Explorers_Haven.Models.Travel", b =>
                {
                    b.HasOne("Explorers_Haven.Models.Offer", "Offer")
                        .WithMany("Travels")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Explorers_Haven.Models.Transport", "Transport")
                        .WithMany("Travels")
                        .HasForeignKey("TransportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");

                    b.Navigation("Transport");
                });

            modelBuilder.Entity("Explorers_Haven.Models.User", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "UserIdentity")
                        .WithOne()
                        .HasForeignKey("Explorers_Haven.Models.User", "UserIdentityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserIdentity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Explorers_Haven.Models.Amenity", b =>
                {
                    b.Navigation("StayAmenities");
                });

            modelBuilder.Entity("Explorers_Haven.Models.Offer", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Bookings");

                    b.Navigation("Comments");

                    b.Navigation("Favorites");

                    b.Navigation("Ratings");

                    b.Navigation("Travels");
                });

            modelBuilder.Entity("Explorers_Haven.Models.Stay", b =>
                {
                    b.Navigation("Offers");

                    b.Navigation("StayAmenities");
                });

            modelBuilder.Entity("Explorers_Haven.Models.Transport", b =>
                {
                    b.Navigation("Travels");
                });

            modelBuilder.Entity("Explorers_Haven.Models.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Comments");

                    b.Navigation("Favorites");

                    b.Navigation("Offers");

                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
