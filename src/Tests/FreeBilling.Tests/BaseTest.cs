using System.Reflection;
using FreeBilling.Data;
using FreeBilling.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using NSubstitute;


namespace FreeBilling.Tests;

public abstract class BaseTest
{
  public virtual IBillingRepository GenerateRepository()
  {
    var repo = Substitute.For<IBillingRepository>();

    repo.GetAllCustomers().Returns(GetCustomers());
    repo.GetCustomer(Arg.Any<int>()).Returns(info => GetCustomers().Where(i => i.Id == info.ArgAt<int>(0)).FirstOrDefault());

    repo.GetCustomerProjects(Arg.Any<int>()).Returns(info => GetProjects().Where(i => i.CustomerId == info.ArgAt<int>(0)).ToList());
    repo.GetProject(Arg.Any<int>()).Returns(info => GetProjects().Where(i => i.Id == info.ArgAt<int>(0)).FirstOrDefault());

    return repo;
  }

  public IServiceProvider GenerateServices()
  {
    var bldr = new ConfigurationBuilder();
    bldr.AddEnvironmentVariables();

    var config = bldr.Build();

    var svcs = new ServiceCollection();

    svcs.AddSingleton<IConfiguration>(config);
    svcs.AddSingleton(GenerateRepository());
    svcs.AddLogging();
    AddServices(svcs);

    return svcs.BuildServiceProvider();
  }

  internal virtual void AddServices(IServiceCollection svcs) { }

  IQueryable<Customer> GetCustomers()
  {
    return new List<Customer>
    {
      new Customer() {
        Id = 1,
        CompanyName = "Acme",
        PhoneNumber = "404-555-1212", 
        Address = new Address() 
        {
          AddressLine1 = "123 Main Street",
          City = "Atlanta",
          StateProvince = "GA",
          PostalCode = "30303"
        }
      }
    }.AsQueryable();
  }

  IQueryable<Project> GetProjects()
  {
    return new List<Project>
    {
      new Project() {
        Id = 1,
        CustomerId = 1,
        ProjectName = "Average Stuff",
        StartDate = DateTime.Today,
        Customer = GetCustomers().First()
      }
    }.AsQueryable();
  }

}

