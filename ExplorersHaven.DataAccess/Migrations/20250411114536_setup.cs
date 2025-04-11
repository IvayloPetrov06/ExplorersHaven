using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Explorers_Haven.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class setup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UserIdentityId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_AspNetUsers_UserIdentityId",
                        column: x => x.UserIdentityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Disc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Stars = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stays_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxPeople = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DurationDays = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    LastDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Disc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DefaultRating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RealRating = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Clicks = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    StayId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Stays_StayId",
                        column: x => x.StayId,
                        principalTable: "Stays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StayAmenity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmenityId = table.Column<int>(type: "int", nullable: true),
                    StayId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StayAmenity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StayAmenity_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StayAmenity_Stays_StayId",
                        column: x => x.StayId,
                        principalTable: "Stays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Activites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activites_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeopleCount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    YoungOldPeopleCount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OfferName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DurationDays = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    OfferId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    OfferId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    OfferName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfferId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    OfferId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Travels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Finish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationDays = table.Column<int>(type: "int", nullable: true),
                    Arrival = table.Column<bool>(type: "bit", nullable: true),
                    DateStart = table.Column<DateOnly>(type: "date", nullable: true),
                    DateFinish = table.Column<DateOnly>(type: "date", nullable: true),
                    TransportId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Travels_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Travels_Transports_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Travels_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Amenities",
                columns: new[] { "Id", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "/Images/parking.svg", "Места за паркиране" },
                    { 2, "/Images/kitchen.svg", "Кухня" },
                    { 3, "/Images/usefortheunabled.svg", "Подходящ за хора с увреждания" },
                    { 4, "/Images/wifi.svg", "Безплатен Wifi" },
                    { 5, "/Images/familyrooms.svg", "Семейни стаи" }
                });

            migrationBuilder.InsertData(
                table: "Stays",
                columns: new[] { "Id", "Disc", "Image", "Name", "Price", "Stars", "UserId" },
                values: new object[,]
                {
                    { 1, "Този луксозен премиум ултра ол инклузив курорт в Хургада предлага само апартаменти и вили с настаняване на брега на морето с обща площ от 255 000 m2. Разполага с 1 км частен пясъчен плаж, 30 плувни басейна (9 вида), 1 основен ресторант на шведска маса, 7 а-ла-карт ресторанта, 14 бара и безплатен Wi-Fi в целия имот. Този 5-звезден хотел предлага частен плаж и кабинки до басейна при заявка.", "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741542028/EgyptHotel_kc6xak.jpg", "Megawish Hotel", 500m, 5, null },
                    { 2, "InterContinental Warszawa е 5-звезден хотел в центъра на Варшава, на 500 метра от централната гара на Варшава. Той разполага с луксозни климатизирани стаи и уелнес център, разположен на 43-ия и 44-ия етаж. Всички стаи в InterContinental са оборудвани с удобства за приготвяне на чай и кафе и минибар.", "/Images/WarsawHotel.jpg", "InterContinental Warsaw Hotel", 120m, 5, null },
                    { 3, "Разположен в зеления квартал Lenbachgärten и близо до историческия Königsplatz, безпроблемният елегантен хотел Charles в Мюнхен е мястото, където съвременният стил среща традиционното баварско гостоприемство.", "/Images/gerHotel.jpg", "Charles Hotel", 500m, 5, null },
                    { 4, "12-ти район на Париж се нуждаеше само от едно нещо, за да бъде още по-страхотен – умопомрачен разкошен бутиков хотел, покрит със зашеметяващ бар на покрива. И ние не се спираме само на най-добрите гледки към силуета на Париж. Мегаудобни легла, възглавници, подобни на облак, и артистична всекидневна са част от пакета.", "/Images/ParisHotel.jpg", "Paris Gare de Lyon hotel", 400m, 4, null }
                });

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Самолет" },
                    { 2, "Влак" },
                    { 3, "Ферибот" },
                    { 4, "Личен Транспорт" }
                });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "BackImage", "Clicks", "CoverImage", "DefaultRating", "Disc", "Discount", "DurationDays", "LastDate", "MaxPeople", "Name", "Price", "Rating", "RealRating", "StartDate", "StayId", "UserId" },
                values: new object[,]
                {
                    { 1, "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741541997/Egypt1_bzftps.avif", null, "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243536/Egypt_geyymk.jpg", 3m, "Пътуване из Египет и круиз по река Нил, обиколка на пирамидите в Гиза.", 20m, 5, new DateOnly(2025, 4, 8), 8m, "Egypt", 759m, 3m, null, new DateOnly(2025, 4, 1), 1, null },
                    { 2, "/Images/polback.jpg", null, "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243527/Poland_heknwf.jpg", 4m, "Полша предлага комбинация от оживени градове, богата история и зашеметяваща природа. Не пропускайте да разгледате Краков и Варшава, опитайте традиционни пироги и използвайте влакове за лесно пътуване между градовете.", 0m, 7, new DateOnly(2025, 5, 1), 12m, "Полша", 300m, 4m, null, new DateOnly(2025, 4, 1), 2, null },
                    { 3, "/Images/gerback.jpg", null, "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243521/Germany_iifb9a.jpg", 5m, "Германия съчетава модерна ефективност с дълбока история – изследвайте културата на Берлин, бирените градини на Мюнхен и приказни градове като Ротенбург.", 15m, 7, new DateOnly(2025, 6, 1), 16m, "Германия", 950m, 5m, null, new DateOnly(2025, 4, 1), 3, null },
                    { 4, "/Images/frback.jpg", null, "/Images/paris.jpg", 3m, "Във Франция всичко е свързано с изкуство, храна и чар – Париж очарова, но не пропускайте винени региони като Бордо или лавандуловите полета на Прованс.", 35m, 6, new DateOnly(2025, 4, 8), 10m, "Франция", 800m, 3m, null, new DateOnly(2025, 4, 1), 4, null }
                });

            migrationBuilder.InsertData(
                table: "StayAmenity",
                columns: new[] { "Id", "AmenityId", "StayId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 2 },
                    { 5, 4, 3 },
                    { 6, 4, 4 },
                    { 7, 1, 2 },
                    { 8, 3, 3 },
                    { 9, 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Activites",
                columns: new[] { "Id", "CoverImage", "Name", "OfferId", "UserId" },
                values: new object[,]
                {
                    { 1, "https://res.cloudinary.com/dkoshuv9z/image/upload/v1741243536/Egypt_geyymk.jpg", "Яздене на камила", 1, null },
                    { 2, "/Images/sighteg.jpg", "Разглеждане на забележителности", 1, null },
                    { 3, "/Images/sightpol.jpg", "Разглеждане на забележителности", 2, null },
                    { 4, "/Images/sightger.jpg", "Разглеждане на забележителности", 3, null },
                    { 5, "/Images/archery.jpg", "Стрелба с лък", 3, null },
                    { 6, "/Images/sightfr.jpg", "Разглеждане на забележителности", 4, null },
                    { 7, "/Images/winefield.jpg", "Пътуване до винени региони", 4, null }
                });

            migrationBuilder.InsertData(
                table: "Travels",
                columns: new[] { "Id", "Arrival", "DateFinish", "DateStart", "DurationDays", "Finish", "OfferId", "Start", "TransportId", "UserId" },
                values: new object[,]
                {
                    { 1, true, null, null, 1, "Кайро", 1, "София", 1, null },
                    { 2, false, null, null, 1, "София", 1, "Кайро", 1, null },
                    { 3, true, null, null, 1, "Варшава", 2, "София", 1, null },
                    { 4, false, null, null, 1, "София", 2, "Варшава", 1, null },
                    { 5, true, null, null, 1, "Берлин", 3, "София", 1, null },
                    { 6, false, null, null, 1, "София", 3, "Берлин", 1, null },
                    { 7, true, null, null, 1, "Париж", 4, "София", 1, null },
                    { 8, false, null, null, 1, "София", 4, "Париж", 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activites_OfferId",
                table: "Activites",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Activites_UserId",
                table: "Activites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_OfferId",
                table: "Bookings",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_OfferId",
                table: "Comments",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_OfferId",
                table: "Favorites",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_StayId",
                table: "Offers",
                column: "StayId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_UserId",
                table: "Offers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_OfferId",
                table: "Ratings",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StayAmenity_AmenityId",
                table: "StayAmenity",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_StayAmenity_StayId",
                table: "StayAmenity",
                column: "StayId");

            migrationBuilder.CreateIndex(
                name: "IX_Stays_UserId",
                table: "Stays",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_OfferId",
                table: "Travels",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_TransportId",
                table: "Travels",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_UserId",
                table: "Travels",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserIdentityId",
                table: "Users",
                column: "UserIdentityId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activites");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "StayAmenity");

            migrationBuilder.DropTable(
                name: "Travels");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropTable(
                name: "Stays");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
