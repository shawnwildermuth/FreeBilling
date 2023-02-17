using AutoMapper;
using FreeBilling.Data;
using FreeBilling.Data.Entities;
using FreeBilling.Filters;
using FreeBilling.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WilderMinds.MinimalApiDiscovery;

namespace FreeBilling.Controllers;

public class WorkTicketsApi : IApi
{

  public void Register(WebApplication app)
  {
    app.MapPost("/api/tickets", Post)
      .RequireAuthorization()
      .AddModelValidation<WorkTicketModel>();
  }

  public async Task<IResult> Post(HttpContext ctx, 
    IMapper mapper,
    UserManager<Employee> userManager,
    IBillingRepository repository,
    ILogger<WorkTicketsApi> logger,
    WorkTicketModel model)
  {
    try
    {
      if (ctx.User?.Identity?.IsAuthenticated == true)
      {
        var entity = mapper.Map<WorkTicket>(model);
        var currentUser = await userManager.GetUserAsync(ctx.User);
        if (currentUser is not null)
        {
          entity.Employee = currentUser;

          var project = await repository.GetProject(model.ProjectId);
          entity.Project = project;

          repository.Add(entity);
          if (await repository.SaveChanges())
          {
            return Results.Created($"/api/tickets/{model.Id}", mapper.Map<WorkTicketModel>(entity));
          }
        }
      }
    } 
    catch (Exception ex)
    {
      logger.LogError($"Failed to save WorkTicket: {ex}");
    }
    return Results.BadRequest("Failed to create WorkTicket");
  }

}
