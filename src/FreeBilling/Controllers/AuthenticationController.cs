using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FreeBilling.Data;
using FreeBilling.Data.Entities;
using FreeBilling.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FreeBilling.Controllers;

[Route("api/auth")]
[ApiController]
[Authorize]
public class AuthenticationController
{
  private readonly IBillingRepository _repository;
  private readonly IMapper _mapper;
  private readonly ILogger<AuthenticationController> _logger;
  private readonly UserManager<Employee> _userManager;
  private readonly IConfiguration _config;

  public AuthenticationController(IBillingRepository repository,
    IMapper mapper,
    ILogger<AuthenticationController> logger,
    UserManager<Employee> userManager,
    IConfiguration config)
  {
    _repository = repository;
    _mapper = mapper;
    _logger = logger;
    _userManager = userManager;
    _config = config;
  }

  [AllowAnonymous]
  [HttpPost("signin")]
  public async Task<IResult> Signin(SignInModel model)
  {
    try
    {
      var user = await _repository.GetEmployee(model.Username);
      if (user is not null)
      {
        var result = await _userManager.CheckPasswordAsync(user, model.Password);
        if (result)
        {

          var bearer = _config.GetSection("Authentication:Schemes:Bearer");
          if (bearer is not null)
          {
            var uniqueKey = bearer.GetSection("SigningKeys").Get<SymmetricSecurityKey[]>()?.First().Key;
            var issuer = bearer["ValidIssuer"];
            var audiences = bearer.GetSection("ValidAudiences").Get<string[]>();
            var key = new SymmetricSecurityKey(uniqueKey);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var expiration = DateTime.UtcNow.AddDays(2);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Iss, issuer!),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ((DateTimeOffset)expiration).ToUnixTimeSeconds().ToString())
            };

            // Add all audiences
            foreach (var audience in audiences!)
            {
              claims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
            }

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audiences[0],
                expires: expiration,
                claims: claims,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Results.Created("/auth/signin", new SignInResultModel()
            {
              Token = tokenString,
              Expiration = token.ValidTo
            });
          }

        }
      }
    }
    catch (Exception ex)
    {
      _logger.LogWarning($"Failed to sign in: {ex}");
    }

    return Results.BadRequest("Bad username or password");
  }


}
