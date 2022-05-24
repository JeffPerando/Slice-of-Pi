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
using MongoDB.Driver;
using MongoDB.Bson;
using System.Diagnostics;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

config.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
//config.AddUserSecrets<CrimeUserSecrets>();


//DB stuff
var connectionStringID =    config.GetConnectionString("MainIdentityDbContextConnection");
var connectionStringApp =   config.GetConnectionString("ApplicationDbContextConnection");
var connectionStringCache = config.GetConnectionString("APICacheDbContextConnection");

services.AddDbContext<MainIdentityDbContext>(options =>
    options.UseSqlServer(connectionStringID), ServiceLifetime.Transient);

services.AddDbContext<CrimeDbContext>(options =>
    options.UseSqlServer(connectionStringApp), ServiceLifetime.Transient);

//MongoDB

var mongo = MongoClientSettings.FromConnectionString(connectionStringCache);

var client = new MongoClient(connectionStringCache);
var database = client.GetDatabase("APICache");

services.AddSingleton<IMongoDatabase>(database);

//Rate limiting

services.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false;
    options.RealIpHeader = "X-Real-IP";
    options.ClientIdHeader = "X-ClientId";
    options.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint = "GET:/api/*",
            Period = "5s",
            Limit = 2,
        },
        new RateLimitRule
        {
            Endpoint = "POST:/api/*",
            Period = "5s",
            Limit = 2,
        }
    };
});

services.AddInMemoryRateLimiting();

//Identity

services.AddDatabaseDeveloperPageExceptionFilter();

services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<MainIdentityDbContext>();

services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedEmail = true;
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

services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(1);
});


//Make singletons and register internal services

services.AddControllersWithViews();
services.AddRazorPages().AddRazorRuntimeCompilation();


//These are needed for rate limiting. Yes I'm serious.

services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();


//Actual website services

services.AddHttpClient<IWebService, WebService>();
services.AddScoped<IWebService, WebService>(); //No, this is not redundant.
services.AddScoped<ISiteUserService, SiteUserService>();
services.AddScoped<IAPICacheService<FBICache>, APICacheService<FBICache>>();
services.AddScoped<IAPICacheService<ATTOMCache>, APICacheService<ATTOMCache>>();
services.AddScoped<ICrimeAPIService, CrimeAPIService>();
services.AddScoped<ICrimeAPIv2, FBIService>();
services.AddSingleton<IEmailService, EmailService>();
services.AddSingleton<IUserVerifierService, UserVerifierService>();
services.AddScoped<IReCaptchaService, ReCaptchaV3Service>();
services.AddScoped<IHousingAPI, ATTOMService>();
services.AddScoped<IHousePriceCalcService, HousePriceCalcService>();
services.AddScoped<IGoogleStreetViewAPIService, GoogleStreetViewAPIService>();
services.AddScoped<IBackendService, BackendService>();//Keep this one on the bottom so it has access to all other services(?)


//BUILD. THE. APP.
var app = builder.Build();

//Comment this out if you want a real stack trace
//app.UseExceptionHandler("/Home/Error");
app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");//can add {0}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseIpRateLimiting();

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
    pattern: "/api/FBI/Listings",
    defaults: new { controller = "ATTOM", action = "Listings" });


app.MapControllerRoute(
    name: "API Street View",
    pattern: "/api/ATTOM/StreetView",
    defaults: new { controller = "ATTOM", action = "StreetView" });

app.MapControllerRoute(
    name: "API Street Lookup",
    pattern: "/api/ATTOM/StreetViewLookUp",
    defaults: new { controller = "ATTOM", action = "StreetViewLookUp" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
