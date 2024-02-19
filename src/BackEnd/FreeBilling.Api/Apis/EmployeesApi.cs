﻿using AutoMapper;
using FreeBilling.Data;
using FreeBilling.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WilderMinds.MinimalApiDiscovery;

namespace FreeBilling.Controllers;

public class EmployeesApi : IApi
{

  public void Register(WebApplication app)
  {
    app.MapGet("/api/employees", Get)
      .Produces(200)
      .RequireAuthorization();
  }

  public async Task<IResult> Get(IBillingRepository repo)
  {
    return Results.Ok(await repo.GetAllEmployees());
  }

}