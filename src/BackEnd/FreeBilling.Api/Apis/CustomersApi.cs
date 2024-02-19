using AutoMapper;
using FreeBilling.Data;
using FreeBilling.Data.Entities;
using FreeBilling.Filters;
using Microsoft.AspNetCore.Mvc;
using WilderMinds.MinimalApiDiscovery;

namespace FreeBilling.Controllers;

public class CustomersApi : IApi
{

  public void Register(WebApplication app)
  {
    var group = app.MapGroup("/api/customers")
      .RequireAuthorization();

    group.MapGet("", GetCustomers)
      .Produces(200);

    group.MapGet("{id:int}", GetCustomer)
      .Produces(200);

    group.MapPost("", Post)
      .AddModelValidation<Customer>()
      .Produces(201);

    group.MapPut("{id:int}", Put)
      .AddModelValidation<Customer>()
      .Produces(200);

    group.MapDelete("{id:int}", Delete)
      .Produces(200);
  }

  public async Task<IResult> GetCustomers(IBillingRepository repo)
  {
    return Results.Ok(await repo.GetAllCustomers());
  }

  public async Task<IResult> GetCustomer(IBillingRepository repo, int id)
  {
    var customer = await repo.GetCustomer(id);
    if (customer is null) return Results.NotFound("Customer Not Found");
    return Results.Ok(customer);
  }


  public async Task<IResult> Post(IBillingRepository repo, 
    ILogger<CustomersApi> logger,
    Customer model)
  {
    try
    {
      repo.Add(model);
      if (await repo.SaveChanges())
      {
        return Results.Created($"/api/customers/{model.Id}", model);
      }
    }
    catch (Exception ex)
    {
      logger.LogError($"Error Occurred while creating Customer: {ex.Message}");
    }
    return Results.BadRequest("Failed to add Customer");
  }

  public async Task<IResult> Put(IBillingRepository repo,
    IMapper mapper,
    ILogger<CustomersApi> logger,
    int id, 
    Customer model)
  {
    try
    {
      var oldCustomer = await repo.GetCustomer(id);
      if (oldCustomer is null) return Results.NotFound("Customer Not Found");

      // Copy new values to connected entity
      mapper.Map(model, oldCustomer);

      if (await repo.SaveChanges())
      {
        return Results.Ok(oldCustomer);
      }
    }
    catch (Exception ex)
    {
      logger.LogError($"Error Occurred while updating Customer: {ex.Message}");
    }
    return Results.BadRequest("Failed to update Customer");

  }

  public async Task<IResult> Delete(IBillingRepository repo,
    ILogger<CustomersApi> logger, 
    int id)
  {
    try
    {
      var oldCustomer = await repo.GetCustomer(id);
      if (oldCustomer is null) return Results.NotFound("Customer Not Found");

      repo.Remove(oldCustomer);
      if (await repo.SaveChanges())
      {
        return Results.Ok();
      }
    }
    catch (Exception ex)
    {
      logger.LogError($"Error Occurred while deleting Customer: {ex.Message}");
    }

    return Results.BadRequest("Failed to delete Customer");
  }
}