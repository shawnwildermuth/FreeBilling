namespace FreeBilling.Models;

public class SignInResultModel
{
  public string Token { get; set; } = "";
  public DateTime Expiration { get; set; } = DateTime.MinValue;
  public string Username { get; set; } = "";
}