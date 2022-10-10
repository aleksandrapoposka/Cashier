using DataAccess;
using DataAccess.Data;
using Entities;
using Entities.Articles;
using Entities.User;
using InfrastructureMongoDB;
using InfrastructureSql.Concrete;
using InfrastructureSql.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRepository<Article>, ArticleRepository>();
builder.Services.AddScoped<IRepository<Country>, CountryRepository>();

builder.Services.Configure<MongoDBConnection>(
                builder.Configuration.GetSection(nameof(MongoDBConnection)));

//builder.Services.AddSingleton<IMongoDBConnection>(sp =>
//    sp.GetRequiredService<IOptions<MongoDBConnection>>().Value);
//builder.Services.AddSingleton<IMongoDBConnection, MongoDBConnection>();

//builder.Services.AddSingleton<IMongoClient>(s =>
//        new MongoClient(builder.Configuration.GetValue<string>("MongoDBDatabaseSettings:ConnectionString")));


//builder.Services.Configure<MongoDBConnection>(
//                builder.Configuration.GetSection(nameof(MongoDBConnection)));

//builder.Services.AddSingleton<IMongoDBConnection>(sp =>
//    sp.GetRequiredService<IOptions<MongoDBConnection>>().Value);

//builder.Services.AddSingleton<IMongoClient>(s =>
//        new MongoClient(builder.Configuration.GetValue<string>("MongoDBDatabaseSettings:ConnectionString")));


builder.Services.Configure<MongoDBConnection>(
    builder.Configuration.GetSection("MongoDBConnection")
);

builder.Services.AddSingleton<IMongoDatabase>(options => {
    var settings = builder.Configuration.GetSection("MongoDBConnection").Get<MongoDBConnection>();
    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddTransient<IArticleImageRepository, ArticleImageRepository>();



// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Host.UseNLog();

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
