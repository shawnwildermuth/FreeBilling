using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FreeBilling.Data.Entities;

public class Employee : IdentityUser<int>
{
  public string Name { get; set; } = "";
  public double BillingRate { get; set; }
}