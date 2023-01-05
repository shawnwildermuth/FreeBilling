using System.ComponentModel.DataAnnotations;

namespace FreeBilling.Models;

public class ContactModel
{
  
  [Required]
  public string? Name { get; set; }
  [Required]
  [EmailAddress]
  public string? Email { get; set; }
  [Required]
  public string? Subject { get; set; }
  [Required]
  public string? Message { get; set; }
}