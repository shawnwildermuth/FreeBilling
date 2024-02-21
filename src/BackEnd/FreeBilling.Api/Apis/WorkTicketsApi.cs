using FreeBilling.Data;
using FreeBilling.Data.Entities;
using FreeBilling.Models;
using Mapster;
using WilderMinds.MinimalApiDiscovery;
using WilderMinds.MinimalApis.FluentValidation;

namespace FreeBilling.Controllers;

public class WorkTicketsApi : IApi
{

  public void Register(IEndpointRouteBuilder app)
  {
    app.MapPost("/api/tickets", Post)
      .Validate<WorkTicketModel>();
  }

  public async Task<IResult> Post(HttpContext ctx, 
    IBillingRepository repository,
    ILogger<WorkTicketsApi> logger,
    WorkTicketModel model)
  {
    try
    {
      var entity = model.Adapt<WorkTicket>();
      //entity.Employee = currentUser;

      var project = await repository.GetProject(model.ProjectId);
      entity.Project = project;

      repository.Add(entity);
      if (await repository.SaveChanges())
      {
        return Results.Created($"/api/tickets/{model.Id}", entity.Adapt<WorkTicketModel>());
      }
    } 
    catch (Exception ex)
    {
      logger.LogError($"Failed to save WorkTicket: {ex}");
    }
    return Results.BadRequest("Failed to create WorkTicket");
  }

}
