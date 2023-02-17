using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FreeBilling.Filters;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
  public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, 
    EndpointFilterDelegate next)
  {
    var svcs = context.HttpContext.RequestServices;

    // Model is more likely at the end
    foreach (var argument in context.Arguments) 
    {
      if (argument?.GetType() == typeof(T))
      {
        var validator = svcs.GetService<IValidator<T>>();
        if (validator is not null)
        {
          var result = validator.Validate((T)argument);
          if (!result.IsValid)
          {
            return Results.Problem(result.ToString());            
          }
        }
      }
    }
    return await next(context);
  }
}