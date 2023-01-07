using AutoMapper;
using FreeBilling.Data;
using FreeBilling.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeBilling.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class EmployeesController
{
  private readonly IBillingRepository _repository;
  private readonly IMapper _mapper;
  private readonly ILogger<EmployeesController> _logger;

  public EmployeesController(IBillingRepository repository,
    IMapper mapper,
    ILogger<EmployeesController> logger)
  {
    _repository = repository;
    _mapper = mapper;
    _logger = logger;
  }

  [HttpGet]
  public async Task<IResult> Get()
  {
    return Results.Ok(await _repository.GetAllEmployees());
  }

}
