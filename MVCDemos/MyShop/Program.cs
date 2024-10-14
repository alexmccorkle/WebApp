using Microsoft.EntityFrameworkCore;
using MyShop.DAL;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ItemDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ItemDbContextConnection' not found.");

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ItemDbContext>(options =>
{ // This line configures the ItemDbContext to use SQLite
  options.UseSqlite(
    builder.Configuration["ConnectionStrings:ItemDbContextConnection"]);
});

// builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ItemDbContext>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
  //Password Settings:
  options.Password.RequireDigit = true;
  options.Password.RequiredLength = 8;
  options.Password.RequireNonAlphanumeric = false;
  options.Password.RequireUppercase = true;
  options.Password.RequireLowercase = true;
  options.Password.RequiredUniqueChars = 6;

  // Lockout Settings:
  options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
  options.Lockout.MaxFailedAccessAttempts = 5;
  options.Lockout.AllowedForNewUsers = true;

  // User Settings:
  options.User.RequireUniqueEmail = true;

  // Sign-In Settings:
  options.SignIn.RequireConfirmedAccount = false; // Set to true if you want email confirmation!
})
.AddEntityFrameworkStores<ItemDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IItemRepository, ItemRepository>();

builder.Services.AddRazorPages();
builder.Services.AddSession(options =>
{
  options.Cookie.Name = ".WebShopSesh.Session";
  options.IdleTimeout = TimeSpan.FromSeconds(1800); // 30 minutes
  options.Cookie.IsEssential = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
  options.LoginPath = "/Identity/Account/Login";
});

var loggerConfiguration = new LoggerConfiguration()
    .MinimumLevel.Information() // levels: Trace < Information < Warning < Error < Fatal
    .WriteTo.File($"Logs/app_{DateTime.Now:yyyyMMdd_HHmmss}.log");

loggerConfiguration.Filter.ByExcluding(e => e.Properties.TryGetValue("SourceContext", out var value) &&
                              e.Level == LogEventLevel.Information &&
                              e.MessageTemplate.Text.Contains("Executed DbCommand"));

var logger = loggerConfiguration.CreateLogger();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  DBInit.Seed(app);
}

app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapDefaultControllerRoute();
app.Run();

