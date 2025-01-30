using Microsoft.EntityFrameworkCore;
using Explorers_Haven.Models;
using Explorers_Haven.Core;
using Explorers_Haven.DataAccess;
using Microsoft.AspNetCore.Identity;
using Explorers_Haven.DataAccess.Repository;
using Explorers_Haven.Core.IServices;
using Explorers_Haven.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IActivityService), typeof(ActivityService));
builder.Services.AddScoped(typeof(IApplicationUserService), typeof(ApplicationUserService));
builder.Services.AddScoped(typeof(IStayService), typeof(StayService));
builder.Services.AddScoped(typeof(ITravelogueService), typeof(TravelogueService));
builder.Services.AddScoped(typeof(ITravelService), typeof(TravelService));
builder.Services.AddScoped(typeof(ITripService), typeof(TripService));

//builder.Services.AddDbContext<ApplicationDbContext>
//    (options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connection, b => b.MigrationsAssembly("Explorers_Haven.DataAccess")));


//builder.Services.AddDefaultIdentity<IdentityUser>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Trip}/{action=ListTrips}/{id?}");

app.Run();
