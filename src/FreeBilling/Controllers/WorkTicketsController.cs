using AutoMapper;
using FreeBilling.Data;
using FreeBilling.Data.Entities;
using FreeBilling.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreeBilling.Controllers;

[ApiController]
[Route("api/tickets")]
[Authorize]
public class WorkTicketsController : ControllerBase
{
  private IBillingRepository _repository;
  private IMapper _mapper;
  private ILogger<WorkTicketsController> _logger;
  private readonly UserManager<Employee> _userManager;

  public WorkTicketsController(IBillingRepository repository,
  IMapper mapper,
  ILogger<WorkTicketsController> logger,
  UserManager<Employee> userManager)
  {
    _repository = repository;
    _mapper = mapper;
    _logger = logger;
    _userManager = userManager;
  }

  [HttpPost]
  public async Task<IResult> Post(WorkTicketModel model)
  {
    try
    {
      if (User?.Identity?.IsAuthenticated == true)
      {
        var entity = _mapper.Map<WorkTicket>(model);
        var currentUser = await _userManager.GetUserAsync(User);
        if (currentUser is not null)
        {
          entity.Employee = currentUser;

          var project = await _repository.GetProject(model.ProjectId);
          entity.Project = project;

          _repository.Add(entity);
          if (await _repository.SaveChanges())
          {
            return Results.Created($"/api/tickets/{model.Id}", _mapper.Map<WorkTicketModel>(entity));
          }
        }
      }
    } 
    catch (Exception ex)
    {
      _logger.LogError($"Failed to save WorkTicket: {ex}");
    }
    return Results.BadRequest("Failed to create WorkTicket");
  }
}
