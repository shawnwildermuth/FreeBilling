using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FreeBilling.Data.Entities;

public class Employee : IdentityUser<int>
{
  [Required]
  [MaxLength(255)]
  public string? Name { get; set; }
  [Required]
  public double BillingRate { get; set; }
}