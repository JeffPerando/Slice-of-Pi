using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Main.Data;
using Main.Areas.Identity.Data;
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
using Main.Services.Concrete;
using Main.Services.Abstract;
using Main.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
builder.Configuration.AddUserSecrets<CrimeUserSecrets>();

//MainIdentityDbContextConnection
// Add services to the container.
var connectionStringID = builder.Configuration.GetConnectionString("MainIdentityDbContextConnection");
var connectionStringApp = builder.Configuration.GetConnectionString("ApplicationDbContextConnection");

//Make singletons

var crimeAPIService = new CrimeAPIService();
crimeAPIService.SetCredentials(builder.Configuration["apiFBIKey"]);

var emailService = new EmailService("Slice of Pi, LLC.", "sliceofpi.cs46x", builder.Configuration["EmailPW"]);
emailService.LogIn();

var userVerifier = new UserVerifierService(emailService);

//DB stuff

builder.Services.AddDbContext<MainIdentityDbContext>(options =>
    options.UseSqlServer(connectionStringID));

builder.Services.AddDbContext<CrimeDbContext>(options =>
    options.UseSqlServer(connectionStringApp));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MainIdentityDbContext>();

//Registration w/internal things

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ICrimeAPIService>(crimeAPIService);
builder.Services.AddSingleton<IEmailService>(emailService);
builder.Services.AddSingleton<IUserVerifierService>(userVerifier);

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
<<<<<<< HEAD
    defaults: new { controller = "Home", action = "GetListStates" });
=======
    defaults: new {controller = "Home", action = "GetListStates"});
>>>>>>> 43d6b077a43d216ea66a2342ebe3e387d79474bc

app.MapControllerRoute(
    name: "API States",
    pattern: "apiv3/FBI/StateStats",
    defaults: new { controller = "Home", action = "GetSafestState" });

app.MapControllerRoute(
    name: "API Cities",
<<<<<<< HEAD
    pattern: "apiv3/FBI/GetCityStats",
    defaults: new { controller = "Crime", action = "GetCrimeStats" });

app.MapControllerRoute(
    name: "API State stats",
    pattern: "/apiv3/FBI/StateCrimeStats",
    defaults: new { controller = "Crime", action = "GetSingleStateStats" });


app.MapControllerRoute(
    name: "API State stats",
    pattern: "/apiv3/FBI/CrimeStateList",
    defaults: new { controller = "Crime", action = "GetStateList" });
=======
    pattern: "/apiv3/FBI/GetCityStats",
    defaults: new {controller = "Crime", action = "GetCrimeStats"});

app.MapControllerRoute(
    name: "API Cities Trends",
    pattern: "/apiv3/FBI/GetCityTrends",
    defaults: new {controller = "Crime", action = "GetCrimeTrends"});

app.MapControllerRoute(
    name: "API Site Forms",
    pattern: "/apiv3/forms/{id?}",
    defaults: new { controller = "Form", action = "GetForm"});

//app.MapControllerRoute(
//    name: "City Stats",
//    pattern: "{controller=Crime}/{action=CrimeStats}/{cityName?}/{stateAbbrev?}");
>>>>>>> 43d6b077a43d216ea66a2342ebe3e387d79474bc

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
