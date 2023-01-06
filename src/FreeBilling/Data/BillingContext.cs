using FreeBilling.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreeBilling.Data;

public class BillingContext : DbContext
{
  public BillingContext(DbContextOptions opt) : base(opt) { }

  public DbSet<Customer> Customers => Set<Customer>();
  public DbSet<Employee> Employees => Set<Employee>();
  public DbSet<Address> Addresses => Set<Address>();
  public DbSet<Project> Projects => Set<Project>();
  public DbSet<WorkTicket> WorkTickets => Set<WorkTicket>();

  protected override void OnModelCreating(ModelBuilder bldr)
  {
    base.OnModelCreating(bldr);

    bldr.Entity<Employee>()
      .HasData(new
      {
        Id = 1,
        Name = "Shawn",
        BillingRate = 325.00
      });

    bldr.Entity<Customer>()
      .HasData(new
      {
        Id = 1,
        CompanyName = "Twain Films, LLC",
        Contact = "Jim Smith",
        PhoneNumber = "404-555-1212",
        AddressId = 1,
      },
      new
      {
        Id = 2,
        CompanyName = "Johnson and Johnson",
        Contact = "Mel Phillips",
        PhoneNumber = "561-555-1212",
        AddressId = 2,
      });

    bldr.Entity<Address>()
      .HasData(new
      {
        Id = 1,
        AddressLine1 = "123 Main Street",
        City = "Atlanta",
        StateProvince = "GA",
        PostalCode = "30303"
      },
      new
      {
        Id = 2,
        AddressLine1 = "500 US 411",
        AddressLine2 = "Suite 100",
        City = "Fort Lauderdale",
        StateProvince = "FL",
        PostalCode = "30033"
      });

    bldr.Entity<Project>()
      .HasData(new
      {
        Id = 1,
        ProjectName = "SEO Help",
        CustomerId = 1,
        StartDate = new DateTime(2023, 1, 5)
      },
      new
      {
        Id = 2,
        ProjectName = "Releasing New Product to Production",
        CustomerId = 1,
        StartDate = new DateTime(2022, 4, 24),
        EndDate = new DateTime(2022, 12, 15)
      },
      new
      {
        Id = 3,
        ProjectName = "Perform Code Review",
        CustomerId = 2,
        StartDate = new DateTime(2023, 1, 5)
      });

  }
}