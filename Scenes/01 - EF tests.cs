using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trests.Scenes
{
  class Scene1 : IScene
  {
    public async Task Run()
    {
      var options = new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase("test")
        .Options;
      using var context = new DataContext(options);
      context.Database.EnsureCreated();

      var x = await context.Customers.ToListAsync();
      x.Dump();
    }
  }

  public static class Shared
  {
    public static bool isNYCitizen(this Customer customer) => customer.City == "NY";
  }

  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Customer>().HasData(new[] {
        SampleData.Customer1,
        SampleData.Customer2,
        SampleData.Customer3
      });
      modelBuilder.Entity<Order>().HasData(new[] {
        SampleData.Order1,
        SampleData.Order2,
        SampleData.Order3,
        SampleData.Order4,
        SampleData.Order5,
        SampleData.Order6
      });
      base.OnModelCreating(modelBuilder);
    }
  }

  public class Customer
  {
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string City { get; set; }

    public List<Order> Orders { get; set; }
  }

  public class Order
  {
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public int CustomerId { get; set; }
  }

  public static class SampleData
  {
    public static Customer Customer1 = new Customer
    {
      CustomerId = 1,
      City = "NY",
      CustomerName = "John Doe"
    };
    public static Customer Customer2 = new Customer
    {
      CustomerId = 2,
      City = "CA",
      CustomerName = "Bob Fetcher"
    };
    public static Customer Customer3 = new Customer
    {
      CustomerId = 3,
      City = "LA",
      CustomerName = "Erny Addams"
    };
    public static Order Order1 = new Order
    {
      OrderId = 1,
      Amount = 100,
      CustomerId = 1
    };
    public static Order Order2 = new Order
    {
      OrderId = 2,
      Amount = 120,
      CustomerId = 1
    };
    public static Order Order3 = new Order
    {
      OrderId = 3,
      Amount = 1000,
      CustomerId = 2
    };
    public static Order Order4 = new Order
    {
      OrderId = 4,
      Amount = 170,
      CustomerId = 2
    };
    public static Order Order5 = new Order
    {
      OrderId = 5,
      Amount = 500,
      CustomerId = 3
    };
    public static Order Order6 = new Order
    {
      OrderId = 6,
      Amount = 300,
      CustomerId = 1
    };
  }
}
