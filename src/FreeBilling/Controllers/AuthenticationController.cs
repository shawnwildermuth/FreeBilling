using AutoMapper;
using FreeBilling.Data;
using FreeBilling.Data.Entities;
using FreeBilling.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreeBilling.Controllers;

[Route("api/auth")]
[ApiController]
[Authorize]
public class AuthenticationController
{
  private readonly IBillingRepository _repository;
  private readonly IMapper _mapper;
  private readonly ILogger<AuthenticationController> _logger;
  private readonly UserManager<BillingUser> _userManager;

  public AuthenticationController(IBillingRepository repository,
    IMapper mapper,
    ILogger<AuthenticationController> logger,
    UserManager<BillingUser> userManager)
  {
    _repository = repository;
    _mapper = mapper;
    _logger = logger;
    _userManager = userManager;
  }

  [AllowAnonymous]
  [HttpPost("signin")]
  public async Task<IResult> Signin(SignInModel model)
  {
    try
    {
      var user = await _repository.GetUser(model.Username);
      if (user is not null)
      {
        var result = await _userManager.CheckPasswordAsync(user, model.Password);
        if (result)
        {
          return Results.Created("/auth/signin", new SignInResultModel()
          {
            Token = "12345",
            Expiration = DateTime.Now.AddMinutes(20)
          });
        }
      }
    }
    catch (Exception ex)
    {
      _logger.LogWarning($"Failed to sign in: {ex.Message}");
    }

    return Results.BadRequest("Bad username or password");
  }

}
