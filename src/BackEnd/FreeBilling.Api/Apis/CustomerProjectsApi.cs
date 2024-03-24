using FreeBilling.Data;
using FreeBilling.Data.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WilderMinds.MinimalApiDiscovery;
using WilderMinds.MinimalApis.FluentValidation;

namespace FreeBilling.Apis;

public class CustomerProjectsApi : IApi
{

  public void Register(IEndpointRouteBuilder app)
  {
    var group = app.MapGroup("api/customers/{custId:int}/projects/");

    group.MapGet("", GetProjects)
      .Produces(200);
    group.MapGet("{id:int}", GetProject)
      .Produces(200);
    group.MapPost("", Post)
      .Validate<Project>()
      .Produces(200);
    group.MapPut("{id:int}", Put)
      .Validate<Project>()
      .Produces(200);
    group.MapDelete("{id:int}", Delete)
      .Produces(200);
  }

  public static async Task<IResult> GetProjects(IBillingRepository repo, int custId)
  {
    return Results.Ok(await repo.GetCustomerProjects(custId));
  }

  public static async Task<IResult> GetProject(IBillingRepository repo, int custId, int projectId)
  {
    var result = await repo.GetProject(projectId);
    if (result is null) return Results.NotFound();
    if (result?.CustomerId != custId) return Results.BadRequest("Project doesn't belong to this customer.");
    return Results.Ok(result);
  }

  public static async Task<IResult> Post(IBillingRepository repo,
                                  ILogger<CustomerProjectsApi> logger,
                                  Project model)
  {
    try
    {
      repo.Add(model);
      if (await repo.SaveChanges())
      {
        return Results.Created($"/api/customers/{model.CustomerId}/projects/{model.Id}", model);
      }
    }
    catch (Exception ex)
    {
      logger.LogError($"Error Occurred while creating Customer: {ex.Message}");
    }
    return Results.BadRequest("Failed to add Customer");
  }

  public static async Task<IResult> Put(IBillingRepository repo,
    ILogger<CustomerProjectsApi> logger,
    int id,
    Project model)
  {
    try
    {
      var oldProject = await repo.GetProject(id);
      if (oldProject is null) return Results.NotFound("Project Not Found");

      // Copy new values to connected entity
      model.Adapt(oldProject);

      await repo.SaveChanges(); // Allow for no changes to be successful
      return Results.Ok(oldProject);
    }
    catch (Exception ex)
    {
      logger.LogError($"Error Occurred while updating Project: {ex.Message}");
    }
    return Results.BadRequest("Failed to update Project");

  }

  public static async Task<IResult> Delete(IBillingRepository repo,
                                    ILogger<CustomerProjectsApi> logger,
                                    int id)
  {
    try
    {
      var oldProject = await repo.GetProject(id);
      if (oldProject is null) return Results.NotFound("Project Not Found");

      repo.Remove(oldProject);
      if (await repo.SaveChanges())
      {
        return Results.Ok();
      }
    }
    catch (Exception ex)
    {
      logger.LogError($"Error Occurred while deleting Project: {ex.Message}");
    }

    return Results.BadRequest("Failed to delete Project");
  }

}