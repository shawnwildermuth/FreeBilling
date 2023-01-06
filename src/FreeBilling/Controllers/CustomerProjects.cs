using AutoMapper;
using FreeBilling.Data;
using FreeBilling.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FreeBilling.Controllers;

[Route("api/customers/{custId:int}/projects/")]
[ApiController]
public class CustomerProjectsController
{
  private readonly IBillingRepository _repository;
  private readonly IMapper _mapper;
  private readonly ILogger<CustomerProjectsController> _logger;

  public CustomerProjectsController(IBillingRepository repository,
    IMapper mapper,
    ILogger<CustomerProjectsController> logger)
  {
    _repository = repository;
    _mapper = mapper;
    _logger = logger;
  }

  [HttpGet]
  public async Task<IResult> GetProjects(int custId)
  {
    return Results.Ok(await _repository.GetCustomerProjects(custId));
  }
}