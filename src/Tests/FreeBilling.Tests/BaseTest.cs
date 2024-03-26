using System.Reflection;
using FreeBilling.Data;
using FreeBilling.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using NSubstitute;


namespace FreeBilling.Tests;

public abstract class BaseTest
{
  public IServiceProvider GenerateServices()
  {
    var bldr = new ConfigurationBuilder();
    bldr.AddEnvironmentVariables();

    var config = bldr.Build();

    var svcs = new ServiceCollection();

    svcs.AddSingleton<IConfiguration>(config);
    svcs.AddTransient(_ => GenerateContext());
    svcs.AddTransient<IBillingRepository, BillingRepository>();
    svcs.AddLogging();
    AddServices(svcs);

    return svcs.BuildServiceProvider();
  }

  public virtual BillingContext GenerateContext()
  {
    var builder = new DbContextOptionsBuilder<BillingContext>();
    builder.UseInMemoryDatabase(databaseName: "BillingContext", new InMemoryDatabaseRoot());

    var theContext = new BillingContext(builder.Options);

    // Delete existing db before creating a new one
    var deleted = theContext.Database.EnsureDeleted();
    var created = theContext.Database.EnsureCreated();

    return theContext;
  }

  internal virtual void AddServices(IServiceCollection svcs) { }


}

