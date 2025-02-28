using Microsoft.EntityFrameworkCore;
using Explorers_Haven.Models;
using Explorers_Haven.Core;
using Explorers_Haven.DataAccess;
using Microsoft.AspNetCore.Identity;
using Explorers_Haven.DataAccess.Repository;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;
using CloudinaryDotNet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IActivityService), typeof(ActivityService));
builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
builder.Services.AddScoped(typeof(IStayService), typeof(StayService));
builder.Services.AddScoped(typeof(IOfferService), typeof(OfferService));
builder.Services.AddScoped(typeof(ITravelService), typeof(TravelService));
builder.Services.AddScoped(typeof(ITripService), typeof(TripService));

builder.Services.AddScoped<CloudinaryService>();

var cloudinarySettings = builder.Configuration.GetSection("Cloudinary");
var cloudinaryAccount = new Account(
    cloudinarySettings["CloudName"],
    cloudinarySettings["ApiKey"],
    cloudinarySettings["ApiSecret"]
);
var cloudinary = new Cloudinary(cloudinaryAccount);
builder.Services.AddSingleton(cloudinary);

//builder.Services.AddDbContext<ApplicationDbContext>
//    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var connection = builder.Configuration.GetConnectionString("LocalDbConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connection, b => b.MigrationsAssembly("Explorers_Haven.DataAccess")));


//builder.Services.AddDefaultIdentity<IdentityUser>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();


var app = builder.Build();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Stay}/{action=Index}/{id?}");

app.Run();
