namespace FreeBilling.Filters;

public static class FilterExtensions
{
  public static RouteHandlerBuilder AddModelValidation<T>(this RouteHandlerBuilder bldr) where T : class
  {
    return bldr.AddEndpointFilter(new ValidationFilter<T>());
  }
}