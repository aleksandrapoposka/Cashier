using DataAccess;
using DataAccess.Data;
using DataAccess.Interfaces;
using DataAccess.Models;
using Entities;
using Entities.Articles;
using Entities.Orders;
using Entities.User;
using InfrastructureMongoDB;
using InfrastructureSql;
using InfrastructureSql.Concrete;
using InfrastructureSql.Interfaces;
using InfrastructureStorageAccount;
using InfrastructureStorageAccount.Concrete;
using InfrastructureStorageAccount.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NLog;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "Cashier_";
});

// Configure Storage Account
builder.Services.Configure<StorageAccountConnection>(builder.Configuration.GetSection(nameof(StorageAccountConnection)));
builder.Services.AddSingleton<IStorageAccountConnection>(config => config.GetRequiredService<IOptions<StorageAccountConnection>>().Value);

// Register Repositories
builder.Services.RegisterSqlRepositories();
builder.Services.RegisterAzureRepositories();

// Application Insights
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
