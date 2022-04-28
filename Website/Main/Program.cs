using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Main.Data;
using Main.Areas.Identity.Data;
using Main.DAL.Abstract;
using Main.DAL.Concrete;
//using System.Data.SqlClient;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity.UI;
//using Microsoft.AspNetCore.HttpsPolicy;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
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

//DB stuff

builder.Services.AddDbContext<MainIdentityDbContext>(options =>
    options.UseSqlServer(connectionStringID));

builder.Services.AddDbContext<CrimeDbContext>(options =>
    options.UseSqlServer(connectionStringApp));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<MainIdentityDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;

    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 12;

    options.User.RequireUniqueEmail = true;

});

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(1);
});


//Make singletons and register internal services

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddHttpClient<IWebService, WebService>();
//No, this is not redundant.
builder.Services.AddScoped<IWebService, WebService>();
builder.Services.AddScoped<ISiteUserService, SiteUserService>();
builder.Services.AddScoped<ICrimeAPIService, CrimeAPIService>();
builder.Services.AddScoped<ICrimeAPIv2, FBIService>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IUserVerifierService, UserVerifierService>();
builder.Services.AddScoped<IReCaptchaService, ReCaptchaV3Service>();
builder.Services.AddScoped<IHousingAPI, ATTOMService>();
builder.Services.AddScoped<IHousePriceCalcService, HousePriceCalcService>();


//BUILD. THE. APP.
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
    name: "API Site Forms",
    pattern: "/api/forms/{id?}",
    defaults: new { controller = "Form", action = "GetForm"});

app.MapControllerRoute(
    name: "API User State Search History",
    pattern: "/api/SearchHistory/StateCrime",
    defaults: new { controller = "API", action = "StateCrimeSearchResults" });

//app.MapControllerRoute(
//    name: "City Stats",
//    pattern: "{controller=Crime}/{action=CrimeStats}/{cityName?}/{stateAbbrev?}");

app.MapControllerRoute(
    name: "API List States",
    pattern: "/apiv/FBI/Listings",
    defaults: new { controller = "ATTOM", action = "Listings" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
