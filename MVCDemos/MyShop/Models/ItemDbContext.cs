using Microsoft.EntityFrameworkCore;

namespace MyShop.Models;

// This class is used to connect to the database and to create the table

public class ItemDbContext : DbContext
{
  public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
  {
    // Database.EnsureCreated(); // Make sure the database is created when the application starts
  }

  public DbSet<Item> Items { get; set; }
  public DbSet<Customer> Customers { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<OrderItem> OrderItems { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    // Allows for Lazy Loading 
    optionsBuilder.UseLazyLoadingProxies();
  }

}