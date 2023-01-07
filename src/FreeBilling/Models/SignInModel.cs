using System.ComponentModel.DataAnnotations;

namespace FreeBilling.Models;

public class SignInModel
{
  [Required]
  public string Username { get; set; } = "";
  [Required]
  public string Password { get; set; } = "";
  public bool RememberMe { get; set; }
}