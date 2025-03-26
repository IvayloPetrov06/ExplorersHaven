using Microsoft.EntityFrameworkCore;
using Explorers_Haven.Models;
using Explorers_Haven.Core;
using Explorers_Haven.DataAccess;
using Microsoft.AspNetCore.Identity;
using Explorers_Haven.DataAccess.Repository;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using CloudinaryDotNet;
//class Program
//{
//static async Task Main(string[] args)
//{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllersWithViews();

    var connection = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connection, b => b.MigrationsAssembly("Explorers_Haven.DataAccess")));

   // builder.Services.AddDbContext<ApplicationDbContext>
    //(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    // b => b.MigrationsAssembly("Explorers_Haven.DataAccess")));

    builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
    //builder.Services.AddDefaultIdentity<IdentityUser>()
    //    .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

    // Add services to the container.
    builder.Services.AddControllersWithViews();
    builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    builder.Services.AddScoped(typeof(IActivityService), typeof(ActivityService));
    builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
    builder.Services.AddScoped(typeof(IStayService), typeof(StayService));
    builder.Services.AddScoped(typeof(IOfferService), typeof(OfferService));
    builder.Services.AddScoped(typeof(ITravelService), typeof(TravelService));
    builder.Services.AddScoped(typeof(IBookingService), typeof(BookingService));
    builder.Services.AddScoped(typeof(IRatingService), typeof(RatingService));
    builder.Services.AddScoped(typeof(IAmenityService), typeof(AmenityService));
    builder.Services.AddScoped(typeof(IFavoriteService), typeof(FavoriteService));
    builder.Services.AddScoped(typeof(ITransportService), typeof(TransportService));
    builder.Services.AddScoped(typeof(ICommentService), typeof(CommentService));
    builder.Services.AddScoped(typeof(IStayAmenityService), typeof(StayAmenityService));
    builder.Services.AddScoped<CloudinaryService>();
    builder.Services.AddRazorPages();

    var cloudinarySettings = builder.Configuration.GetSection("Cloudinary");
    var cloudinaryAccount = new Account(
        cloudinarySettings["CloudName"],
        cloudinarySettings["ApiKey"],
        cloudinarySettings["ApiSecret"]
    );
    var cloudinary = new Cloudinary(cloudinaryAccount);
    builder.Services.AddSingleton(cloudinary);

    //builder.Services.AddDbContext<ApplicationDbContext>
    //    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));LocalDb
    

    //var connection = builder.Configuration.GetConnectionString("DefaultConnection");
    //builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //    options.UseSqlServer(connection, b => b.MigrationsAssembly("Explorers_Haven.DataAccess")));


    //builder.Services.AddDefaultIdentity<IdentityUser>()
    //    .AddEntityFrameworkStores<ApplicationDbContext>();

    


    var app = builder.Build();
/*using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Seed();
}*/


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapRazorPages();

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        await CreateRoles(services);
    }

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        await CreateAdmin(services);
    }

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

    static async Task CreateRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roleNames = { "Admin", "User"};

        foreach (var roleName in roleNames)
        {

            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));

            }
        }
    }
    static async Task CreateAdmin(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var adminEmail = "admin7@admin.com";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);





        if (adminUser == null)

        {

            var user = new IdentityUser { UserName = "admin7@admin.com", Email = adminEmail };

            var result = await userManager.CreateAsync(user, "AdminPassword1123!");

            if (result.Succeeded)

            {

                await userManager.AddToRoleAsync(user, "Admin"); // Добавя роля "Admin" 

            }

        }
    }
//}
//}


