using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FreeBilling.Data.Entities;

public class Employee 
{
  public int Id { get; set; }
  public string Name { get; set; } = "";
  public string UserId { get; set; } = "";
  public string Email { get; set; } = "";
  public double BillingRate { get; set; }
}