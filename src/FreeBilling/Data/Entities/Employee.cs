using System.ComponentModel.DataAnnotations;

namespace FreeBilling.Data.Entities;

public class Employee
{
  public int Id { get; set; }
  [Required]
  [MaxLength(255)]
  public string? Name { get; set; }
  public string? UserId { get; set; }
  [Required]
  public double BillingRate { get; set; }
}