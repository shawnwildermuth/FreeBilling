using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeBilling.Apis;
using FreeBilling.Data;
using FreeBilling.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace FreeBilling.Tests.Apis;

public class CustomerApiTests : BaseTest
{
  private IBillingRepository _repo;
  private ILogger<CustomersApi> _logger;

  public CustomerApiTests()
  {
    var svcs = GenerateServices();
    _repo = svcs.GetService<IBillingRepository>()!;
    _logger = svcs.GetService<ILogger<CustomersApi>>()!;
  }

  [Fact]
  public async Task CanReadCustomers()
  {
    var result = await CustomersApi.GetCustomers(_repo);
    Assert.IsAssignableFrom<Ok<IEnumerable<Customer>>>(result);
    var customers = ((Ok<IEnumerable<Customer>>)result).Value;
    Assert.NotNull(customers);
    Assert.True(customers.Count() > 0);
  }

  [Fact]
  public async Task CanReadCustomer()
  {
    var result = await CustomersApi.GetCustomer(_repo, 1);
    Assert.IsAssignableFrom<Ok<Customer>>(result);
    var customer = ((Ok<Customer>)result).Value;
    Assert.NotNull(customer);
    Assert.True(customer.PhoneNumber == "404-555-1212");
  }

  [Fact]
  public async Task CantFindCustomer()
  {
    var result = await CustomersApi.GetCustomer(_repo, 30);
    Assert.IsAssignableFrom<NotFound<string>>(result);
  }

  [Fact]
  public async Task CanSaveCustomer()
  {
    var newItem = new Customer()
    {
      Id = 0,
      CompanyName = "Test Me"
    };

    var result = await CustomersApi.Post(_repo, _logger, newItem);
    Assert.IsAssignableFrom<Created<Customer>>(result);
    var customer = ((Created<Customer>)result).Value;
    Assert.NotNull(customer);
    var found = await CustomersApi.GetCustomer(_repo, customer.Id);
    Assert.NotNull(found);
    Assert.True(customer.CompanyName == newItem.CompanyName);
  }

  [Fact]
  public async Task CanUpdateCustomer()
  {
    var result = await CustomersApi.GetCustomer(_repo, 3);
    Assert.IsAssignableFrom<Ok<Customer>>(result);
    var customer = ((Ok<Customer>)result).Value;
    Assert.NotNull(customer);
    var originalName = customer.CompanyName;
    customer.CompanyName += "-";
    var updated = await CustomersApi.Put(_repo, _logger, customer.Id, customer);
    Assert.IsAssignableFrom<Ok<Customer>>(result);
    var found = await CustomersApi.GetCustomer(_repo, customer.Id);
    Assert.NotNull(found);
    Assert.True(customer.CompanyName == originalName + "-");
  }


  [Fact]
  public async Task CanDeleteCustomer()
  {
    var newItem = new Customer()
    {
      Id = 0,
      CompanyName = "Test Me"
    };

    var created = await CustomersApi.Post(_repo, _logger, newItem);
    Assert.IsAssignableFrom<Created<Customer>>(created);
    var customer = ((Created<Customer>)created).Value;
    Assert.NotNull(customer);
    var result = await CustomersApi.Delete(_repo, _logger, customer.Id);
    Assert.IsAssignableFrom<Ok>(result);
    var found = await CustomersApi.GetCustomer(_repo, customer.Id);
    Assert.IsAssignableFrom<NotFound<string>>(found);
  }
}
