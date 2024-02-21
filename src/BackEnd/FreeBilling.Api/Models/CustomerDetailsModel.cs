using FreeBilling.Data.Entities;

namespace FreeBilling.Api.Models;

public class CustomerDetailsModel
{
  public int Id { get; set; }
  public string CompanyName { get; set; } = "";
  public Address? Address { get; set; }
  public string? Contact { get; set; }
  public string? PhoneNumber { get; set; }
  public double TotalBalance { get; set; }

  public ICollection<ProjectModel>? Projects { get; set; }
}
