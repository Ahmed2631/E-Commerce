using BusinessLayer;
using BusinessLayer.RepositryImplementation;
using ECommerce.Helper;
using ECommerceDomains.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Stripe;
using BusinessLayer.DbInit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// Add the database context with SQL Server support
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false);

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.Configure<StripeBank>(builder.Configuration.GetSection("stripe"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    options => options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(4)
    ).AddDefaultTokenProviders().AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICategoryRepositry, CategoeyRepositry>();
builder.Services.AddScoped<IProductRepositry, ProductRepositry>();
builder.Services.AddScoped<IOrderDetailsRepositry,OrderDetailsRepositry >();
builder.Services.AddScoped<IOrderHeaderRepositry, OrderHeaderRepositry>();
builder.Services.AddScoped<IWishlistRepositry, WishlistRepositry>();
builder.Services.AddScoped<IShippingCartRepositry, ShippingCartRepositry>();
builder.Services.AddScoped<IDbInit, DbInit>();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();                                     // Ah2540$$

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

StripeConfiguration.ApiKey = builder.Configuration.GetSection("stripe:Secretkey").Get<string>();
DbSeed(); // Seed the database with initial data
app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "User",
    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");

app.Run();
 void DbSeed()
{
    using ( var Scope = app.Services.CreateScope())
    {
        var Db = Scope.ServiceProvider.GetRequiredService<IDbInit>();
        Db.InitializeDatabaseAsync();
    }

}