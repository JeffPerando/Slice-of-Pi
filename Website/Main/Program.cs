using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Main.Data;
using Main.Areas.Identity.Data;
using MyApplication.Data;
using Main.DAL.Abstract;
using Main.DAL.Concrete;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
//MainIdentityDbContextConnection
// Add services to the container.
var connectionStringID = builder.Configuration.GetConnectionString("MainIdentityDbContextConnection");
var connectionStringApp = builder.Configuration.GetConnectionString("ApplicationDbContextConnection");

builder.Services.AddDbContext<MainIdentityDbContext>(options =>
    options.UseSqlServer(connectionStringID));

builder.Services.AddDbContext<CrimeDbContext>(options =>
    options.UseSqlServer(connectionStringApp));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MainIdentityDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICrimeAPIService, CrimeAPIService>();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    name: "API List States",
    pattern: "/apiv3/FBI/StateList",
    defaults: new {controller = "Home", action= "GetListStates"});

app.MapControllerRoute(
    name: "API States",
    pattern: "/apiv3/FBI/StateStats",
    defaults: new { controller = "Home", action = "GetSafestState" });

app.MapControllerRoute(
    name: "API Cities",
    pattern: "/apiv3/FBI/GetCityStats",
    defaults: new {controller = "Crime", action= "GetCrimeStats"});

app.MapControllerRoute(
<<<<<<< HEAD
    name: "API State stats",
    pattern: "/apiv3/FBI/StateCrimeStats",
    defaults: new { controller = "Crime", action = "GetSingleStateStats" });


app.MapControllerRoute(
    name: "API State stats",
    pattern: "/apiv3/FBI/CrimeStateList",
    defaults: new { controller = "Crime", action = "GetStateList" }); 

=======
    name: "API Cities Trends",
    pattern: "/apiv3/FBI/GetCityTrends",
    defaults: new {controller = "Crime", action= "GetCrimeTrends"});

//app.MapControllerRoute(
//    name: "City Stats",
//    pattern: "{controller=Crime}/{action=CrimeStats}/{cityName?}/{stateAbbrev?}");
    
>>>>>>> dev/dev
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
