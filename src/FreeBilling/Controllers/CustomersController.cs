using AutoMapper;
using FreeBilling.Data;
using FreeBilling.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FreeBilling.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController 
{
  private readonly IBillingRepository _repository;
  private readonly IMapper _mapper;
  private readonly ILogger<CustomersController> _logger;

  public CustomersController(IBillingRepository repository,
    IMapper mapper,
    ILogger<CustomersController> logger)
  {
    _repository = repository;
    _mapper = mapper;
    _logger = logger;
  }

  [HttpGet]
  public async Task<IResult> GetCustomers()
  {
    return Results.Ok(await _repository.GetAllCustomers());
  }

  [HttpGet("{id:int}")]
  public async Task<IResult> GetCustomer(int id)
  {
    var customer = await _repository.GetCustomer(id);
    if (customer is null) return Results.NotFound("Customer Not Found");
    return Results.Ok(customer);
  }


  [HttpPost]
  public async Task<IResult> Post(Customer model)
  {
    try
    {
      _repository.Add(model);
      if (await _repository.SaveChanges())
      {
        return Results.Created($"/api/customers/{model.Id}", model);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError($"Error Occurred while creating Customer: {ex.Message}");
    }
    return Results.BadRequest("Failed to add Customer");
  }

  [HttpPut("{id:int}")]
  public async Task<IResult> Put(int id, Customer model)
  {
    try
    {
      var oldCustomer = await _repository.GetCustomer(id);
      if (oldCustomer is null) return Results.NotFound("Customer Not Found");

      // Copy new values to connected entity
      _mapper.Map(model, oldCustomer);

      if (await _repository.SaveChanges())
      {
        return Results.Ok(oldCustomer);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError($"Error Occurred while updating Customer: {ex.Message}");
    }
    return Results.BadRequest("Failed to update Customer");

  }

  [HttpDelete("{id:int}")]
  public async Task<IResult> Delete(int id)
  {
    try
    {
      var oldCustomer = await _repository.GetCustomer(id);
      if (oldCustomer is null) return Results.NotFound("Customer Not Found");

      _repository.Remove(oldCustomer);
      if (await _repository.SaveChanges())
      {
        return Results.Ok();
      }
    }
    catch (Exception ex)
    {
      _logger.LogError($"Error Occurred while deleting Customer: {ex.Message}");
    }

    return Results.BadRequest("Failed to delete Customer");
  }
}
