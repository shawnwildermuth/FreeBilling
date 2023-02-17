using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FreeBilling.Data;
using FreeBilling.Data.Entities;
using FreeBilling.Filters;
using FreeBilling.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WilderMinds.MinimalApiDiscovery;

namespace FreeBilling.Controllers;

public class AuthenticationApi : IApi
{
  public void Register(WebApplication app)
  {
    app.MapPost("/api/auth/signin", Signin)
      .AddModelValidation<SignInModel>();
  }

  public async Task<IResult> Signin(IBillingRepository repository,
    ILogger<AuthenticationApi> logger,
    UserManager<Employee> userManager,
    IConfiguration config,
    SignInModel model)
  {
    try
    {
      var user = await repository.GetEmployee(model.Username);
      if (user is not null)
      {
        var result = await userManager.CheckPasswordAsync(user, model.Password);
        if (result)
        {

          var bearer = config.GetSection("Authentication:Schemes:Bearer");
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
              Expiration = token.ValidTo,
              Username = user.UserName!
            });
          }

        }
      }
    }
    catch (Exception ex)
    {
      logger.LogWarning($"Failed to sign in: {ex}");
    }

    return Results.BadRequest("Bad username or password");
  }


}
