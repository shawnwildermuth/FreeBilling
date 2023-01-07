using Microsoft.AspNetCore.Identity;

namespace FreeBilling.Data.Entities;

public class BillingUser : IdentityUser<int>
{
  public bool IsEmployee { get; set; }
}
