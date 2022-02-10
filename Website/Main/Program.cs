using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Main.Data;
using Main.Areas.Identity.Data;
using MyApplication.Data;
using Main.DAL.Abstract;
using Main.DAL.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection");
builder.Services.AddDbContext<MainIdentityDbContext>(options =>
    options.UseSqlServer(connectionString));builder.Services.AddDbContext<CrimeDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<CrimeDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICrimeAPIService,CrimeAPIService>();

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
    pattern: "apiv3/FBI/StateList",
    defaults: new {controller = "Home", action= "GetListStates"});

app.MapControllerRoute(
    name: "API States",
    pattern: "apiv3/FBI/StateStats",
    defaults: new {controller = "Home", action= "GetSafestState"});

app.MapControllerRoute(
    name: "API Cities",
    pattern: "apiv3/FBI/GetCityStats",
    defaults: new {controller = "Crime", action= "GetCrimeStats"});

//app.MapControllerRoute(
//    name: "City Stats",
//    pattern: "{controller=Crime}/{action=CrimeStats}/{cityName?}/{stateAbbrev?}");
    
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
