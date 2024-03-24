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
using Newtonsoft.Json.Linq;

namespace FreeBilling.Tests.Apis;

public class CustomerApiTests : BaseTest
{
  private IBillingRepository _repo;

  public CustomerApiTests()
  {
    var svcs = GenerateServices();
    _repo = svcs.GetService<IBillingRepository>()!;
  }

  [Fact]
  public async Task CanReadCustomers()
  {
    var result = await CustomersApi.GetCustomers(_repo);
    Assert.IsAssignableFrom<Ok<IEnumerable<Customer>>>(result);
    var customers = ((Ok<IEnumerable<Customer>>)result).Value;
    Assert.NotNull(customers);
    Assert.True(customers.Count() == 1);
  }

  [Fact]
  public async Task CanReadCustomer()
  {
    var result = await CustomersApi.GetCustomer(_repo, 1);
    Assert.IsAssignableFrom<Ok<Customer>>(result);
    var customer = ((Ok<Customer>)result).Value;
    Assert.NotNull(customer);
    Assert.True(customer.CompanyName == "Acme");
  }

  [Fact]
  public async Task CantFindCustomer()
  {
    var result = await CustomersApi.GetCustomer(_repo, 2);
    Assert.IsAssignableFrom<NotFound<string>>(result);
  }
}
