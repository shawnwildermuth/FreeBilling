using AutoMapper;
using FreeBilling.Data;
using FreeBilling.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using WilderMinds.MinimalApiDiscovery;

namespace FreeBilling.Controllers;

public class CustomerProjectsApi : IApi
{

  public void Register(WebApplication app)
  {
    app.MapGet("api/customers/{custId:int}/projects/", GetProjects)
      .Produces(200)
      .RequireAuthorization();
  }

  public async Task<IResult> GetProjects(IBillingRepository repo, int custId)
  {
    return Results.Ok(await repo.GetCustomerProjects(custId));
  }
}